using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flight1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        double y = 0;
        double t = 0;
        double x = 0;
        double y0;
        double v0;
        double alpha;
        double times = 0;

        bool isPlaying = false;

        const double dt = 0.01;
        const double g = 9.81;
        private void btStart_Click(object sender, EventArgs e)
        {
            v0 = (double)edSpeed.Value;
            y0 = (double)edHight.Value;
            alpha = (double)edAngle.Value;

            x = 0;
            y = y0;
            t = 0;

            chart1.Series[0].Points.Clear();
            chart1.Series[0].Points.AddXY(x, y);

            UpdateArea();
            FlightStart();
        }
        

        private void FlightStart()
        {
            isPlaying = true;
            timer1.Start();
        }

        private void FlightStop()
        {
            isPlaying = false;
            timer1.Stop();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            t += dt;
            x = v0 * Math.Cos(alpha * Math.PI / 180) * t;
            y = y0 + v0 * Math.Sin(alpha * Math.PI / 180) * t - g * t * t / 2;
          //  chart1.Series[0].Points.Clear();
            chart1.Series[0].Points.AddXY(x, y);
            times++;
            labelTime.Text = "Time: " + times;
            if (y <= 0)
            {
                FlightStop();
            }
        }

       

        private void btStop_Click(object sender, EventArgs e)
        {
            if (isPlaying)
            {
                FlightStop();
                btStop.Text = "CONTINUE";
            }
            else
            {
                FlightStart();
                btStop.Text = "STOP";
            }

        }

        private void edHight_ValueChanged(object sender, EventArgs e)
        {
            y0 = (double)(sender as NumericUpDown).Value;
            UpdateArea();
        }

        private void edSpeed_ValueChanged(object sender, EventArgs e)
        {
            v0 = (double)(sender as NumericUpDown).Value;
            UpdateArea();
        }

        private void UpdateArea()
        {
            
            chart1.ChartAreas[0].AxisX.Maximum = (double) v0*v0*Math.Sin(2* alpha*Math.PI / 180) /g * 1.1;
            chart1.ChartAreas[0].AxisY.Maximum = (double) edSpeed.Value * 3;

        }
    }
}


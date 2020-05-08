using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Integrals
{
    partial class Graph : Form
    {
        double min;
        double max;
        int quantity;
        double interval;
        MidpointMethod d;
        SimpsonsMethod d1;
        MonteCarloMethod d2;
        Get_Data g;
        
        public Graph()
        {
            InitializeComponent();
        }
        public Graph(Get_Data f, decimal a, decimal b, decimal quantity,MidpointMethod d)
        {
            
            this.min = (double)a;
            this.max = (double)b;
            this.quantity = (int)quantity;
            interval = (double)(b - a) / this.quantity;
            d.EventColumn += OnColumn;
            d.EventProgress += OnProgress;
            d.EventFinish += OnFinish;
            d.EventTime += OnTime;
            this.d = d;
            //d.Start();
            f.Hide();
            g = f;
            InitializeComponent();
        }
        public Graph(Get_Data f, decimal a, decimal b, decimal quantity, SimpsonsMethod d)
        {
            this.min = (double)a;
            this.max = (double)b;
            this.quantity = (int)quantity;
            interval = (double)(b - a) / this.quantity;
            d.EventSpline += OnSpline1; 
            d.EventProgress += OnProgress;
            d.EventFinish += OnFinish;
            d.EventTime += OnTime;
            this.d1 = d;
            f.Hide();
            g = f;
            InitializeComponent();
        }
        public Graph(Get_Data f, decimal a, decimal b, decimal quantity, MonteCarloMethod d)
        {
            this.min = (double)a;
            this.max = (double)b;
            this.quantity = (int)quantity;
            interval = (double)(b - a) / this.quantity;
            d.EventPoints += OnPoints;
            d.EventNeedPoints += OnNeedPoints;
            d.EventProgress += OnProgress;
            d.EventFinish += OnFinish;
            d.EventTime += OnTime;
            this.d2 = d;
            f.Hide();
            g = f;
            InitializeComponent();
        }

        private void Graph_Load(object sender, EventArgs e)
        {
            progressBar1.Maximum = quantity;
            chart1.Series[0].Name = "Функция";
           
            OnSpline();
            if(d!=null) d.Start();
            if (d1 != null) d1.Start();
            if (d2 != null) d2.Start();



        }
        void OnNeedPoints(decimal x, decimal y)
        {

            if (!chart1.InvokeRequired)
            {
                if (chart1.Series.Count <= 3) chart1.Series.Add("Точки");
                chart1.Series[3].ChartType = SeriesChartType.Bubble;
                chart1.Series[3].BorderWidth = 1;
                chart1.Series[3].Points.AddXY(x, y);
            }
            else
            {
                object[] pars = { x, y };
                Invoke(new MonteCarloMethod.NeedPoints(OnNeedPoints), pars);
            }

        }

        void OnPoints(decimal x, decimal y)
        {

            if (!chart1.InvokeRequired)
            {
                if (chart1.Series.Count <= 2)
                {
                    chart1.Series.Add("Метод3");

                    chart1.Series.Add("Прямоугольник");
                    chart1.Series[2].ChartType = SeriesChartType.Line;
                    chart1.Series[2].BorderWidth = 3;
                    chart1.Series[2].Points.AddXY(min, 0);
                    chart1.Series[2].Points.AddXY(min, 2);
                    chart1.Series[2].Points.AddXY(max, 2);
                    chart1.Series[2].Points.AddXY(max, 0);
                    chart1.Series[2].Points.AddXY(min, 0);


                }
                chart1.Series[1].ChartType = SeriesChartType.Bubble;
                chart1.Series[1].BorderWidth = 1;
                chart1.Series[1].Points.AddXY(x, y);
            }
            else
            {
                object[] pars = { x, y };
                Invoke(new MonteCarloMethod.Points(OnPoints), pars);
            }

        }

        void OnColumn(decimal x, decimal y)
        {
            
            if (!chart1.InvokeRequired)
            {
               if(chart1.Series.Count<=1) chart1.Series.Add("Метод1");
               
                chart1.Series[1].Points.AddXY(x, y);
            }
            else
            {
                object[] pars = { x, y };
                Invoke(new MidpointMethod.Column(OnColumn), pars);
            }

        }

        void OnSpline()
        {
            
            chart1.Series[0].ChartType = SeriesChartType.Spline;
            chart1.Series[0].BorderWidth= 7;
            double x = min;
            int N = quantity+1;
            for (int i = 1; i <=N; i++)
            {
                double y = func(x);
                chart1.Series[0].Points.AddXY(x, y);
                x += interval;
            }
            //chart1.Series[1].Points.Max();
        }
        void OnSpline1(decimal x, decimal y)
        {

            if (!chart1.InvokeRequired)
            {
                //count++;

                if (chart1.Series.Count <= 1) chart1.Series.Add("Метод2");
                chart1.Series[1].ChartType = SeriesChartType.Spline;
                chart1.Series[1].BorderWidth = 3;
                chart1.Series[1].Points.AddXY(x, y);
                chart1.Series[1].Sort(0);
            }
            else
            {
                object[] pars = { x, y };
                Invoke(new SimpsonsMethod.Spline(OnSpline1), pars);
            }
        }
        private void OnProgress(int value)
        {
            if (!progressBar1.InvokeRequired)
                progressBar1.Value = value;
            else
            {

                if (d != null) Invoke(new MidpointMethod.Progress(OnProgress), value);
                if (d1!=null) Invoke(new SimpsonsMethod.Progress(OnProgress), value);
                if (d2 != null) Invoke(new MonteCarloMethod.Progress(OnProgress), value);
            }
        }
        private void OnFinish(decimal resVal)
        {
            if (!Answer.InvokeRequired)
            {
                Answer.Text = "Ответ " + resVal;
            }
            else
            {
                if (d != null) Invoke(new MidpointMethod.Finish(OnFinish), resVal);
                if (d1 != null) Invoke(new SimpsonsMethod.Finish(OnFinish), resVal);
                if (d2 != null) Invoke(new MonteCarloMethod.Finish(OnFinish), resVal);

            }
        }
        private void OnTime(decimal resVal)
        {
            if (!label1.InvokeRequired)
            {
                label1.Text = "Время" + resVal;
            }
            else
            {
                if (d != null) Invoke(new MidpointMethod.Time(OnTime), resVal);
                if (d1 != null) Invoke(new SimpsonsMethod.Time(OnTime), resVal);
                if (d2 != null) Invoke(new MonteCarloMethod.Time(OnTime), resVal);

            }
        }
        double func(double x)
        {
            var res = ((Math.Pow(Math.E, x)) / (Math.Pow(x, 3) - Math.Pow(Math.Sin(x), 3)));
            return res;
        }

        private void Graph_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (d != null) d.Stop();
            if (d1 != null) d1.Stop();
            if (d2 != null) d2.Stop();
            if(g!=null) g.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Get_Data g1 = new Get_Data(this);
            g1.Show();
            
        }
    }   
}

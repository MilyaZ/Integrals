using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Integrals
{
    class Point
    {
        double X { get; set; }
        double Y { get; set; }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }
        public double GetX() { return X; }
        public double GetY() { return Y; }
    }

    

    class MonteCarloMethod
    {

        List<Point> points = new List<Point>();
        Random r;
        double a, b;
        int quantity;
        int parts = Environment.ProcessorCount;
        int donePercent = 0;
        private Thread t = null;
        double max;
        public double Getmax() { return max; }

        private class Sum
        {
            public double value;
        }
        private Sum res;
        public double Result
        {
            get { return res.value; }
            private set { res.value = value; }
        }

        public delegate void Points(double x, double y,double max);
        public event Points EventPoints;

        public delegate void NeedPoints(double x, double y);
        public event NeedPoints EventNeedPoints;

        public delegate void Progress(int value);
        public event Progress EventProgress;

        public delegate void Finish(double resultValue);
        public event Finish EventFinish;
        public delegate void Time(double resultValue);
        public event Time EventTime;
        public MonteCarloMethod(double a, double b, double quantity)
        {
            this.a = a;
            this.b = b;
            this.quantity = (int)quantity;
            res = new Sum();
            Result = 0;
        }
        public void Integrate()
        {
            max = maxi();
            Parallel.For(
               0,
               parts,
               new Action<int>(Generate)
           );
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Parallel.For(
                0,
                parts,
                new Action<int>(_Integrate)
            );
            double I = Result / (double)(quantity) * Math.Abs(b - a) * 2;
            sw.Stop();
            if (donePercent != quantity) { EventProgress?.Invoke(quantity); }
            EventFinish?.Invoke(I);
            EventTime?.Invoke(sw.ElapsedMilliseconds);
          
        }
        private void _Integrate(int part)
        {
            int partsSize = (int)(quantity / parts); 
            int ost = quantity - partsSize * parts; 
            int st = part * partsSize + ((part < ost) ? part : ost);
            int fn = (part + 1) * partsSize + ((part + 1 < ost) ? part : (ost - 1));
            int Count = 0;
            for (int i = st; i <= fn; i++)
            {
                if (points[i].GetY() < func(points[i].GetX()))
                {
                    Thread.Sleep(100);
                    Count++;
                    EventNeedPoints?.Invoke(points[i].GetX(),points[i].GetY());
                }
                donePercent += 1;
                EventProgress?.Invoke(donePercent);
                

            }
            Monitor.Enter(res);
            try
            {
                Result += Count;
            }
            finally
            {
                Monitor.Exit(res);
            }
        }
        private void Generate(int part)
        {
           
            int partsSize = (int)(quantity) / (parts); 
            int ost = (quantity) - partsSize * parts; 
            int st = part * partsSize + ((part < ost) ? part : ost);
            int fn = (part + 1) * partsSize + ((part + 1 < ost) ? part : (ost - 1));
            for (int i = st; i <= fn; i++)
            {
                if (r== null) r = new Random(DateTime.UtcNow.Millisecond);
                Point t = new Point(r.NextDouble()*(b-a)+a, r.NextDouble()*((int)max+1));
                points.Add(t);
                EventPoints?.Invoke(t.GetX(), t.GetY(),max+1);
            }
           
        }
        public void Start()
        {
            if (t == null || !t.IsAlive)
            {
                ThreadStart th = new ThreadStart(Integrate);
                t = new Thread(th);
                t.Start();
            }
        }
        double maxi()
        {
            const double epsilon = 1e-10;
            double a1 = a;
            double b1 = b;

            double goldenRatio = (1 + Math.Sqrt(5)) / 2; // "Золотое" число
           
            double x1, x2; // Точки, делящие текущий отрезок в отношении золотого сечения
            while (Math.Abs(b1 - a1) > epsilon)
            {
                x1 = b1 - (b1 - a1) / goldenRatio;
                x2 = a1 + (b1 - a1) / goldenRatio;
                if (func(x1) <= func(x2)) a1 = x1;
                else b1 = x2;
            }
            return func((a1 + b1) / 2);
        }

        double func(double x)
        {
            var res = ((Math.Pow(Math.E, (double)x)) / (Math.Pow((double)x, 3) - Math.Pow(Math.Sin((double)x), 3)));
            return res;
        }
        public void Stop()
        {
            t.Abort();
            t.Join();
        }
    }
}

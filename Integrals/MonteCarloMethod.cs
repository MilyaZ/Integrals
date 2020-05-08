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
        decimal X { get; set; }
        decimal Y { get; set; }

        public Point(decimal x, decimal y)
        {
            X = x;
            Y = y;
        }
        public decimal GetX() { return X; }
        public decimal GetY() { return Y; }
    }

    

    class MonteCarloMethod
    {

        List<Point> points = new List<Point>();
        Random r; 
        decimal a, b;
        int quantity;
        int parts = 4;
        int donePercent = 0;
        private Thread t = null;
        private class Sum
        {
            public decimal value;
        }
        private Sum res;
        public decimal Result
        {
            get { return res.value; }
            private set { res.value = value; }
        }

        public delegate void Points(decimal x, decimal y);
        public event Points EventPoints;

        public delegate void NeedPoints(decimal x, decimal y);
        public event NeedPoints EventNeedPoints;

        public delegate void Progress(int value);
        public event Progress EventProgress;

        public delegate void Finish(decimal resultValue);
        public event Finish EventFinish;
        public delegate void Time(decimal resultValue);
        public event Time EventTime;
        public MonteCarloMethod(decimal a, decimal b, decimal quantity)
        {
            this.a = a;
            this.b = b;
            this.quantity = (int)quantity;
            res = new Sum();
            Result = 0;

        }
        public void Integrate()
        {
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
            decimal I = Result / (decimal)(quantity) * Math.Abs(b - a) * 2;
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
                Point t = new Point((a+ (decimal)(r.Next() % 100) / (decimal)100), r.Next(0, 20000) /(decimal)10000);
                points.Add(t);
                EventPoints?.Invoke(t.GetX(), t.GetY());
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

        decimal func(decimal x)
        {
            var res = ((Math.Pow(Math.E, (double)x)) / (Math.Pow((double)x, 3) - Math.Pow(Math.Sin((double)x), 3)));
            return (decimal)res;
        }
        public void Stop()
        {
            t.Abort();
            t.Join();
        }
    }
}

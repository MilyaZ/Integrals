using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Integrals
{
    class SimpsonsMethod
    {
        private Thread t = null;
        decimal a, b;
        int quantity;
        int parts = 4;
        decimal h;
        int donePercent = 0;
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

        public delegate void Spline(decimal x, decimal y);
        public event Spline EventSpline;

        public delegate void Progress(int value);
        public event Progress EventProgress;

        public delegate void Finish(decimal resultValue);
        public event Finish EventFinish;

        public delegate void Time(decimal resultValue);
        public event Time EventTime;

        public SimpsonsMethod(decimal a, decimal b, decimal quantity)
        {
            this.a = a;
            this.b = b;
            this.quantity = (int)quantity;
            h = (b - a) / (this.quantity);
            res = new Sum();
            Result = -func(a) + func(b);

        }
        public void Integrate()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Parallel.For(
                0,
                parts,
                new Action<int>(_Integrate)
            );
            decimal I = (h / 3) * Result;
            sw.Stop();
            if (donePercent != quantity) { EventProgress?.Invoke(quantity); }
            EventFinish?.Invoke(I);
            EventTime?.Invoke(sw.ElapsedMilliseconds);
            
        }
        private void _Integrate(int part)
        {
            Result = -func(a) + func(b);
           
            int partsSize = (int)(quantity / 2) / (parts); 
            int ost = (quantity / 2) - partsSize * parts; 
            int st = part * partsSize + ((part < ost) ? part : ost);
            int fn = (part + 1) * partsSize + ((part + 1 < ost) ? part : (ost - 1));
            decimal sum2 = 0;
            decimal sum4 = 0;
            for (int i = st; i <= fn; i++)
            {
                Thread.Sleep(100);
                var s2 = func(a + 2 * i * h);
                var s4 = func(a + h * (2 * i + 1));
                sum2 += s2;
                sum4 += s4;
                donePercent += 2;
                EventProgress?.Invoke(donePercent);
                EventSpline?.Invoke(a + 2 * i * h, s2);
                EventSpline?.Invoke(a + h * (2 * i + 1), s4);

            }
            Monitor.Enter(res);
            try
            {
                Result += 2 * sum2;
                Result += 4 * sum4;
            }
            finally
            {
                Monitor.Exit(res);
            }
        }
        decimal func(decimal x)
        {
            var res = ((Math.Pow(Math.E, (double)x)) / (Math.Pow((double)x, 3) - Math.Pow(Math.Sin((double)x), 3)));
            return (decimal)res;
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
        public void Stop()
        {
            t.Abort();
            t.Join();
        }
    }
}

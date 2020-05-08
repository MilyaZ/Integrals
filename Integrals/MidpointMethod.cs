using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Integrals
{
    class MidpointMethod

    {
        decimal a, b;
        int quantity;
        int parts = 4;
        decimal h;
        private Thread t = null;
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

        public delegate void Column(decimal x, decimal y);
        public event Column EventColumn;

        public delegate void Progress(int value);
        public event Progress EventProgress;

        public delegate void Finish(decimal resultValue);
        public event Finish EventFinish;

        public delegate void Time(decimal resultValue);
        public event Time EventTime;

        public MidpointMethod(decimal a, decimal b, decimal quantity)
        {
            this.a = a;
            this.b = b;
            this.quantity =(int) quantity;
            h = (b - a) / this.quantity;
            res = new Sum();
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

            decimal I = h * Result;
            sw.Stop();
            if (donePercent != quantity) { EventProgress?.Invoke(quantity); }
            EventFinish?.Invoke(I);
            EventTime?.Invoke(sw.ElapsedMilliseconds);
   
        }
        private void _Integrate(int part)
        {
            
            Result = (-func(a) + func(b)) / 2;
           
            int partsSize = (int)quantity/ parts; 
            int ost = quantity- partsSize * parts; 
            int st = part * partsSize + ((part < ost) ? part : ost);
            int fn = (part + 1) * partsSize + ((part + 1 < ost) ? part : (ost - 1));
            decimal s = 0;
            for (int i = st; i <=fn; i++)
            {
                Thread.Sleep(100);
                var f= func(a + (i* h));
                s += f;
                donePercent +=1;
                EventProgress?.Invoke(donePercent);
                EventColumn?.Invoke(a + (i + 1) * h, f);
            }
            Monitor.Enter(res);
            try
            {
                Result += s;
            }
            finally
            {
                Monitor.Exit(res);
            }
        }
        decimal func(decimal x)
        {
            
            var res = ((Math.Pow(Math.E, (double)x)) / (Math.Pow((double)x, 3) - Math.Pow(Math.Sin((double)x), 3)));
            EventColumn?.Invoke(x,(decimal)res);
            return (decimal) res;
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

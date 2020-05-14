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
        double a, b;
        int quantity;
        int parts = 4;
        double  h;
        private Thread t = null;
        int donePercent = 0;
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

        public delegate void Column(double x, double y);
        public event Column EventColumn;

        public delegate void Progress(int value);
        public event Progress EventProgress;

        public delegate void Finish(double resultValue);
        public event Finish EventFinish;

        public delegate void Time(double resultValue);
        public event Time EventTime;

        public MidpointMethod(double a, double b, double quantity)
        {
            this.a = a;
            this.b = b;
            this.quantity =(int) quantity;
            h = (b - a) / this.quantity;
            res = new Sum();
           
            //ThreadStart th = new ThreadStart(Integrate);
            //t = new Thread(th);
            //t.Start();

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

            double I = h * Result;
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
            double s = 0;
            for (int i = st; i <=fn; i++)
            {
                
                var f= func(a + h *(i + (1/(double)2)));
                s += f;
                donePercent +=1;
                EventProgress?.Invoke(donePercent);
                EventColumn?.Invoke((a + h * (i+ (1 / (double)2))), f);
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
        double func(double x)
        {
            
            var res = ((Math.Pow(Math.E, (double)x)) / (Math.Pow((double)x, 3) - Math.Pow(Math.Sin((double)x), 3)));
            return res;
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

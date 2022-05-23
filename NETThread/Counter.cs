using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace CounterClass
{
    class Counter
    {
        const int LOOP_COUNT = 100;

        readonly object thisLock;

        private int count;

        public int Count
        {
            get { return count; }
        }


        public Counter()
        {
            thisLock = new object();
            count = 0;
        }


        public void Increase()
        {
            int loopCount = LOOP_COUNT;
            while (loopCount-- > 0)
            {
                lock (thisLock)
                {
                    count++;
                    Console.WriteLine("Increase" + count);
                }
                Thread.Sleep(1);
            }
        }


        public void Decrease()
        {
            int loopCount = LOOP_COUNT;
            while (loopCount-- > 0)
            {
                lock (thisLock)
                {
                    count--;
                    Console.WriteLine("Decrease" + count);
                }
                Thread.Sleep(1);
            }

        }


    }
}

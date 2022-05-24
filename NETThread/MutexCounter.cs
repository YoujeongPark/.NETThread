using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NETThread
{
    class MutexCounter
    {
        private static Mutex mutex = new Mutex();
        private static int number = 100; 
        public void Print(string _thread)
        {

            mutex.WaitOne();
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(_thread + " : " + number--);
            }
            mutex.ReleaseMutex();
        }

        public void FirstWork()
        {
            Print("F");
        }

        public void SecondWork()
        {
            Print("S");
        }
    }
}

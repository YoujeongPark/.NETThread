using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using CounterClass;
using NETThread;

internal class Program
{
    static bool myFlag = false;

    private static void PrintThreadState(ThreadState state)
    {
        Console.WriteLine("{0-16} : {1} ", state, (int)state);
    }


    private static void ThreadFunc()
    {
        for (int i = 0; i < 10; i++)
        {
            Console.WriteLine("서브스레드 : " + (i + 1));
            Thread.Sleep(100);
        }

    }

    

    private static void ThreadPolling()
    {
        while (true)
        {
            if (myFlag)
            {
                Console.WriteLine("폴링성공");
            }
            Thread.Sleep(100);

        }
    }




    static void Main(string[] args)
    {
        bool runServer = true;
        while (runServer)
        {
            Console.WriteLine("---------------------------");
            Console.WriteLine("해당 작업을 선택하시오\n" +
            "1. Thread 실행  \n" +
            "2. Thread + Lambda \n" +
            "3. Thread 실행 Polling + Abort\n" +
            "4. Thread 실행 Polling  + Interrupt\n" +
            "5. Thread 실행 Lock \n" +
            "6. Multi Threading \n"
            );

            int inputNumber = Int32.Parse(Console.ReadLine());
            switch (inputNumber)
            {
                case 1:
                    Thread thread11 = new Thread(ThreadFunc);
                    thread11.Start();
                    
                    for (int i = 0; i < 3; i++)
                    {
                        Console.WriteLine("메인스레드" + (i + 1));
                        Thread.Sleep(100);

                    }
                    thread11.Join(); // 위치에 따라 어떻게 실행되는지 다름 
                    Console.WriteLine("메인스레드 종료 ");
                    break;
                case 2:
                    int sum = 0; 
                    var thread2 = new Thread(() =>
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            Console.WriteLine(DateTime.Now.ToString("yyyy/MM/dd hh:mm:ssss"));
                            sum++; 
                        }
                    });
                    thread2.Start();
                    Console.WriteLine("Thread 2");
                    thread2.Join(); //Join이 있어야 Thread를 Waiting... 
                    Console.WriteLine(sum);
                    break;
                case 3:
                    Thread thread3 = new Thread(ThreadPolling);
                    thread3.Start();
                    for (int i = 0; i < 3; i++)
                    {
                        Console.WriteLine(i + 1);
                        Thread.Sleep(100);
                    }
                    myFlag = true;
                    thread3.Abort();
                    thread3.Join();
                    Console.WriteLine("메인쓰레드 종료 ");
                    break;
                case 4:
                    Thread thread4 = new Thread(ThreadPolling);
                    thread4.Start();
                    for (int i = 0; i < 3; i++)
                    {
                        Console.WriteLine(i + 1);
                        Thread.Sleep(100);
                    }
                    myFlag = true;
                    //thread3.Abort();
                    //thread3.Join();
                    thread4.Interrupt();
                    Console.WriteLine("메인쓰레드 종료 ");
                    break;
                case 5:
                    Counter counter = new CounterClass.Counter();

                    Thread t1 = new Thread(new ThreadStart(counter.Increase));
                    Thread t2 = new Thread(new ThreadStart(counter.Decrease));

                    t1.Start();
                    t2.Start();
                    t1.Join();
                    t2.Join();

                    Console.WriteLine(counter.Count);

                    break;
                case 6:
                    MutexCounter mutexCounter = new MutexCounter();
                    Thread first =  new Thread(new ThreadStart(mutexCounter.FirstWork));
                    Thread second = new Thread(new ThreadStart(mutexCounter.SecondWork));
                    first.Start(); 
                    second.Start();

                    first.Join();
                    second.Join();
                    break;
                case 11:
                    PrintThreadState(ThreadState.Running);
                    PrintThreadState(ThreadState.StopRequested);
                    PrintThreadState(ThreadState.SuspendRequested);
                    PrintThreadState(ThreadState.Background);
                    PrintThreadState(ThreadState.Unstarted);
                    PrintThreadState(ThreadState.Stopped);
                    PrintThreadState(ThreadState.WaitSleepJoin);
                    PrintThreadState(ThreadState.Suspended);
                    PrintThreadState(ThreadState.AbortRequested);
                    PrintThreadState(ThreadState.Aborted);
                    PrintThreadState(ThreadState.Aborted | ThreadState.Stopped);
                    break;
                default:
                    Console.WriteLine("다시 재 선택 하시오. ");
                    break;

            }
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

internal class Program
{

    private static void ThreadFunc()
    {
        for (int i = 0; i < 10; i++)
        {
            Console.WriteLine("서브스레드 : " + (i + 1));
            Thread.Sleep(100);
        }

    }

    static bool myFlag = false;

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
            Console.WriteLine("해당 작업을 선택하시오" +
            "1. Thread 실행  \n" +
            "2. Thread 실행 Polling \n" +
            "3. Thread 실행 Lock \n");

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
                    Thread thread12 = new Thread(ThreadPolling);
                    thread12.Start();
                    for (int i = 0; i < 3; i++)
                    {
                        Console.WriteLine(i + 1);
                        Thread.Sleep(100);
                    }
                    myFlag = true;
                    thread12.Abort();
                    thread12.Join();
                    Console.WriteLine("메인쓰레드 종료 ");
                    
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


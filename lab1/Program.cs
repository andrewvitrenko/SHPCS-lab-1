using System.Runtime.InteropServices;
using lab1;

namespace lab1
{
    class Program
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetCurrentThread();

        [DllImport("kernel32.dll")]
        static extern IntPtr SetThreadAffinityMask(IntPtr hThread, IntPtr
            dwThreadAffinityMask);

        private static bool isLarge;
        private static int N;

        public static void Main()
        {
            N = Data.Read("Input N: ");
            isLarge = N > 100;
            Console.WriteLine("Hello world!");

            int stackSize = 1024 * 1024;

            Thread T1 = new Thread(F1, stackSize)
            {
                Name = "Thread 1",
                Priority = ThreadPriority.Normal,
            };

            Thread T2 = new Thread(F2, stackSize)
            {
                Name = "Thread 2",
                Priority = ThreadPriority.Normal,
            };

            Thread T3 = new Thread(F3, stackSize)
            {
                Name = "Thread 3",
                Priority = ThreadPriority.Normal,
            };

            T1.Start();
            T2.Start();
            T3.Start();
        }

        static void SetThreadAffinity(int processorNumber)
        {
            if (OperatingSystem.IsWindows())
            {
                IntPtr ptrThread = GetCurrentThread();
                SetThreadAffinityMask(ptrThread, new IntPtr(1 << processorNumber));
                Console.WriteLine($"Thread {Thread.CurrentThread.Name} is running on the core {processorNumber}");
            }
        }


        private static void F1()
        {
            Console.WriteLine("Thread 1 starts");

            SetThreadAffinity(0);

            Data data = new Data(isLarge, N);
            int[] result = data.F1();

            data.Write(@"..\..\..\f1.txt", "Thread 1 result",result);

            Console.WriteLine("Thread 1 finished");
        }

        private static void F2()
        {
            Console.WriteLine("Thread 2 starts");

            SetThreadAffinity(1);

            Data data = new Data(isLarge, N);
            int result = data.F2();

            data.Write(@"..\..\..\f2.txt", "Thread 2 result", result);

            Console.WriteLine("Thread 2 finished");
        }

        private static void F3()
        {
            Console.WriteLine("Thread 3 starts");

            SetThreadAffinity(2);

            Data data = new Data(isLarge, N);
            int[] result = data.F3();

            data.Write(@"..\..\..\f3.txt", "Thread 3 result", result);

            Console.WriteLine("Thread 3 finished");
        }
    }
}
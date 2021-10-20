using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace lab1_queue
{
    class Program
    {
        static void Main(string[] args)
        {
            int threadsNumber, taskNumber;
            Console.WriteLine("Enter number of threads:");
            threadsNumber = Convert.ToInt32(Console.ReadLine());
            if (threadsNumber <= 0)
            {
                Console.WriteLine("Number of threads must be more than 0. Enter number of threads:");
                threadsNumber = Convert.ToInt32(Console.ReadLine());
            }
            Console.WriteLine("Enter number of tasks:");
            taskNumber = Convert.ToInt32(Console.ReadLine());
            if (taskNumber < 0)
            {
                Console.WriteLine("Number of threads must be positive or 0. Enter number of tasks:");
                threadsNumber = Convert.ToInt32(Console.ReadLine());
            }

            TaskQueue taskQueue = new TaskQueue(threadsNumber);

            for (int i = 0; i < taskNumber; i++)
            {
                taskQueue.EnqueuTask(MyTask);
            }
            Console.ReadLine();
            taskQueue.FinishThreads();
            void MyTask()
            {
                Console.WriteLine("Thread №{0} completed task", Thread.CurrentThread.Name);
            }
            
        }
    }
}

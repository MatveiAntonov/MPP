using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace fileThreads
{
    class TaskQueue
    {
        public delegate void TaskDelegate();
        private List<Thread> ThreadsPool = new List<Thread>();
        private static List<bool> ActiveThreads = new List<bool>();
        public ConcurrentQueue<string> TaskList = new ConcurrentQueue<string>();

        public  string destination;
        public static int NumberOfCopiedFiles = 0;

        public TaskQueue(int threadsNumber, string dest)
        {
            for (int i = 0; i < threadsNumber; i++)
            {
                Thread thread = new Thread(TaskProc);
                destination = dest;
                thread.Name = i.ToString();
                thread.IsBackground = true;
                ActiveThreads.Add(false);
                ThreadsPool.Add(thread);
                thread.Start();
            }
        }

        public void EnqueueTask(string task)
        {
            TaskList.Enqueue(task);
        }

       

        public void FinishThreads()
        {
            foreach (Thread t in ThreadsPool)
            {
                t.IsBackground = false;
            }
        }

        private void TaskProc()
        {
            ActiveThreads[Convert.ToInt32(Thread.CurrentThread.Name)] = true;
            while (Thread.CurrentThread.IsBackground)
            {
                if (TaskList.TryDequeue(out string fileName))
                    CopyFile(fileName, destination);
            }
            ActiveThreads[Convert.ToInt32(Thread.CurrentThread.Name)] = false;
        }

        private void CopyFile(string fileName, string dest)
        {
            Console.WriteLine($"COPY {fileName} TO {dest} WITH THREAD №{Convert.ToInt32(Thread.CurrentThread.Name) + 1}");
            FileInfo fileInfo = new FileInfo(fileName);
            fileInfo.CopyTo(Path.Combine(dest, fileInfo.Name), true);
            NumberOfCopiedFiles++;
        }
        public bool CheckActiveThreads()
        {
            return ActiveThreads.Contains(true);
        }
    }
}

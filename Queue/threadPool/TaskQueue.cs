using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace lab1_queue
{
    class TaskQueue
    {
        public delegate void TaskDelegate();
        private List<Thread> ThreadsPool = new List<Thread>();
        ConcurrentQueue<TaskDelegate> TaskList= new ConcurrentQueue<TaskDelegate>();
        private List<Thread> pool = new List<Thread>();

        public TaskQueue (int threadsNumber)
        {
            for (int i = 0; i < threadsNumber; i++)
            {
                Thread thread = new Thread(TaskProc);
                thread.Name = (i + 1).ToString();
                thread.IsBackground = true;
                ThreadsPool.Add(thread);
                thread.Start();
            }
        }

        public void FinishThreads()
        {
            foreach (Thread t in pool)
            {
                t.IsBackground = false;
            }
        }

        private void TaskProc()
        {
            while (Thread.CurrentThread.IsBackground)
            {
                if (TaskList.TryDequeue(out TaskDelegate task) && task != null)
                    task();
            }  
        }

        // Постановка задачи в очередь
        public void EnqueuTask(TaskDelegate task)
        {
            TaskList.Enqueue(task);
        }
    }
}

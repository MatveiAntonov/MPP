using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace fileThreads
{
    class Program
    {
        static void Main(string[] args)
        {
            string source, destination;
            int threadsNumber;
            string[] fileNames;
            if (args.Length == 3)
            {
                source = args[0];
                destination = args[1];
                threadsNumber = Convert.ToInt32(args[2]);
            }
            else
            {
                source = @"D:\BSUIR\5sem\СПП\Copy_source";
                destination = @"D:\BSUIR\5sem\СПП\Copy_destination";
                threadsNumber = 0;
            }

            TaskQueue task = new TaskQueue(threadsNumber, destination);
            fileNames = Directory.GetFiles(source);

            foreach (var file in fileNames)
            {
                task.EnqueueTask(file);
            }

            while (task.TaskList.Count > 0) { }

            task.FinishThreads();
            while (task.CheckActiveThreads()) { }
            Console.WriteLine($"\nNumber of copied files: {TaskQueue.NumberOfCopiedFiles}");

            Console.ReadLine();
        }
    }
}

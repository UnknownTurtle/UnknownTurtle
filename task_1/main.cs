using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace task_1
{
    class Program
    {
        static void Main(string[] args)
        {
            const int p = 3;
            const int c = 2;
            List<Producer> producers = new List<Producer>();
            List<Consumer> consumers = new List<Consumer>();
            myRequests _queue = new myRequests();

            for (int i = 0; i < p; i++)
            {
                Producer producer = new Producer(ref _queue, i);
                Thread.Sleep(1);
                producers.Add(producer);
                producer.Start();
            }

            for (int i = 0; i < c; i++)
            {
                Consumer consumer = new Consumer(ref _queue, i);
                consumers.Add(consumer);
                consumer.Start();
            }
            
            Console.ReadKey();

            foreach(Producer prod in producers)
            {
                prod.Stop();
            }
            foreach (Consumer cons in consumers)
            {
                cons.Stop();
            }

            Console.WriteLine("The program is stop. Have a nice day!");
            // _queue.getList();
            //Thread.Sleep(5000);
            Console.ReadKey();
        }
    }
}
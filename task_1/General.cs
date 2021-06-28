using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace task_1
{
    public abstract class General
    {
        private volatile bool active = false;
        public int name = -1;
        protected Thread myThread;
        public void Start()
        {
            active = true;
            myThread.Start();
        }
        public void Stop()
        {
            active = false;
            myThread.Join();
        }
        public bool Check()
        {
            return active;
        }
    }

    public class Producer : General
    {
        myRequests queue;
        public Producer(ref myRequests queue, int name)
        {
            this.name = name;
            this.queue = queue;
            myThread = new Thread(produce);

        }
        public void produce()
        {
            while (this.Check())
            {
                Request req = new Request();
                bool acquiredLock = false;
                try
                {
                    Monitor.Enter(queue, ref acquiredLock);
                    Console.WriteLine("added request " + req.value + " from " + (this.name + 1) + " thread");
                    queue.Add(req);
                    queue.getList();
                }
                finally
                {
                    if (acquiredLock) Monitor.Exit(queue);
                }
                Thread.Sleep(1000);
            }
        }
    }
    public class Consumer : General
    {
        myRequests queue;
        public Consumer(ref myRequests queue, int name)
        {
            this.name = name;
            this.queue = queue;
            myThread = new Thread(consume);
        }
        public void consume()
        {
            while (this.Check())
            {
                Request req = new Request();
                bool acquiredLock = false;
                try
                {
                    Monitor.Enter(queue, ref acquiredLock);
                    req = queue.Take();
                    if (req == null)
                    {
                        Console.WriteLine("The queue is empty");
                    }
                    else
                    {
                        Console.WriteLine("getting request " + req.value + " for " + (this.name + 1) + " thread");
                        queue.getList();
                    }
                }
                finally
                {
                    if (acquiredLock) Monitor.Exit(queue);
                }
                Thread.Sleep(1000);

            }
        }
    }


}

using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace task_1.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TakeSomeRequestsMoreThanYouHave()
        {
            //List<Request> myList = new List<Request>();
            Request req = new Request();
            myRequests requests = new myRequests();
            for (int i = 0; i < 5; i++)
            {
                requests.Add(req);
            }
            for (int i = 0; i < 7; i++)
            {
                requests.Take();
            }
        }

        [TestMethod]
        public void GenerateProducers()
        {
            List<Producer> producers = new List<Producer>();
            myRequests _queue = new myRequests();

            for (int i = 0; i < 3; i++)
            {
                Producer producer = new Producer(ref _queue, i);
                producers.Add(producer);
                producer.Start();
            }

            Assert.AreEqual(3, producers.Count);       
        }

        [TestMethod]
        public void GenerateConsumers()
        {
            List<Consumer> consumers = new List<Consumer>();
            myRequests _queue = new myRequests();

            for (int i = 0; i < 3; i++)
            {
                Consumer cons = new Consumer(ref _queue, i);
                consumers.Add(cons);
                cons.Start();
            }

            Assert.AreEqual(3, consumers.Count);

        }

        [TestMethod]
        public void UnActiveConsumersDoNotTakeRequests()
        {
            List<Consumer> consumers = new List<Consumer>();
            myRequests _queue = new myRequests();
            for (int i = 0; i < 5; i++)
            {
                Request req = new Request
                {
                    value = i
                };
                _queue.Add(req);
            }

            for (int i = 0; i < 3; i++)
            {
                Consumer cons = new Consumer(ref _queue, i);
                consumers.Add(cons);
                //cons.Start();
                cons.consume();
            }

            Assert.AreEqual(5, _queue.getCountRequests());
        }

        [TestMethod]
        public void ActiveConsumersTakeRequests()
        {
            List<Consumer> consumers = new List<Consumer>();
            myRequests _queue = new myRequests();

            for (int i = 0; i < 5; i++)
            {
                Request req = new Request
                {
                    value = i
                };
                _queue.Add(req);
            }

            Assert.AreEqual(5, _queue.getCountRequests());

            for (int i = 0; i < 3; i++)
            {
                Consumer cons = new Consumer(ref _queue, i);
                consumers.Add(cons);
                cons.Start();
            }
            Thread.Sleep(4000);


            Assert.AreEqual(0, _queue.getCountRequests());

        }
    }
}

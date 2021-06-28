using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task_1
{
    public class Request
    {
        public int value;
        private static object myRequest = new object();
        private static List<Request> queue = new List<Request>();

        public Request()
        {
            this.value = generateValue();
        }
        private int generateValue()
        {
            int seed = (int)DateTime.Now.Ticks;
            Random rnd = new Random(seed);
            return rnd.Next(0, 10);
        }
    }

    public class myRequests
    {
        private List<Request> myList = new List<Request>();
        public void Add(Request req)
        {
            this.myList.Add(req);
        }
        public Request Take()
        {
            if (this.myList.Count > 0)
            {
                Request temp = this.myList.First();
                this.myList.Remove(temp);
                return temp;
            }
            else return null;
        }
        public void getList()
        {
            foreach (Request i in myList)
            {
                Console.Write(i.value + " | ");
            }
            Console.Write("\n");
        }
        public int getCountRequests()
        {
            return this.myList.Count;
        }
    }

}

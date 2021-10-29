using Microsoft.Identity.Client;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RequestHandlerAPI
{
    public class ItemsData : IData
    {
        public ConcurrentBag<Item> ItemsList { get; set; } = new ConcurrentBag<Item>();
        private ManualResetEvent _resetEvent = new ManualResetEvent(false);

        public void Add(Item item)
        {
            lock (_resetEvent)
            {
                ItemsList.Add(item);
            }
        }

        public void TaskRun()
        {
            _resetEvent.WaitOne();
            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(5000);
                Get();
            });
            _resetEvent.Set();
        }

        public ConcurrentBag<Item> Get()
        {
            return ItemsList;
        }
    }
}

using Microsoft.Identity.Client;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RequestHandlerAPI
{
    public class ItemsData : IData
    {
        public ConcurrentBag<Item> ItemsList { get; set; } = new ConcurrentBag<Item>();
        private ManualResetEvent _resetEvent = new ManualResetEvent(true);
        private bool TaskExecuted = false;

        public void Add(Item item)
        {
            _resetEvent.WaitOne();
            ItemsList.Add(item);
        }
        public int counter { get; set; } = 0;
        public void TaskRun()
        {
            if (TaskExecuted)
            {
                return;
            }

            File.WriteAllText("Result12345.txt", $"I deleted all lines {counter}");
            Task.Factory.StartNew(() =>
            {
                TaskExecuted = true;

                Thread.Sleep(5000);
                _resetEvent.Reset();
                ItemsList.Clear();

                Thread.Sleep(7000);
                _resetEvent.Set();
            });
        }

        public ConcurrentBag<Item> Get()
        {
            return ItemsList;
        }
    }
}

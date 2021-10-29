using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RequestHandlerAPI
{
    public interface IData
    {
        void Add(Item item);

        ConcurrentBag<Item> Get();

        void TaskRun();
    }
}

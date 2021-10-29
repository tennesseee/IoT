using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace RequestHandlerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemsController : ControllerBase
    {
        private IData _data;


        public ItemsController(IData data)
        {
            _data = data;
            _data.TaskRun();

        }

        [HttpPost]
        [Route("Post")]
        public void Post(Item item)
        {
            _data.Add(item);
            
        }

        [HttpGet]
        [Route("Get")]
        public ConcurrentBag<Item> Get()
        {
            return _data.Get();
        }

        //[HttpDelete]
    }
}

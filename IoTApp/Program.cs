using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IoTApp
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            RunAsync();
            while (true)
            {
                Console.WriteLine("ATTEMPT TO READ");
                Thread.Sleep(1000);
                Get();
            }
        }

        private static ConcurrentBag<Item> GetItems()
        {
            var action = $"/Items/Get";
            var request =
                client.GetAsync(action);

            var response =
                request.Result.Content.
                ReadAsAsync<ConcurrentBag<Item>>();

            return response.Result;
        }

        static void Get()
        {
            //client.BaseAddress = new Uri("https://localhost:44310/");
            //client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(
            //    new MediaTypeWithQualityHeaderValue("aplication/json"));

            try
            {
                // read
                Console.WriteLine("List of items:");
                ConcurrentBag<Item> items = GetItems();
                //foreach (var item in items)
                //{
                //    Console.WriteLine(item.ToString());
                //}

                Console.WriteLine(items.Count());
            }

            catch (Exception)
            {

                throw;
            }
        }

        static async Task RunAsync()
        {
            client.BaseAddress = new Uri("https://localhost:44310/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("aplication/json"));

            try
            {
                for (int i = 0; i < 5000; i++)
                {
                    Item item = new Item(i, $"{i} element", i * i);
                    var url = await CreateItemAsync(item);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex); ;
            }
        }

        static async Task<Uri> CreateItemAsync(Item item)
        {
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync("Items/Post", item);
            responseMessage.EnsureSuccessStatusCode();

            return responseMessage.Headers.Location;
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MODotNetCore.ConsoleAppHttpClient
{
    internal class HttpClientExample
    {
        private readonly HttpClient _client = new HttpClient() { BaseAddress = new Uri("https://localhost:7043") };
        private readonly string _blogEndPoint = "api/blog";
        public async Task RunAsync()
        {
            //await ReadAsync();
            //await EditAsync(1);
            //await EditAsync(200);
            //await CreateAsync("Async Title", "Async Author", "Async Content");
            //await UpdateAsync(2015, "Title", "Author", "Content");
            //await UpdateAsync(202215, "Async", "Async", "Async");
            await PatchAsync(2015, "Title Async", "", "Content");
            await PatchAsync(202215, "Async", "Async", "Async");
            Console.ReadKey();
            await ReadAsync();
        }

        public async Task ReadAsync()
        {
            var response = await _client.GetAsync(_blogEndPoint);
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                //Console.WriteLine(jsonStr);
                List<BlogDto> lst = JsonConvert.DeserializeObject<List<BlogDto>>(jsonStr)!;
                foreach (var blog in lst)
                {
                    ConsoleOutput(blog);
                }
            }
        }

        public async Task EditAsync(int id)
        {
            var response = await _client.GetAsync($"{_blogEndPoint}/{id}");
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                //Console.WriteLine(jsonStr);
                BlogDto item = JsonConvert.DeserializeObject<BlogDto>(jsonStr)!;
                ConsoleOutput(item);
            }
            else
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }
        }

        public async Task CreateAsync(string title, string author, string content)
        {
            // C# Object
            BlogDto blog = new BlogDto()
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };

            // C# Object to JSON
            string blogJson = JsonConvert.SerializeObject(blog);

            HttpContent httpContent = new StringContent(blogJson, Encoding.UTF8, Application.Json);
            var response = await _client.PostAsync($"{_blogEndPoint}",httpContent);
            if (response.IsSuccessStatusCode)
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
                // Implement Other Processes
            }
            else
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }
        }

        public async Task UpdateAsync(int id, string title, string author, string content)
        {
            // C# Object
            BlogDto blog = new BlogDto()
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };

            // C# Object to JSON
            string blogJson = JsonConvert.SerializeObject(blog);

            HttpContent httpContent = new StringContent(blogJson, Encoding.UTF8, Application.Json);
            var response = await _client.PutAsync($"{_blogEndPoint}/{id}", httpContent);
            if (response.IsSuccessStatusCode)
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
                // Implement Other Processes
            }
            else
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }
        }
        
        public async Task PatchAsync(int id, string? title, string? author, string? content)
        {
            // C# Object
            BlogDto blog = new BlogDto()
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };

            // C# Object to JSON
            string blogJson = JsonConvert.SerializeObject(blog);

            HttpContent httpContent = new StringContent(blogJson, Encoding.UTF8, Application.Json);
            var response = await _client.PatchAsync($"{_blogEndPoint}/{id}", httpContent);
            if (response.IsSuccessStatusCode)
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
                // Implement Other Processes
            }
            else
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _client.DeleteAsync($"{_blogEndPoint}/{id}");
            if (response.IsSuccessStatusCode)
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
                // Implement Other Processes
            }
            else
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }
        }

        private static void ConsoleOutput(BlogDto item)
        {
            Console.WriteLine("--------------------------------------");
            Console.WriteLine(JsonConvert.SerializeObject(item));
            Console.WriteLine("--------------------------------------");
            Console.WriteLine($"Id => {item.BlogId}");
            Console.WriteLine($"Title => {item.BlogTitle}");
            Console.WriteLine($"Author => {item.BlogAuthor}");
            Console.WriteLine($"Content => {item.BlogContent}");
        }
    }
}

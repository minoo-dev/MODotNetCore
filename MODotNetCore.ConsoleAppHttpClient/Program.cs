using MODotNetCore.ConsoleAppHttpClient;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

Console.WriteLine("Hello, World!");

//JsonToObjectExample _jsonToObjectExample = new JsonToObjectExample();
//await _jsonToObjectExample.GetJsonDataAsync();

//HttpClient client = new HttpClient();
//var response = await client.GetAsync("https://localhost:7043/api/blog");
//if (response.IsSuccessStatusCode)
//{
//    string jsonStr = await response.Content.ReadAsStringAsync();
//    //Console.WriteLine(jsonStr);
//    List<BlogDto> lst = JsonConvert.DeserializeObject<List<BlogDto>>(jsonStr)!;
//    foreach (var blog in lst)
//    {
//        Console.WriteLine(JsonConvert.SerializeObject(blog));
//        Console.WriteLine($"Id => {blog.BlogId}");
//        Console.WriteLine($"Title => {blog.BlogTitle}");
//        Console.WriteLine($"Author => {blog.BlogAuthor}");
//        Console.WriteLine($"Content => {blog.BlogContent}");
//    }
//}

HttpClientExample httpClientExample = new HttpClientExample();
await httpClientExample.RunAsync();

Console.ReadLine();

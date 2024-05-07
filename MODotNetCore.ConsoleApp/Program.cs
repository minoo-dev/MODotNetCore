// See https://aka.ms/new-console-template for more information
using MODotNetCore.ConsoleApp.AdoDotNetExamples;
using MODotNetCore.ConsoleApp.EFCoreExamples;
using System.Data;
using System.Data.SqlClient;

Console.WriteLine("Hello, World!");

AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
//adoDotNetExample.Read();
//adoDotNetExample.Create("title", "author", "content");
//adoDotNetExample.Update(12, "title 1", "author 1", "content 1");
//adoDotNetExample.Delete(12);
//adoDotNetExample.Edit(12);
//adoDotNetExample.Edit(11);

//DapperExample dapperExample = new DapperExample();
//dapperExample.Run();

EFcoreExample EFcoreExample = new EFcoreExample();
EFcoreExample.Run();

Console.ReadKey();

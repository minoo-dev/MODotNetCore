# DotNet Training Batch 4 Project

## Project Structure
- MODotNetCore
	- MODotNetCore.ConsoleApp
		- BlogDto
		- AdoDotNetExamples
		- DapperExamples
		- EFCoreExamples
	- MODotNetCore.RestApi
		- Models
			- BlogModel
		- Controller
			- BlogController `EFCore`
			- BlogAdoDotNetController
			- BlogAdoDotNetServiceController
			- BlogDapperController
			- BlogDapperServiceController
	- MODotNetCore.RestApiWithNLayer
		- Models
			- BlogModel
		- Features
			- Blog
				- BL_Blog
				- DA_Blog
				- BlogController `EFCore`
	- MODotNetCore.shared
		- DapperService
		- AdoDotNetService

## Timeline
2024-04-09 => Console App  
2024-04-10 => ADO.Net CRUD  
2024-04-22 => Dapper CRUD  
2024-04-23 => EFCore CRUD  
2024-04-23 => ASP.Net Core Web API + EFCore CRUD  
2024-04-29 => ASP.Net Core Web API + Dapper CRUD  
2024-05-01 => ASP.Net Core Web API + Ado.Net CRUD  
2024-05-01 => Dapper Custom Service  
2024-05-01 => ADO.Net Custom Service  
2024-05-07 => N-Layer Architecture (ASP.Net Core Web API + EFCore)  

## Questions/Reminders
- [ ] Q : How to handle possible Null return value? (ADO.Net/Dapper Custom Service) `ref 2024-05-01 Video`
- [ ] R : BlogResponseModel Return Type in Data Access Layer (Remind Ko Lynn) `ref 2024-05-07 N-Layer Video 14:00 mins`
	
## Notable Steps
1. Create New Project > Template > Console App ****Please Note** > *Not Console App (.Net Framework )*
2. Fill Project Information  
*[Name Convention : NameInitialLetter+DotNetCore e.g Min Oo > MODotNetCore]*  
	Project Name > MODotNetCore.ConsoleApp  
	Solution Name > MODotNetCore  
	Location > [desired code location folder]
3. Next > Framework > .NET 7.0 > Click "Create"
4. Click "Add to source control"
	- Add GitHub Account if not connected before
	- Set Repository to "Public"
	- Click "Create & Push"
5. Check your git hub email in Visual Studio
	- Tool > Options > Search "git" > Source Control > Git Repository Settings > General
	- Check User name & Email whether correct GitHub Information is used
6. Right click Solution Name > New Solution Folder > Name : "Documents"
7. Right click "Documents" Folder > Add > New Item > Name : "README.md"
8. Open SQL Server Management Studio > Connect with sa account  
   [user name: sa / password : sasa@123]
9. SQL Client  
	- Right click "ConsoleApp" project name > Manage NuGet Packages
	- search "sqlclient"
	- select "System.Data.SqlClient" > Install
10. Create New Database => "DotNetTrainingBatch4"
11. Create New Table => "Tbl_Blog"  
	- BlogId (int) Primary Identity 1,1
	- BlogTitle (varchar(50)),
	- BlogAuthor (varchar(50)),
	- BlogContent (varchar(50))
12. Create `AdoDotNetExample.cs` 
	- => Implement `Read(), Create(), Edit(), Update(), Delete()`
13. ORM => Dapper  
	- Right click "ConsoleApp" project name > Manage NuGet Packages
	- search "dapper"
	- select "Dapper" > Install
14. Add `BlogDto.cs`
15. Add `ConnectionStrings.cs` 
16. Add `DapperExample.cs` 
	- => Implement `Read(), Create(), Edit(), Update(), Delete()`
17.	ORM => EFCore
	- Right click "ConsoleApp" Project Name > Manage NuGet Packages
	- search "entityframework"
	- select "Microsoft.EntityFrameworkCore" > change version "7.xx" > Install
18. EF Core DB Provider
	- Right click "ConsoleApp" Project Name > Manage NuGet Packages
	- search "entityframework"	
	- select "Microsoft.EntityFrameworkCore.SqlServer" > change version "7.xx" > Install
19. Add `AppDbContext.cs` 
	- `DbSet<BlogDto> Blogs`
	- override `onConfiguring`
20. Update `BlogDto.cs` > `[Table("Tbl_Blog")]` `[key]`
21. Add `EFCoreExample.cs` 
	- => Implement `Read(), Create(), Edit(), Update(), Delete()`
22. Console App Folder Restructure
23. Web API (RestApi)
	- Add Web Core Api Project `MODotNetCore.RestApi`
	- Remove Any files related to `Weather`
	- Right Click `Controller` > Add New API Controller `BlogController`
	- Implement skeleton methods for `[HttpGet], [HttpPost], [HttpPut], [HttpPatch], [HttpDelete]`
24. Set up EFCore
	- Add Packages `EntityFrameworkCore` & `EntityFrameworkCore.SqlServer`
	- Add DBContext `AppDbContext.cs` (Copy From ConsoleApp)
	- Add Model `BlogModel.cs` (Copy `BlogDto.cs` Then Rename)
	- Add `ConnectionStrings.cs`
25. Detail Implement BlogController 
	- Optional Model Data : Add ? after data type in Blog Model e.g `public string? BlogTitle`
26. Web API (RestApi) + Dapper
	- Install `Dapper`
	- Add New API Controller `Controller` > `BlogDapperController`
	- Implement methods for `[HttpGet], [HttpPost], [HttpPut], [HttpPatch], [HttpDelete]`
27. Web API (RestApi) + Ado.Net
	- Add New API Controller `Controller` > `BlogAdoDotNetController`
	- Use `System.Data.SqlClient`
	- Implement methods for `[HttpGet], [HttpPost]`
	- **Homework** : Implement methods for `[HttpPut], [HttpPatch], [HttpDelete]`
28. Add Custom Service shared Project `MODotNetCore.Shared`
	- Reference it in `MODotNetCore.RestApi`
29. Dapper Custom Service
	- Implement `DapperService.cs` in `MODotNetCore.shared`
	- Implement `BlogDapperServiceController` in `MODotNetCore.RestApi`
30. Install `Newtonsoft.Json` package in `MODotNetCore.Shared`
	- To Convert **Data Table** > **Json** > **List**
30. ADO.Net Custom Service
	- Implement `AdoDotNetService.cs` in `MODotNetCore.shared`
	- Implement `BlogAdoDotNetServiceController` in `MODotNetCore.RestApi`
31. Add RestApi with N-Layer Project `MODotNetCore.RestApiWithNLayer` (User Interface, Business Logic, Data Access)
	- Add DBContext `Db/AppDbContext.cs` (Copy From MODotNetCore.RestApi)
	- Add Model `Models/BlogModel.cs` (Copy From MODotNetCore.RestApi)
	- Implement `Features/Blog/BL_Blog.cs` ** Business Logic Layer**
	- Implement `Features/Blog/DA_Blog.cs` ** Data Access Layer**
	- Implement `Features/Blog/BlogController.cs` **User Interface Layer**
	- **Homework** : Implement methods for **PatchBlog** `BlogController, BL_Blog, DA_Blog`

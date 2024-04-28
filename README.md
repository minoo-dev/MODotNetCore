# DotNet Training Batch 4 Project

## Project Structure
- MODotNetCore
	- MODotNetCore.ConsoleApp
		- BlogDto
		- AdoDotNetExamples
		- DapperExamples
	- MODotNetCore.RestApi

## Timeline
2024-04-09 => Console App  
2024-04-10 => ADO.Net CRUD  
2024-04-22 => Dapper CRUD  
2024-04-23 => EFCore CRUD  

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
12. Add `ConnectionStrings.cs` 
12. Ad `DapperNetExample.cs` 
	- => Implement `Read(), Create(), Edit(), Update(), Delete()`

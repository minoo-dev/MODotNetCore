1. Create New Project > Template > Console App * Not  Console App (.Net Framework )
2. Fill Project Information [Name Convention : NameInitialLetter+DotNetCore e.g Min Oo > MODotNetCore]
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
8. Open SQL Server Management Studio > Connect with sa account [user name: sa / password : sasa@123]
9. Right click "ConsoleApp" project name > Manage NuGet Packages
	- search "sqlclient"
	- select "System.Data.SqlClient" > Install
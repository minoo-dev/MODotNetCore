1. Create New Project > Template > Console App ****Please Note** > *Not Console App (.Net Framework )*
3. Fill Project Information [Name Convention : NameInitialLetter+DotNetCore e.g Min Oo > MODotNetCore]
	Project Name > MODotNetCore.ConsoleApp
	Solution Name > MODotNetCore
	Location > [desired code location folder]
4. Next > Framework > .NET 7.0 > Click "Create"
5. Click "Add to source control"
	- Add GitHub Account if not connected before
	- Set Repository to "Public"
	- Click "Create & Push"
6. Check your git hub email in Visual Studio
	- Tool > Options > Search "git" > Source Control > Git Repository Settings > General
	- Check User name & Email whether correct GitHub Information is used
7. Right click Solution Name > New Solution Folder > Name : "Documents"
8. Right click "Documents" Folder > Add > New Item > Name : "README.md"
9. Open SQL Server Management Studio > Connect with sa account [user name: sa / password : sasa@123]
10. Right click "ConsoleApp" project name > Manage NuGet Packages
	- search "sqlclient"
	- select "System.Data.SqlClient" > Install

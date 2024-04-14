using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODotNetCore.ConsoleApp
{
    internal class AdoDotNetExample
    {
        public void Read()
        {
            SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder();
            stringBuilder.DataSource = "DESKTOP-XPC"; // Server Name
            stringBuilder.InitialCatalog = "DotNetTrainingBatch4";
            stringBuilder.UserID = "sa";
            stringBuilder.Password = "sasa@123";
            SqlConnection connection = new SqlConnection(stringBuilder.ConnectionString);
            connection.Open();
            Console.WriteLine("Connection open...");

            string query = "select * from tbl_blog";
            SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = query;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            connection.Close();
            Console.WriteLine("Connection close...");

            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine("Blog Id => " + row["BlogId"]);
                Console.WriteLine("Blog Title => " + row["BlogTitle"]);
                Console.WriteLine("Blog Author => " + row["BlogAuthor"]);
                Console.WriteLine("Blog Content => " + row["BlogContent"]);
                Console.WriteLine("------------------------------------");
            }
        }
    }
}

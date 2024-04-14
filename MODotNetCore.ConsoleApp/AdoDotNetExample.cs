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
        private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = "DESKTOP-XPC",
            InitialCatalog = "DotNetTrainingBatch4",
            UserID = "sa",
            Password = "sasa@123"
            
        };

        public void Read()
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
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

        public void Edit(int id)
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            Console.WriteLine("Connection open...");

            string query = "select * from tbl_blog where BlogId = @BlogId";
            SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = query;
            sqlCommand.Parameters.AddWithValue("@BlogId", id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            connection.Close();
            Console.WriteLine("Connection close...");

            if (dataTable.Rows.Count == 0 )
            {
                Console.WriteLine("No data found.");
                return;
            }
            
            DataRow row = dataTable.Rows[0];
            Console.WriteLine("Blog Id => " + row["BlogId"]);
            Console.WriteLine("Blog Title => " + row["BlogTitle"]);
            Console.WriteLine("Blog Author => " + row["BlogAuthor"]);
            Console.WriteLine("Blog Content => " + row["BlogContent"]);
            Console.WriteLine("------------------------------------");
        }

        public void Create(string title, string authour, string content)
        {
            SqlConnection sqlConnection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            sqlConnection.Open();

            string query = @"INSERT INTO [dbo].[Tbl_Blog]
            ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
            VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent)";
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@BlogTitle", title);
            sqlCommand.Parameters.AddWithValue("@BlogAuthor", authour);
            sqlCommand.Parameters.AddWithValue("@BlogContent", content);
            int result = sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();

            string message = result > 0 ? "Saving Successful." : "Saving Failed.";
            Console.WriteLine(message);
        }

        public void Update (int id, string title, string authour, string content)
        {
            SqlConnection sqlConnection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            sqlConnection.Open();

            string query = @"UPDATE [dbo].[Tbl_Blog]
            SET [BlogTitle] = @BlogTitle
            ,[BlogAuthor] = @BlogAuthor
            ,[BlogContent] = @BlogContent
            WHERE BlogId = @BlogId";
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@BlogId", id);
            sqlCommand.Parameters.AddWithValue("@BlogTitle", title);
            sqlCommand.Parameters.AddWithValue("@BlogAuthor", authour);
            sqlCommand.Parameters.AddWithValue("@BlogContent", content);
            int result = sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();

            string message = result > 0 ? "Update Successful." : "Update Failed.";
            Console.WriteLine(message);
        }

        public void Delete(int id)
        {
            SqlConnection sqlConnection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            sqlConnection.Open();

            string query = @"DELETE FROM [dbo].[Tbl_Blog]
            WHERE BlogId = @BlogId";
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@BlogId", id);
            int result = sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();

            string message = result > 0 ? "Delete Successful." : "Delete Failed.";
            Console.WriteLine(message);
        }
    }
}

﻿using System;
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

            string message = result > 0 ? "Saving Successful." : "Saving Failded.";
            Console.WriteLine(message);
        }
    }
}

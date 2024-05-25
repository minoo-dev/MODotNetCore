using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MODotNetCore.Shared
{
    public class AdoDotNetService
    {
        private readonly string _connectionString;

        public AdoDotNetService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<T> Query<T>(string query, params AdoDotNetParameter[]? parameters)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            SqlCommand sqlCommand = new SqlCommand(query, connection);
            if (parameters is not null && parameters.Length > 0) 
            {
                //foreach (var item in parameters)
                //{
                //    sqlCommand.Parameters.AddWithValue(item.Name, item.Value);
                //}

                //sqlCommand.Parameters.AddRange(parameters.Select(item => new SqlParameter(item.Name, item.Value)).ToArray());

                var parametersArray = parameters.Select(item => new SqlParameter(item.Name, item.Value)).ToArray();
                sqlCommand.Parameters.AddRange(parametersArray);
            }
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            connection.Close();

            string json = JsonConvert.SerializeObject(dataTable); // C# > JSON
            List<T> lst = JsonConvert.DeserializeObject<List<T>>(json)!; // JSON > C#

            return lst;
        }
        public T QueryFirstOrDefault<T>(string query, params AdoDotNetParameter[]? parameters)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            SqlCommand sqlCommand = new SqlCommand(query, connection);
            if (parameters is not null && parameters.Length > 0)
            {
                //foreach (var item in parameters)
                //{
                //    sqlCommand.Parameters.AddWithValue(item.Name, item.Value);
                //}

                //sqlCommand.Parameters.AddRange(parameters.Select(item => new SqlParameter(item.Name, item.Value)).ToArray());

                var parametersArray = parameters.Select(item => new SqlParameter(item.Name, item.Value)).ToArray();
                sqlCommand.Parameters.AddRange(parametersArray);
            }
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            connection.Close();

            string json = JsonConvert.SerializeObject(dataTable); // C# > JSON
            List<T> lst = JsonConvert.DeserializeObject<List<T>>(json)!; // JSON > C#

            return lst.Count > 0 ? lst[0] : default(T);
        }
        public int Execute(string query, params AdoDotNetParameter[]? parameters)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            SqlCommand sqlCommand = new SqlCommand(query, connection);
            if (parameters is not null && parameters.Length > 0)
            {
                sqlCommand.Parameters.AddRange(parameters.Select(item => new SqlParameter(item.Name, item.Value)).ToArray());
            }
            var result = sqlCommand.ExecuteNonQuery();

            connection.Close();

            return result;
        }
    }
}

public class AdoDotNetParameter
    {
        public AdoDotNetParameter(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }
        public object Value { get; set; }
    }

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MODotNetCore.RestApi.Models;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata;

namespace MODotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoDotNetController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetBlogs()
        {
            string query = "select * from tbl_blog";
            SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            SqlCommand sqlCommand = new SqlCommand(query, connection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            connection.Close();

            //List<BlogModel> lst = new List<BlogModel>();
            //foreach (DataRow dr in dataTable.Rows)
            //{
            //    BlogModel blog = new BlogModel
            //    {
            //        BlogId = Convert.ToInt32(dr["BlogId"]),
            //        BlogTitle = Convert.ToString(dr["BlogTitle"]),
            //        BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
            //        BlogContent = Convert.ToString(dr["BlogContent"])
            //    };
            //    lst.Add( blog );
            //}

            List<BlogModel> lst = dataTable.AsEnumerable().Select(dr => new BlogModel
            {
                BlogId = Convert.ToInt32(dr["BlogId"]),
                BlogTitle = Convert.ToString(dr["BlogTitle"]),
                BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
                BlogContent = Convert.ToString(dr["BlogContent"])
            }).ToList();

            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            string query = "select * from tbl_blog where BlogId = @BlogId";
            SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            SqlCommand sqlCommand = new SqlCommand(query, connection);
            sqlCommand.Parameters.AddWithValue("@BlogId", id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            connection.Close();

            if (dataTable.Rows.Count == 0)
            {
                return NotFound("No data found.");
            }

            DataRow dr = dataTable.Rows[0];

            var item = new BlogModel
            {
                BlogId = Convert.ToInt32(dr["BlogId"]),
                BlogTitle = Convert.ToString(dr["BlogTitle"]),
                BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
                BlogContent = Convert.ToString(dr["BlogContent"])
            };

            return Ok(item);
        }

        [HttpPost]
        public IActionResult CreateBlog(BlogModel blog)
        {
            string query = @"INSERT INTO [dbo].[Tbl_Blog]
            ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
            VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent)";

            SqlConnection sqlConnection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
            sqlCommand.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
            sqlCommand.Parameters.AddWithValue("@BlogContent", blog.BlogContent);
            int result = sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();

            string message = result > 0 ? "Saving Successful." : "Saving Failed.";
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogModel blog)
        {
            if (!ValidateBlogId(id))
            {
                return NotFound("No data found.");
            }

            string query = @"UPDATE [dbo].[Tbl_Blog]
            SET [BlogTitle] = @BlogTitle
            ,[BlogAuthor] = @BlogAuthor
            ,[BlogContent] = @BlogContent
            WHERE BlogId = @BlogId";

            SqlConnection sqlConnection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@BlogId", id);
            sqlCommand.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
            sqlCommand.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
            sqlCommand.Parameters.AddWithValue("@BlogContent", blog.BlogContent);
            int result = sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();

            string message = result > 0 ? "Update Successful." : "Update Failed.";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogModel blog)
        {
            if (!ValidateBlogId(id))
            {
                return NotFound("No data found.");
            }

            List<string> conditionList = new List<string>();
            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                conditionList.Add("[BlogTitle] = @BlogTitle");
            }
            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                conditionList.Add("[BlogAuthor] = @BlogAuthor");
            }
            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                conditionList.Add("[BlogContent] = @BlogContent");
            }
            if (conditionList.Count == 0)
            {
                return NotFound("No data to update.");
            }

            string conditions = string.Join(", ",conditionList);

            string query = $@"UPDATE [dbo].[Tbl_Blog]
            SET {conditions}
            WHERE BlogId = @BlogId";

            SqlConnection sqlConnection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@BlogId", id);
            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                sqlCommand.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
            }
            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                sqlCommand.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
            }
            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                sqlCommand.Parameters.AddWithValue("@BlogContent", blog.BlogContent);
            }
            int result = sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();

            string message = result > 0 ? "Update Successful." : "Update Failed.";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            if (!ValidateBlogId(id))
            {
                return NotFound("No data found.");
            }

            string query = @"DELETE FROM [dbo].[Tbl_Blog]
            WHERE BlogId = @BlogId";

            SqlConnection sqlConnection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@BlogId", id);
            int result = sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();

            string message = result > 0 ? "Deleting Successful." : "Deleting Failed.";
            return Ok(message);
        }

        private bool ValidateBlogId(int id)
        {
            string query = "select count(*) from tbl_blog where BlogId = @BlogId";
            SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            SqlCommand sqlCommand = new SqlCommand(query, connection);
            sqlCommand.Parameters.AddWithValue("@BlogId", id);
            int result = (int)sqlCommand.ExecuteScalar();

            connection.Close();
            return result > 0;
        }
    }
}

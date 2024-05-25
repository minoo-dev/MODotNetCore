using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MODotNetCore.RestApi.Models;
using MODotNetCore.Shared;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata;

namespace MODotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoDotNetServiceController : ControllerBase
    {
        private readonly AdoDotNetService _adoDotNetService = new AdoDotNetService(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);

        [HttpGet]
        public IActionResult GetBlogs()
        {
            string query = "select * from tbl_blog";
            var lst = _adoDotNetService.Query<BlogModel>(query);

            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            string query = "select * from tbl_blog where BlogId = @BlogId";

            //AdoDotNetParameter[] parameters = new AdoDotNetParameter[1];
            //parameters[0] = new AdoDotNetParameter("@BlogId", id);
            //var lst = _adoDotNetService.Query<BlogModel>(query);

            var item = _adoDotNetService.QueryFirstOrDefault<BlogModel>(query, new AdoDotNetParameter("@BlogId", id));

            if (item is null)
            {
                return NotFound("No data found.");
            }

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

            int result = _adoDotNetService.Execute(query,
                new AdoDotNetParameter("@BlogTitle", blog.BlogTitle),
                new AdoDotNetParameter("@BlogAuthor", blog.BlogAuthor),
                new AdoDotNetParameter("@BlogContent", blog.BlogContent)
                );

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

            int result = _adoDotNetService.Execute(query,
                new AdoDotNetParameter("@BlogId", id),
                new AdoDotNetParameter("@BlogTitle", blog.BlogTitle),
                new AdoDotNetParameter("@BlogAuthor", blog.BlogAuthor),
                new AdoDotNetParameter("@BlogContent", blog.BlogContent)
                );

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
            List<AdoDotNetParameter> parameters = new List<AdoDotNetParameter>();
            parameters.Add(new AdoDotNetParameter("@BlogId", id));

            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                conditionList.Add("[BlogTitle] = @BlogTitle");
                parameters.Add(new AdoDotNetParameter("@BlogTitle", blog.BlogTitle));
            }
            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                conditionList.Add("[BlogAuthor] = @BlogAuthor");
                parameters.Add(new AdoDotNetParameter("@BlogAuthor", blog.BlogAuthor));
            }
            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                conditionList.Add("[BlogContent] = @BlogContent");
                parameters.Add(new AdoDotNetParameter("@BlogContent", blog.BlogContent));
            }
            if (conditionList.Count == 0)
            {
                return NotFound("No data to update.");
            }

            string conditions = string.Join(", ",conditionList);

            string query = $@"UPDATE [dbo].[Tbl_Blog]
            SET {conditions}
            WHERE BlogId = @BlogId";

            int result = _adoDotNetService.Execute(query, parameters.ToArray());

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

            int result = _adoDotNetService.Execute(query, new AdoDotNetParameter("@BlogId", id));

            string message = result > 0 ? "Deleting Successful." : "Deleting Failed.";
            return Ok(message);
        }

        private bool ValidateBlogId(int id)
        {
            string query = "select * from tbl_blog where BlogId = @BlogId";
            var item = _adoDotNetService.QueryFirstOrDefault<BlogModel>(query, new AdoDotNetParameter("@BlogId", id));

            return item != null;
        }
    }
}

using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
namespace EFCoreTest.Normal
{
    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }

        //public List<Post> Posts { get; set; }
    }
    public class BlogService
    {
        private BloggingContext _context;

        public BlogService(BloggingContext context)
        {
            _context = context;
        }

        public void Add(string url)
        {
            var blog = new Blog { Url = url };
            _context.Blogs.Add(blog);
            _context.SaveChanges();
        }

        public IEnumerable<Blog> Find(string term)
        {
            return _context.Blogs
                .Where(b => b.Url.Contains(term))
                .OrderBy(b => b.Url)
                .ToList();
        }
    }

    public class BloggingContext : DbContext
    {
        public BloggingContext()
        {
        }

        public BloggingContext(DbContextOptions<BloggingContext> options)
            : base(options)
        {
        }
        public DbSet<Blog> Blogs { get; set; }
    }
    [TestClass]
    public class NormalTest
    {
        [TestMethod]
        public void Add_writes_to_database()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<BloggingContext>()
                    .UseSqlite(connection)
                    .Options;
                // Create the schema in the database
                using (var context = new BloggingContext(options))
                {
                    context.Database.EnsureCreated();
                }

                // Run the test against one instance of the context
                using (var context = new BloggingContext(options))
                {
                    var service = new BlogService(context);
                    service.Add("http://sample.com");
                }

                // Use a separate instance of the context to verify correct data was saved to database
                using (var context = new BloggingContext(options))
                {
                    Assert.AreEqual(1, context.Blogs.Count());
                    Assert.AreEqual("http://sample.com", context.Blogs.Single().Url);
                }
            }
            finally
            {
                connection.Close();
            }
        }
    }
}

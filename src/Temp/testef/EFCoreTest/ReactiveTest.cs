using EntityFrameworkCore.Rx;
using EntityFrameworkCore.Triggers;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;

namespace EFCoreTest.Reactive
{
    public abstract class Trackable
    {
        public DateTime Inserted { get; protected set; }
        public DateTime Updated { get; protected set; }

        static Trackable()
        {
            Triggers<Trackable>.Inserting += e => e.Entity.Inserted = e.Entity.Updated = DateTime.UtcNow;
            Triggers<Trackable>.Updating += e => e.Entity.Updated = DateTime.UtcNow;
        }
    }

    public class Blog : Trackable
    {
        public int BlogId { get; set; }
        public string Url { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        //public List<Post> Posts { get; set; }
        public override string ToString() => $"{this.Url} - inserted at:{this.Inserted}";
    }

    public class BloggingContext : DbContextWithTriggers
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
    public class BlogService
    {
        private BloggingContext _context;

        public BlogService(BloggingContext context)
        {
            _context = context;
        }

        public void Add(string url)
        {
            var blog = new Blog { Url = url, DateOfBirth = DateTime.Today };

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
    [TestClass]
    public class ReactiveTest
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

                var observerThread = new Thread(ObserveNewBlog);
                observerThread.Start();
                Thread.Sleep(100);

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

        private static void ObserveNewBlog()
        {
            Console.WriteLine("thread");
            var o = DbObservable<BloggingContext>.FromInserted<Blog>();
            o.Where(x => x.Entity.DateOfBirth.Month == DateTime.Today.Month && x.Entity.DateOfBirth.Day == DateTime.Today.Day)
             .Subscribe(entry => Console.WriteLine($"Happy birthday to {entry.Entity.Url}!"));
           
            o.Subscribe(entry => Console.WriteLine(entry.Entity));

            Thread.Sleep(500);
        }
    }
}

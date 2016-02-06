using BookWebMVC.Data.Model;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;

namespace BookWebMVC.Data.Core
{
    public class BookWebContext : IdentityDbContext<BookWebUser>
    {
        public BookWebContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // TODO move to config file
            var connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=BooksDB;Trusted_Connection=true;";
            optionsBuilder.UseSqlServer(connectionString);

            base.OnConfiguring(optionsBuilder);
        }
    }
}

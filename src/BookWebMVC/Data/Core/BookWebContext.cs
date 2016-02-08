using BookWebMVC.Data.Model;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Extensions.OptionsModel;

namespace BookWebMVC.Data.Core
{
    public class BookWebContext : IdentityDbContext<BookWebUser>
    {
        private readonly string _connectionString;

        public BookWebContext(IOptions<ConnectionString> options)
        {
            _connectionString = options.Value.ConnectionStringDefault;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);

            base.OnConfiguring(optionsBuilder);
        }
    }
}

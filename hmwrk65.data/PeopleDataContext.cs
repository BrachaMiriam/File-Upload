using Microsoft.EntityFrameworkCore;

namespace hmwrk65.data
{
    public class PeopleDataContext : DbContext
    {
        private string _connectionString;

        public PeopleDataContext(string connectionString)
        {
            _connectionString = connectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        public DbSet<Person> People { get; set; }
    }


}

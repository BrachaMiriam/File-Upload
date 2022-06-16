using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace hmwrk65.data
{
    public class PeopleContextFactory : IDesignTimeDbContextFactory<PeopleDataContext>
    {
        public PeopleDataContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
               .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), $"..{Path.DirectorySeparatorChar}hmwrk65.web"))
               .AddJsonFile("appsettings.json")
               .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true).Build();

            return new PeopleDataContext(config.GetConnectionString("ConStr"));
        }

    }


}

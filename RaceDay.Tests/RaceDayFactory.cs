using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RaceDay.Api.Data;

namespace RaceDay.Tests
{
    // starts the real API for the tests, but uses an in-memory database instead of SQL Server
    public class RaceDayFactory : WebApplicationFactory<Program>
    {
        private readonly string _dbName = Guid.NewGuid().ToString();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var oldOptions = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<RaceDayContext>));
                if (oldOptions != null)
                {
                    services.Remove(oldOptions);
                }

                services.AddDbContext<RaceDayContext>(options => options.UseInMemoryDatabase(_dbName));
            });
        }
    }
}

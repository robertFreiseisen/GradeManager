using System.Reflection;

using Base.Helper;

using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        /// <summary>
        /// Für InMemory-DB in UnitTests
        /// </summary>
        /// <param name="options"></param>
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = ConfigurationHelper.GetConfiguration();

                string connectionString = configuration["ConnectionStrings:DefaultConnection"];

                optionsBuilder.UseNpgsql(connectionString);
                //optionsBuilder.UseLoggerFactory(GetLoggerFactory());

            }
        }

        //static ILoggerFactory GetLoggerFactory()
        //{

        //    Log.Logger = new LoggerConfiguration(ConfigurationHelper.GetConfiguration()).

        //    var serviceProvider = new ServiceCollection()
        //        .AddLogging(configure => configure.AddSerilog())
        //        .BuildServiceProvider();

        //    return serviceProvider.GetService<ILoggerFactory>();
        //}
    }
}

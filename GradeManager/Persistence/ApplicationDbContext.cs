using System.Reflection;
using System.Runtime.CompilerServices;
using Base.Helper;
using Microsoft.EntityFrameworkCore;
using Shared.Entities;

namespace Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<GradeKey> GradeKeys => Set<GradeKey>();
        public DbSet<Grade> Grades => Set<Grade>();
        public DbSet<Teacher> Teachers => Set<Teacher>();
        public DbSet<Subject> Subjects => Set<Subject>();
        public DbSet<Student> Students => Set<Student>();      
        public DbSet<SchoolClass> SchoolClasses => Set<SchoolClass>();
        public DbSet<GradeKind> GradeKinds => Set<GradeKind>();
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
                AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
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

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Infrastructure.Data
{
    public class DaoContextFactory : IDesignTimeDbContextFactory<DaoContext>
    {
        public DaoContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<DaoContext>();
            var connectionString = "Host=localhost;Port=5432;Pooling=true;Database=postgres;User Id=postgres;Password=postgres;";
            // var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__myapp");            
            builder.UseNpgsql(connectionString, x => x.MigrationsHistoryTable("__EFMigrationsHistory", "SCA"));
            return new DaoContext(builder.Options);
        }
    }
}

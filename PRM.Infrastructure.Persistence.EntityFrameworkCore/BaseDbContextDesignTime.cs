using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PRM.Infrastructure.Persistence.EntityFrameworkCore
{
    public abstract class BaseDbContextDesignTime<TDbContext> : IDesignTimeDbContextFactory<TDbContext> where TDbContext : DbContext
    {
        private readonly string _connectionString;

        public BaseDbContextDesignTime(string connectionString, string databaseName)
        {
            _connectionString = connectionString.Replace("DATABASE_NAME", databaseName);
        }
        
        public TDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TDbContext>();
            optionsBuilder.UseMySql(_connectionString);

            return Activator.CreateInstance(typeof(TDbContext), optionsBuilder.Options) as TDbContext;
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PRM.Domain.Products;
using PRM.Domain.Renters;
using PRM.Domain.Rents;
using PRM.Infrastructure.Persistence.EntityFrameworkCore;
using PRM.Infrastructure.Persistence.EntityFrameworkCore.Products;
using PRM.Infrastructure.Persistence.EntityFrameworkCore.Renters;
using PRM.Infrastructure.Persistence.EntityFrameworkCore.Rents;

namespace PRM.Infrastructure.Persistence.MySQL
{
    public class PrmDbContext : BaseDbContext<PrmDbContext>, ICurrentDbContext
    {
        public DbContext Context { get; }

        public DbSet<Product> Products { get; set; }
        public DbSet<Rent> Rents { get; set; }
        public DbSet<Renter> Renters { get; set; }
        
        public PrmDbContext(DbContextOptions<PrmDbContext> options) : base(options)
        {
            Context = this;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.ApplyConfiguration(new RenterEntityTypeConfiguration(modelBuilder.Model.GetDefaultSchema()));
            modelBuilder.ApplyConfiguration(new RenterRentalHistoryEntityTypeConfiguration(modelBuilder.Model.GetDefaultSchema()));
            modelBuilder.ApplyConfiguration(new RentEntityTypeConfiguration(modelBuilder.Model.GetDefaultSchema()));
            modelBuilder.ApplyConfiguration(new ProductRentalHistoryEntityTypeConfiguration(modelBuilder.Model.GetDefaultSchema()));
            modelBuilder.ApplyConfiguration(new ProductEntityTypeConfiguration(modelBuilder.Model.GetDefaultSchema()));
        }
    }
    
    public class PrmDbContextDesignTime : BaseDbContextDesignTime<PrmDbContext>
    {
        public PrmDbContextDesignTime() : base(MySqlSettings.ConnectionString,"prm")
        {
        }
    }
}
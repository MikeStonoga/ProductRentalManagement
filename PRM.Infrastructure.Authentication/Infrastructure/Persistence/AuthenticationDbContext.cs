using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PRM.Infrastructure.Authentication.Users;
using PRM.Infrastructure.Persistence.EntityFrameworkCore;

namespace PRM.Infrastructure.Authentication.Infrastructure.Persistence
{
    public class AuthenticationDbContext : BaseDbContext<AuthenticationDbContext>, ICurrentDbContext
    {
        public DbContext Context { get; }

        public DbSet<User> Users { get; set; }

        public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options) : base(options)
        {
            Context = this;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasKey(user => user.Id);
            modelBuilder.Entity<User>().Property(user => user.Code).ValueGeneratedOnAdd();
            modelBuilder.Entity<User>().Property(user => user.Name).IsRequired();
            modelBuilder.Entity<User>().HasIndex(user => user.Email).IsUnique();
            modelBuilder.Entity<User>().Property(user => user.Email).IsRequired();
            modelBuilder.Entity<User>().HasIndex(user => user.Login).IsUnique();
            modelBuilder.Entity<User>().Property(user => user.Login).IsRequired();
            modelBuilder.Entity<User>().Property(user => user.Password).IsRequired();
        }
    }
    
    public class AuthenticationDbContextDesignTime : BaseDbContextDesignTime<AuthenticationDbContext>
    {
        public AuthenticationDbContextDesignTime() : base(PRM.Infrastructure.Persistence.MySQL.MySqlSettings.ConnectionString, "authentication")
        {
        }
    }
}
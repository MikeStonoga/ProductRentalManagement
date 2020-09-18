using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace PRM.Infrastructure.Persistence.EntityFrameworkCore
{
    public class BaseDbContext<TDbContext> : DbContext where TDbContext : DbContext
    {
        protected BaseDbContext(DbContextOptions<TDbContext> options) : base(options)
        {
        }
    }
}
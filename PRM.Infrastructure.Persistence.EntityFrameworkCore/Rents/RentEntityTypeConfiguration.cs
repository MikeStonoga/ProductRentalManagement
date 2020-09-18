using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRM.Domain.Rents;

namespace PRM.Infrastructure.Persistence.EntityFrameworkCore.Rents
{
    public class RentEntityTypeConfiguration : IEntityTypeConfiguration<Rent>
    {
        private readonly string _defaultSchema;

        public RentEntityTypeConfiguration(string defaultSchema)
        {
            _defaultSchema = defaultSchema;
        }

        public void Configure(EntityTypeBuilder<Rent> rentConfiguration)
        {
            rentConfiguration.ToTable("rents", _defaultSchema);
            
            rentConfiguration.HasKey(rent => rent.Id);
            rentConfiguration.Property(r => r.RenterId).IsRequired();
            rentConfiguration.Property(r => r.DailyPrice).IsRequired();
            rentConfiguration.Property(r => r.DailyLateFee).IsRequired();
            rentConfiguration.Property(r => r.RentedProductsCount).IsRequired();
            rentConfiguration.OwnsOne(r => r.RentPeriod);
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRM.Domain.Renters;
using PRM.Domain.Rents;

namespace PRM.Infrastructure.Persistence.EntityFrameworkCore.Renters
{
    public class RenterRentalHistoryEntityTypeConfiguration : IEntityTypeConfiguration<RenterRentalHistory>
    {
        private readonly string _defaultSchema;

        public RenterRentalHistoryEntityTypeConfiguration(string defaultSchema)
        {
            _defaultSchema = defaultSchema;
        }
        
        public void Configure(EntityTypeBuilder<RenterRentalHistory> renterRentalHistoryConfiguration)
        {
            renterRentalHistoryConfiguration.ToTable("renters_rental_history", _defaultSchema);
            
            renterRentalHistoryConfiguration.HasKey(history => history.Id);
            renterRentalHistoryConfiguration.HasOne<Rent>().WithMany().HasForeignKey(history => history.RentId);
            renterRentalHistoryConfiguration.HasOne<Renter>().WithMany().HasForeignKey(history => history.RenterId);
        }
    }
}
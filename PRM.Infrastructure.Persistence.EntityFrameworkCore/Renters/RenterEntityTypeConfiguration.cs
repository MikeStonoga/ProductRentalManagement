using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRM.Domain.Renters;

namespace PRM.Infrastructure.Persistence.EntityFrameworkCore.Renters
{
    public class RenterEntityTypeConfiguration : IEntityTypeConfiguration<Renter>
    {
        private readonly string _defaultSchema;

        public RenterEntityTypeConfiguration(string defaultSchema)
        {
            _defaultSchema = defaultSchema;
        }

        public void Configure(EntityTypeBuilder<Renter> renterConfiguration)
        {
            renterConfiguration.ToTable("renters", _defaultSchema);
            
            renterConfiguration.HasKey(renter => renter.Id);
            renterConfiguration.Property(r => r.Name).IsRequired();
            renterConfiguration.Property(r => r.Email).IsRequired();
            renterConfiguration.Property(r => r.BirthDate).IsRequired();
            renterConfiguration.Property(r => r.Code).IsRequired();
            renterConfiguration.Property(r => r.Phone).IsRequired();
            renterConfiguration.HasIndex(r => r.GovernmentRegistrationDocumentCode).IsUnique();
            renterConfiguration.Property(r => r.GovernmentRegistrationDocumentCode).IsRequired();

        }
    }
}
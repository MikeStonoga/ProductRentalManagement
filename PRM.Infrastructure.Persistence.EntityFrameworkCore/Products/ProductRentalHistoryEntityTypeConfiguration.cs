using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRM.Domain.Products;
using PRM.Domain.Rents;

namespace PRM.Infrastructure.Persistence.EntityFrameworkCore.Products
{
    public class ProductRentalHistoryEntityTypeConfiguration : IEntityTypeConfiguration<ProductRentalHistory>
    {
        private readonly string _defaultSchema;

        public ProductRentalHistoryEntityTypeConfiguration(string defaultSchema)
        {
            _defaultSchema = defaultSchema;
        }
        
        public void Configure(EntityTypeBuilder<ProductRentalHistory> renterRentalHistoryConfiguration)
        {
            renterRentalHistoryConfiguration.ToTable("products_rental_history", _defaultSchema);
            
            renterRentalHistoryConfiguration.HasKey(history => history.Id);
            renterRentalHistoryConfiguration.HasOne<Rent>().WithMany().HasForeignKey(history => history.RentId);
            renterRentalHistoryConfiguration.HasOne<Product>().WithMany().HasForeignKey(history => history.ProductId);
        }
    }
}
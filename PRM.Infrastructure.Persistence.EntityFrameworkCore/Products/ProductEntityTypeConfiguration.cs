using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRM.Domain.Products;
using PRM.Domain.Products.Enums;

namespace PRM.Infrastructure.Persistence.EntityFrameworkCore.Products
{
    public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        private readonly string _defaultSchema;

        public ProductEntityTypeConfiguration(string defaultSchema)
        {
            _defaultSchema = defaultSchema;
        }

        public void Configure(EntityTypeBuilder<Product> productConfiguration)
        {
            productConfiguration.ToTable("products", _defaultSchema);

            productConfiguration.HasKey(product => product.Id);
            productConfiguration.Property(p => p.Name).IsRequired();
            productConfiguration.Property(p => p.Code).IsRequired();
            productConfiguration.Property(p => p.Status).HasDefaultValue(ProductStatus.Available);
            productConfiguration.Property(p => p.RentDailyPrice).IsRequired();
            productConfiguration.Property(p => p.RentDailyLateFee).IsRequired();
        }
    }
}
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using AG.Products.API.Domain.Entities;

namespace AG.Products.API.Infra.Data.Mappings
{
    public class ProductContextMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(c => c.Code).UseIdentityColumn(1, 1);

            builder.Property(p => p.Description)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.ToTable("Products");

            builder.ComplexProperty(p => p.Supplier, config =>
            {
                config.Property(x => x.Code)
                    .HasColumnName("SupplierCode");

                config.Property(x => x.Name)
                    .HasColumnType("varchar(100)")
                    .HasColumnName("SupplierName");

                config.Property(x => x.CNPJ)
                    .HasColumnType("varchar(18)")
                    .HasColumnName("CNPJ");
            });

            builder.ComplexProperty(p => p.ValidityPeriod, config =>
            {
                config.Property(x => x.ManufactureDate)
                    .HasColumnName("ManufactureDate");

                config.Property(x => x.DueDate)
                    .HasColumnName("DueDate");
            });
        }
    }
}

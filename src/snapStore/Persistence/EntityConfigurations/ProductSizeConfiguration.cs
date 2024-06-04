using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ProductSizeConfiguration : IEntityTypeConfiguration<ProductSize>
{
    public void Configure(EntityTypeBuilder<ProductSize> builder)
    {
        builder.ToTable("ProductSizes").HasKey(ps => ps.Id);

        builder.Property(ps => ps.Id).HasColumnName("Id").IsRequired();
        builder.Property(ps => ps.Size).HasColumnName("Size").IsRequired();
        builder.Property(ps => ps.StockQuantity).HasColumnName("StockQuantity").IsRequired();
        builder.Property(ps => ps.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(ps => ps.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(ps => ps.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(ps => !ps.DeletedDate.HasValue);
    }
}
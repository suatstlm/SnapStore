using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ProductDescriptionConfiguration : IEntityTypeConfiguration<ProductDescription>
{
    public void Configure(EntityTypeBuilder<ProductDescription> builder)
    {
        builder.ToTable("ProductDescriptions").HasKey(pd => pd.Id);

        builder.Property(pd => pd.Id).HasColumnName("Id").IsRequired();
        builder.Property(pd => pd.Name).HasColumnName("Name").IsRequired();
        builder.Property(pd => pd.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(pd => pd.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(pd => pd.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(pd => !pd.DeletedDate.HasValue);
    }
}
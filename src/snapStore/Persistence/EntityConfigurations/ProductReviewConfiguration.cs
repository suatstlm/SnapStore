using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ProductReviewConfiguration : IEntityTypeConfiguration<ProductReview>
{
    public void Configure(EntityTypeBuilder<ProductReview> builder)
    {
        builder.ToTable("ProductReviews").HasKey(pr => pr.Id);

        builder.Property(pr => pr.Id).HasColumnName("Id").IsRequired();
        builder.Property(pr => pr.Content).HasColumnName("Content").IsRequired();
        builder.Property(pr => pr.Rating).HasColumnName("Rating").IsRequired();
        builder.Property(pr => pr.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(pr => pr.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(pr => pr.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(pr => !pr.DeletedDate.HasValue);
    }
}
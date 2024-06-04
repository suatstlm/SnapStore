using Application.Features.Auth.Constants;
using Application.Features.OperationClaims.Constants;
using Application.Features.UserOperationClaims.Constants;
using Application.Features.Users.Constants;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NArchitecture.Core.Security.Constants;
using Application.Features.Categories.Constants;
using Application.Features.Products.Constants;
using Application.Features.ProductDescriptions.Constants;
using Application.Features.ProductImages.Constants;
using Application.Features.ProductReviews.Constants;
using Application.Features.ProductSizes.Constants;

namespace Persistence.EntityConfigurations;

public class OperationClaimConfiguration : IEntityTypeConfiguration<OperationClaim>
{
    public void Configure(EntityTypeBuilder<OperationClaim> builder)
    {
        builder.ToTable("OperationClaims").HasKey(oc => oc.Id);

        builder.Property(oc => oc.Id).HasColumnName("Id").IsRequired();
        builder.Property(oc => oc.Name).HasColumnName("Name").IsRequired();
        builder.Property(oc => oc.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(oc => oc.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(oc => oc.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(oc => !oc.DeletedDate.HasValue);

        builder.HasData(_seeds);

        builder.HasBaseType((string)null!);
    }

    public static int AdminId => 1;
    private IEnumerable<OperationClaim> _seeds
    {
        get
        {
            yield return new() { Id = AdminId, Name = GeneralOperationClaims.Admin };

            IEnumerable<OperationClaim> featureOperationClaims = getFeatureOperationClaims(AdminId);
            foreach (OperationClaim claim in featureOperationClaims)
                yield return claim;
        }
    }

#pragma warning disable S1854 // Unused assignments should be removed
    private IEnumerable<OperationClaim> getFeatureOperationClaims(int initialId)
    {
        int lastId = initialId;
        List<OperationClaim> featureOperationClaims = new();

        #region Auth
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = AuthOperationClaims.Admin },
                new() { Id = ++lastId, Name = AuthOperationClaims.Read },
                new() { Id = ++lastId, Name = AuthOperationClaims.Write },
                new() { Id = ++lastId, Name = AuthOperationClaims.RevokeToken },
            ]
        );
        #endregion

        #region OperationClaims
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Admin },
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Read },
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Write },
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Create },
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Update },
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Delete },
            ]
        );
        #endregion

        #region UserOperationClaims
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Admin },
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Read },
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Write },
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Create },
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Update },
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Delete },
            ]
        );
        #endregion

        #region Users
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = UsersOperationClaims.Admin },
                new() { Id = ++lastId, Name = UsersOperationClaims.Read },
                new() { Id = ++lastId, Name = UsersOperationClaims.Write },
                new() { Id = ++lastId, Name = UsersOperationClaims.Create },
                new() { Id = ++lastId, Name = UsersOperationClaims.Update },
                new() { Id = ++lastId, Name = UsersOperationClaims.Delete },
            ]
        );
        #endregion

        
        #region Categories CRUD
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = CategoriesOperationClaims.Admin },
                new() { Id = ++lastId, Name = CategoriesOperationClaims.Read },
                new() { Id = ++lastId, Name = CategoriesOperationClaims.Write },
                new() { Id = ++lastId, Name = CategoriesOperationClaims.Create },
                new() { Id = ++lastId, Name = CategoriesOperationClaims.Update },
                new() { Id = ++lastId, Name = CategoriesOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region Products CRUD
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = ProductsOperationClaims.Admin },
                new() { Id = ++lastId, Name = ProductsOperationClaims.Read },
                new() { Id = ++lastId, Name = ProductsOperationClaims.Write },
                new() { Id = ++lastId, Name = ProductsOperationClaims.Create },
                new() { Id = ++lastId, Name = ProductsOperationClaims.Update },
                new() { Id = ++lastId, Name = ProductsOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region ProductDescriptions CRUD
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = ProductDescriptionsOperationClaims.Admin },
                new() { Id = ++lastId, Name = ProductDescriptionsOperationClaims.Read },
                new() { Id = ++lastId, Name = ProductDescriptionsOperationClaims.Write },
                new() { Id = ++lastId, Name = ProductDescriptionsOperationClaims.Create },
                new() { Id = ++lastId, Name = ProductDescriptionsOperationClaims.Update },
                new() { Id = ++lastId, Name = ProductDescriptionsOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region ProductImages CRUD
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = ProductImagesOperationClaims.Admin },
                new() { Id = ++lastId, Name = ProductImagesOperationClaims.Read },
                new() { Id = ++lastId, Name = ProductImagesOperationClaims.Write },
                new() { Id = ++lastId, Name = ProductImagesOperationClaims.Create },
                new() { Id = ++lastId, Name = ProductImagesOperationClaims.Update },
                new() { Id = ++lastId, Name = ProductImagesOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region ProductReviews CRUD
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = ProductReviewsOperationClaims.Admin },
                new() { Id = ++lastId, Name = ProductReviewsOperationClaims.Read },
                new() { Id = ++lastId, Name = ProductReviewsOperationClaims.Write },
                new() { Id = ++lastId, Name = ProductReviewsOperationClaims.Create },
                new() { Id = ++lastId, Name = ProductReviewsOperationClaims.Update },
                new() { Id = ++lastId, Name = ProductReviewsOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region ProductSizes CRUD
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = ProductSizesOperationClaims.Admin },
                new() { Id = ++lastId, Name = ProductSizesOperationClaims.Read },
                new() { Id = ++lastId, Name = ProductSizesOperationClaims.Write },
                new() { Id = ++lastId, Name = ProductSizesOperationClaims.Create },
                new() { Id = ++lastId, Name = ProductSizesOperationClaims.Update },
                new() { Id = ++lastId, Name = ProductSizesOperationClaims.Delete },
            ]
        );
        #endregion
        
        return featureOperationClaims;
    }
#pragma warning restore S1854 // Unused assignments should be removed
}

using Application.Features.ProductReviews.Commands.Create;
using Application.Features.ProductReviews.Commands.Delete;
using Application.Features.ProductReviews.Commands.Update;
using Application.Features.ProductReviews.Queries.GetById;
using Application.Features.ProductReviews.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.ProductReviews.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateProductReviewCommand, ProductReview>();
        CreateMap<ProductReview, CreatedProductReviewResponse>();

        CreateMap<UpdateProductReviewCommand, ProductReview>();
        CreateMap<ProductReview, UpdatedProductReviewResponse>();

        CreateMap<DeleteProductReviewCommand, ProductReview>();
        CreateMap<ProductReview, DeletedProductReviewResponse>();

        CreateMap<ProductReview, GetByIdProductReviewResponse>();

        CreateMap<ProductReview, GetListProductReviewListItemDto>();
        CreateMap<IPaginate<ProductReview>, GetListResponse<GetListProductReviewListItemDto>>();
    }
}
using Application.Features.ProductSizes.Commands.Create;
using Application.Features.ProductSizes.Commands.Delete;
using Application.Features.ProductSizes.Commands.Update;
using Application.Features.ProductSizes.Queries.GetById;
using Application.Features.ProductSizes.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.ProductSizes.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateProductSizeCommand, ProductSize>();
        CreateMap<ProductSize, CreatedProductSizeResponse>();

        CreateMap<UpdateProductSizeCommand, ProductSize>();
        CreateMap<ProductSize, UpdatedProductSizeResponse>();

        CreateMap<DeleteProductSizeCommand, ProductSize>();
        CreateMap<ProductSize, DeletedProductSizeResponse>();

        CreateMap<ProductSize, GetByIdProductSizeResponse>();

        CreateMap<ProductSize, GetListProductSizeListItemDto>();
        CreateMap<IPaginate<ProductSize>, GetListResponse<GetListProductSizeListItemDto>>();
    }
}
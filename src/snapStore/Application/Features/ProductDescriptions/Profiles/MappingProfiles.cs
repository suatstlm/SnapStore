using Application.Features.ProductDescriptions.Commands.Create;
using Application.Features.ProductDescriptions.Commands.Delete;
using Application.Features.ProductDescriptions.Commands.Update;
using Application.Features.ProductDescriptions.Queries.GetById;
using Application.Features.ProductDescriptions.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.ProductDescriptions.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateProductDescriptionCommand, ProductDescription>();
        CreateMap<ProductDescription, CreatedProductDescriptionResponse>();

        CreateMap<UpdateProductDescriptionCommand, ProductDescription>();
        CreateMap<ProductDescription, UpdatedProductDescriptionResponse>();

        CreateMap<DeleteProductDescriptionCommand, ProductDescription>();
        CreateMap<ProductDescription, DeletedProductDescriptionResponse>();

        CreateMap<ProductDescription, GetByIdProductDescriptionResponse>();

        CreateMap<ProductDescription, GetListProductDescriptionListItemDto>();
        CreateMap<IPaginate<ProductDescription>, GetListResponse<GetListProductDescriptionListItemDto>>();
    }
}
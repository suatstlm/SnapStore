using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IProductDescriptionRepository : IAsyncRepository<ProductDescription, Guid>, IRepository<ProductDescription, Guid>
{
}
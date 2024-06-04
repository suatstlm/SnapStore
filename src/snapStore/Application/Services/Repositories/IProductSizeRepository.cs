using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IProductSizeRepository : IAsyncRepository<ProductSize, Guid>, IRepository<ProductSize, Guid>
{
}
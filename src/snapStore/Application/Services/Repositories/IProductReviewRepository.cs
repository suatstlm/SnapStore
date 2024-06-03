using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IProductReviewRepository : IAsyncRepository<ProductReview, Guid>, IRepository<ProductReview, Guid>
{
}
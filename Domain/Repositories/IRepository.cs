using AG.Products.API.Domain.Entities;

namespace AG.Products.API.Domain.Repositories
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        public IUnitOfWork UnitOfWork { get; }
    }
}

namespace AG.Products.API.Domain.Repositories
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}

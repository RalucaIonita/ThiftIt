using DataLayer.Entities;

namespace DataLayer.Repositories
{
    public interface IProductRepository : IRepositoryBase<Product>
    {

    }
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(Context db) : base(db)
        {
        }
    }
}

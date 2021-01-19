using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        Task<List<Product>> GetAllProductsWithSellerAsync();
    }
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(Context db) : base(db)
        {
        }

        public async Task<List<Product>> GetAllProductsWithSellerAsync()
        {
            return await GetRecords().Where(p => !p.IsReserved).Include(p => p.Seller).ToListAsync();
        }
    }
}

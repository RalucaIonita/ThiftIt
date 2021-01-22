using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public interface ICartRepository : IRepositoryBase<Cart>
    {
        Task<Cart> GetUserCartAsync(Guid userId);

    }
    public class CartRepository : RepositoryBase<Cart>, ICartRepository
    {
        public CartRepository(Context db) : base(db)
        {
        }

        public async Task<Cart> GetUserCartAsync(Guid userId)
        {
            return await GetRecords().Include(c => c.Products).FirstOrDefaultAsync(c => c.UserId == userId);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public interface ICartRepository : IRepositoryBase<Cart>
    {
        Cart GetUserCart(Guid userId);

    }
    public class CartRepository : RepositoryBase<Cart>, ICartRepository
    {
        public CartRepository(Context db) : base(db)
        {
        }

        public Cart GetUserCart(Guid userId)
        {
            return GetRecords().Include(c => c.Products).FirstOrDefault(c => c.UserId == userId);
        }
    }
}

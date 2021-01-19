using DataLayer.Repositories;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IUnitOfWork
    {
        Context Context { get; }
        IProductRepository Products { get; }
        IUserRepository Users { get; set; }
        ICartRepository Carts { get; set; }

        Task<bool> SaveChangesAsync();

    }
    public class UnitOfWork : IUnitOfWork
    {
        public Context Context { get; set; }

        public IProductRepository Products { get; set; }
        public IUserRepository Users { get; set; }
        public ICartRepository Carts { get; set; }

        public async Task<bool> SaveChangesAsync()
        {
            return (await Context.SaveChangesAsync() > 0);
        }


        public UnitOfWork(Context context, 
            IProductRepository productRepository,
            IUserRepository userRepository,
            ICartRepository cartRepository)
        {
            Context = context;
            Products = productRepository;
            Users = userRepository;
            Carts = cartRepository;
        }



    }
}

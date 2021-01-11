using DataLayer;
using DataLayer.Entities;
using DataLayer.Repositories;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Services
{
    public interface IUnitOfWork
    {
        Context Context { get; }
        IProductRepository Products { get; }
        IUserRepository Users { get; set; }

        Task<bool> SaveChangesAsync();

    }
    public class UnitOfWork : IUnitOfWork
    {
        public Context Context { get; set; }

        public IProductRepository Products { get; set; }
        public IUserRepository Users { get; set; }

        public async Task<bool> SaveChangesAsync()
        {
            return (await Context.SaveChangesAsync() > 0);
        }


        public UnitOfWork(Context context, IProductRepository productRepository,IUserRepository userRepository)
        {
            Context = context;
            Products = productRepository;
            Users = userRepository;
        }



    }
}

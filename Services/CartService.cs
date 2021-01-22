using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataLayer;
using DataLayer.Entities;
using Services.Dtos;

namespace Services
{
    public interface ICartService : IBaseService
    {
        Task AddProductsToCartAsync(Guid userId, List<Guid> productIds);
        Task<CartDto> GetUserCartAsync(Guid userId);
    }
    public class CartService : BaseService, ICartService
    {
        private IMapper _mapper;
        public CartService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            _mapper = mapper;
        }

        public async Task AddProductsToCartAsync(Guid userId, List<Guid> productIds)
        {
            var products = new List<Product>();
            productIds.ForEach(async id =>
            {
                var product = await UnitOfWork.Products.GetByIdAsync(id);
                products.Add(product);
            });

            var user = await UnitOfWork.Users.GetUserByIdAsync(userId);

            var cart = new Cart
            {
                Products = products,
                User = user
            };

            UnitOfWork.Carts.Insert(cart);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task<CartDto> GetUserCartAsync(Guid userId)
        {
            var cart = await UnitOfWork.Carts.GetUserCartAsync(userId);
            return _mapper.Map<Cart, CartDto>(cart);
        }
    }
}

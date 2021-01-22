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
    public interface IProductService : IBaseService
    {
        Task<List<ProductDto>> GetAllProductsAsync();
        Task<bool> AddProductAsync(Guid userId, CreateProductDto payload);
        Task<Product> GetProductByIdAsync(Guid productId);
        Task UpdateProductAsync(Guid productId, CreateProductDto payload);
        Task DeleteProductAsync(Guid productId);
    }
    public class ProductService : BaseService, IProductService
    {
        private IMapper _mapper;
        public ProductService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            _mapper = mapper;
        }

        
        public async Task<bool> AddProductAsync(Guid userId, CreateProductDto payload)
        {
            var pictures = new List<Picture>();
            var product = new Product
            {
                Description = payload.Description,
                Price = payload.Price,
                Title = payload.Title,
            };

            var picture = new Picture
            {
                Body = payload.Picture,
                ProductId = product.Id,
                Url = "idk, ma mai gandesc",
                Title = product.Title + Guid.NewGuid()
            };

            var user = await UnitOfWork.Users.GetUserByIdAsync(userId);
            if (user == null)
                return false;
            product.Picture = picture;
            product.Seller = user;

            UnitOfWork.Products.Insert(product);
            return await UnitOfWork.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(Guid productId)
        {
            var product = await UnitOfWork.Products.GetByIdAsync(productId);
            UnitOfWork.Products.Remove(product);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            var products = await UnitOfWork.Products.GetAllProductsWithSellerAsync();
            var dtos = new List<ProductDto>();
            products.ForEach(x => dtos.Add(_mapper.Map<Product, ProductDto>(x)));

            return dtos;
        }

        public async Task<Product> GetProductByIdAsync(Guid productId)
        {
            return await UnitOfWork.Products.GetByIdAsync(productId);
        }

        public async Task UpdateProductAsync(Guid productId, CreateProductDto payload)
        {
            var product = await UnitOfWork.Products.GetByIdAsync(productId);
            _mapper.Map(payload, product);

            UnitOfWork.Products.Update(product);
            await UnitOfWork.SaveChangesAsync();
        }
    }
}

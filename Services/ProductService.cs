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

            payload.PictureBodies.ForEach(x => pictures.Add(new Picture
            {
                Body = x,
                ProductId = product.Id,
                Url = "idk, ma mai gandesc",
                Title = product.Title + Guid.NewGuid()
            }));

            var user = await UnitOfWork.Users.GetUserByIdAsync(userId);
            if (user == null)
                return false;
            product.Pictures = pictures;
            product.Seller = user;

            UnitOfWork.Products.Insert(product);
            return await UnitOfWork.SaveChangesAsync();
        }

        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            var products = await UnitOfWork.Products.GetAllProductsWithSellerAsync();
            var dtos = new List<ProductDto>();
            products.ForEach(x => dtos.Add(_mapper.Map<Product, ProductDto>(x)));

            return dtos;
        }
    }
}

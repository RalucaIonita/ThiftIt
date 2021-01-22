using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Dtos;

namespace ThriftIt.Controllers
{
    [ApiController]
    [Route("api/products")]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private IUserService _userService;
        private IMapper _mapper;

        public ProductController(IProductService productService, IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllProductsAsync()
        {
            var productDtos = await _productService.GetAllProductsAsync();
            if (productDtos == null || productDtos.Count == 0)
                return BadRequest("Sorry, nu exista produse.");
            return Ok(productDtos);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductByIdAsync([FromRoute]Guid productId)
        {
            var product = await _productService.GetProductByIdAsync(productId);
            if (product == null)
                return BadRequest("Sorry, nu exista produse.");
            return Ok(_mapper.Map<Product, ProductDto>(product));
        }



        [HttpPost("sell")]
        public async Task<IActionResult> CreateProductAsync([FromBody]CreateProductDto payload)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.SerialNumber)?.Value;
            if (userId == null)
                return Unauthorized("You have no power here.");
            var user = await _userService.GetUserByIdAsync(Guid.Parse(userId));
            if(user == null)
                return Unauthorized("You have no power here.");


            var result = await _productService.AddProductAsync(Guid.Parse(userId), payload);

            if (!result)
                return BadRequest("Either invalid input or backend problem.");
            return Ok("Product added");
        }

        [HttpPut("modify-product/{productId}")]
        public async Task<IActionResult> UpdateProductAsync([FromRoute] Guid productId, [FromBody] CreateProductDto payload)
        {
            var product = await _productService.GetProductByIdAsync(productId);
            if (product == null)
                return BadRequest("Product does not exist.");

            await _productService.UpdateProductAsync(productId, payload);

            return Ok("Product updated.");
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] Guid productId)
        {
            var product = await _productService.GetProductByIdAsync(productId);
            if (product == null)
                return BadRequest("Product does not exist.");

            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.SerialNumber)?.Value;
            if (userId == null)
                return Unauthorized("You have no power here.");
            var user = await _userService.GetUserByIdAsync(Guid.Parse(userId));
            if (user == null)
                return Unauthorized("You have no power here.");

            if (product.SellerId != user.Id || !User.IsInRole("SuperAdmin"))
                return Unauthorized("You have no power here.");

            await _productService.DeleteProductAsync(productId);
            return Ok("Product deleted.");
        }


        [HttpGet("mine")]
        public async Task<IActionResult> GetMyProductsAsync()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.SerialNumber)?.Value;
            if (userId == null)
                return Unauthorized("You have no power here.");
            var user = await _userService.GetUserByIdAsync(Guid.Parse(userId));
            if (user == null)
                return Unauthorized("You have no power here.");

            var productDtos = await _productService.GetAllProductsAsync();
            if (productDtos == null || productDtos.Count == 0)
                return BadRequest("Sorry, nu exista produse.");

            var myProducts = productDtos.Where(p => p.SellerEmail == user.Email);
            return Ok(myProducts);
        }
    }
}

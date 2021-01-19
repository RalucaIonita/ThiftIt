using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
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
        private IProductService _productService;
        private IUserService _userService;

        public ProductController(IProductService productService, IUserService userService)
        {
            _userService = userService;
            _productService = productService;
        }



        [HttpGet("all")]
        public async Task<IActionResult> GetAllProductsAsync()
        {
            var productDtos = await _productService.GetAllProductsAsync();
            if (productDtos == null || productDtos.Count == 0)
                return BadRequest("Sorry, nu exista produse.");
            return Ok(productDtos);
        }

        [HttpPost("sell")]
        public async Task<IActionResult> CreateProductAsync([FromBody]CreateProductDto payload)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.SerialNumber)?.Value;
            if (userId == null)
                return Unauthorized("You have no power here.");
            var result = await _productService.AddProductAsync(Guid.Parse(userId), payload);

            if (!result)
                return BadRequest("Either invalid input or backend problem.");
            return Ok("Product added");
        }

    }
}

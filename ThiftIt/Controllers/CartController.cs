using System;
using System.Collections.Generic;
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
    [Route("api/cart")]
    // [Authorize]
    public class CartController : ControllerBase
    {
        private ICartService _cartService;
        private IProductService _productService;
        private IUserService _userService;

        public CartController(ICartService cartService, IProductService productService, IUserService userService)
        {
            _cartService = cartService;
            _productService = productService;
            _userService = userService;
        }


        [HttpPost("new")]
        public async Task<IActionResult> CreateNewCart([FromBody]List<Guid> productIds)
        {
            var badData = false;
            foreach (var id in productIds)
            {
                var product = await _productService.GetProductByIdAsync(id);
                if (product == null)
                {
                    badData = true;
                    break;
                }
            }

            if (badData)
                return BadRequest("One or more products that you chose does not exist.");

            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.SerialNumber)?.Value;
            if (userId == null)
                return Unauthorized("You have no power here.");
            var user = await _userService.GetUserByIdAsync(Guid.Parse(userId));
            if (user == null)
                return Unauthorized("You have no power here.");


            await _cartService.AddProductsToCartAsync(Guid.Parse(userId), productIds);
            return Ok("Products have been added.");
        }

        [HttpGet("my-cart")]
        public async Task<IActionResult> GetUserCartAsync()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.SerialNumber)?.Value;
            if (userId == null)
                return Unauthorized("You have no power here.");
            var user = await _userService.GetUserByIdAsync(Guid.Parse(userId));
            if (user == null)
                return Unauthorized("You have no power here.");

            var cartDto = await _cartService.GetUserCartAsync(Guid.Parse(userId));
            return Ok(cartDto);
        }

        // [HttpPut("modify-cart/{cartId}")]
        // public async Task<IActionResult> UpdateCartAsync([FromRoute] Guid cartId, [FromBody] CartDto payload)
        // {
        //     var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.SerialNumber)?.Value;
        //     if (userId == null)
        //         return Unauthorized("You have no power here.");
        //     var user = await _userService.GetUserByIdAsync(Guid.Parse(userId));
        //     if (user == null)
        //         return Unauthorized("You have no power here.");
        //
        //     //some other stuff
        // }


    }
}

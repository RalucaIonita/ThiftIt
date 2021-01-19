using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;

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




        // [HttpGet("all")]

    }
}

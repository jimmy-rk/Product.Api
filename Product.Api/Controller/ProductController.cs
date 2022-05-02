using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Product.Api.Extensions;
using Product.Api.Models.General;
using Product.Api.Services.Product;
using System;
using System.Threading.Tasks;

namespace Product.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;

        public ProductController(ILogger<ProductController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        #region Get

        [HttpGet("/product/productGroupTypes")]
        public async Task<IActionResult> GetProductGroupTypes()
        {
            try
            {
                var serviceResponse = await _productService.GetProductGroupTypes();
                _logger.LogServiceResponse("Get Product Group Types", serviceResponse.Success, serviceResponse);
                return serviceResponse.ConvertToObjectResult();

            }
            catch (Exception ex)
            {
                _logger.LogException("Get Product Group Types", ex);
                return new ServiceResponse(false, "Unexpected_error", StatusCodes.Status500InternalServerError).ConvertToObjectResult();
            }
        }

        #endregion
    }
}

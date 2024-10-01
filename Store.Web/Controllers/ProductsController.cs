using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Repository.Specification.ProductSpecs;
using Store.Service.Services.ProductServices;
using Store.Service.Services.ProductServices.Dtos;

namespace Store.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductServices _productServices;
        public ProductsController(IProductServices productService) {
        
            _productServices = productService;
        
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<BrandTypeDetailsDto>>> GetAllBrands()
            =>Ok(await _productServices.GetAllBrandsAsync());
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<BrandTypeDetailsDto>>> GetAllTypes()
         => Ok(await _productServices.GetAllTypesAsync());
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductDetailsDto>>> GetAllProducts([FromQuery]ProductSpecification input)
     => Ok(await _productServices.GetAllProductsAsync(input));
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductDetailsDto>>> GetProductById(int? id)
=> Ok(await _productServices.GetProductByIdAsynx(id));
    }
}

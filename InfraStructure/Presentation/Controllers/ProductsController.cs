using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared;
using Shared.DataTransferObjects.ProductDTos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IServiceManager _serviceManager) : ControllerBase
    {

        // GET ALL PRODUCTS
        [HttpGet]

        public async Task<ActionResult<PaginatedResult<ProductDTo>>> GetAllProducts([FromQuery]ProductQueryParams queryParams)
        {


            var products = await _serviceManager.productService.GetAllProductsAsync(queryParams);
            return Ok(products);
        }

        [HttpGet("{id:int}")]

        public async Task<ActionResult<ProductDTo>> GetProduct(int id )
        {
            var product = await _serviceManager.productService.GetByIdAsync(id);
          
                return Ok(product);
          
         


        }


        [HttpGet("types")]
         
        public async Task<ActionResult<IEnumerable<TypeDTo>>> GetAllTypes()
        {
            var types = await _serviceManager.productService.GetAllTypesAsync();
            return Ok(types);
        }


        [HttpGet("brands")]

        public async Task<ActionResult<IEnumerable<BrandDTo>>> GetAllBrands()
        {
            var brands = await _serviceManager.productService.GetAllBrandsAsync();
            return Ok(brands);
        }




    }
}


using API.RequestHelpers;
using Core.Entities;
using Core.Interfaces;
using Core.Specification;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{

    public class ProductsController(IGenericRepository<Product> repo) : BaseApiController
    {
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts([FromQuery] ProductSpecParams specParams)
        {

            // return Ok(await repo.GetProductsAsync(brand, type, sort));
            var spec = new ProductSpecification(specParams);
            // var products = await repo.ListAsync(spec);
            // var count = await repo.CountAsync(spec);
            // var pagination = new Pagination<Product>(specParams.PageIndex, specParams.PageSize, count, products);
            return await CreatePagedResult(repo, spec, specParams.PageIndex, specParams.PageSize);
        }
        [HttpGet("{id:int}")] //api/products/2

        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            // var product = await repo.GetProductByIdAsync(id);
            var product = await repo.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return product;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            repo.Add(product);
            if (await repo.SaveAllAsync())
            {
                return CreatedAtAction("GetProduct", new { id = product.Id }, product);
            }
            return BadRequest("Problem creating a product");

        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Product>> UpdateProduct(int id, Product product)
        {
            if (product.Id != id || !ProductExists(id))
            {
                return BadRequest("Cannot update this product");
            }
            repo.Update(product);
            if (await repo.SaveAllAsync())
            {
                return NoContent();
            }
            return BadRequest("Failed to update product");
        }
        [HttpDelete("{id:int}")]

        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            var product = await repo.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            repo.Remove(product);
            if (await repo.SaveAllAsync())
            {
                return NoContent();
            }
            return BadRequest("Failed to delete product");

        }

        [HttpGet("brands")]

        public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
        {
            //TODO: Implement method
            var spec = new BrandListSpecification();
            return Ok(await repo.ListAsync(spec));
        }

        [HttpGet("types")]

        public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
        {
            //TODO: Implement method
            var spec = new TypeListSpecification();
            return Ok(await repo.ListAsync(spec));
        }
        private bool ProductExists(int id)
        {

            return repo.Exists(id);
        }
    }
}
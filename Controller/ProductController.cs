using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AzurePractice.Models;
using AzurePractice.Repositories;
using AzurePractice.Repositories.Base;

namespace AzurePractice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductsRepository _repository;

        public ProductController(IProductsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await this._repository.GetProducts();
            if (products == null)
            {
                return NotFound();
            }
            return Ok(products);
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<Product>> GetProduct(string name)
        {
            var product = await this._repository.GetProductByName(name: name);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            try
            {
                await this._repository.CreateProduct(newProduct: product);
                return CreatedAtAction(nameof(GetProduct), new { name = product.Name }, product);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Product updatedProduct)
        {
            if (id != updatedProduct.Id)
            {
                return BadRequest();
            }

            try
            {
                await this._repository.UpdateProduct(newProduct: updatedProduct);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await this._repository.DeleteProduct(productId: id);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
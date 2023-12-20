using API.FurnitureStore.Data;
using API.FurnitureStore.shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.FurnitureStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly APIFurnitureStoreContext _context;

        public ProductsController(APIFurnitureStoreContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> Get()
        {
            try
            {
                return await _context.products.ToListAsync();
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine(ex);
                throw new ApplicationException("Error retrieving products.", ex);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetails(int id)
        {
            try
            {
                var product = await _context.products.FirstOrDefaultAsync(p => p.Id == id);

                if (product == null) return BadRequest();

                return Ok(product);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine(ex);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetByCategory/{productCategoryId}")]

        public async Task<IEnumerable<Product>> GetByCategory(int productCategoryId)
        {
            // filtramos los productos y decimos que solamente nos traiga los productos que cumplan con esa ca
            return await _context.products
                .Where(p => p.ProductCategoryId == productCategoryId)
                .ToListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Post(Product product)
        {
            try
            {
                await _context.products.AddAsync(product);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetDetails), new { id = product.Id }, product);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine(ex);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(Product product)
        {
            try
            {
                _context.products.Update(product);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine(ex);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Product product)
        {
            try
            {
                if (product == null) return NotFound();

                _context.products.Remove(product);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine(ex);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}

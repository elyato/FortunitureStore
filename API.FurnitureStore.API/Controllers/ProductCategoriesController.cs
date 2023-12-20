using API.FurnitureStore.Data;
using API.FurnitureStore.shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.FurnitureStore.API.Controllers
{
    [ApiController]

    [Route("api/ f[controller]")]    
    public class ProductCategoriesController : ControllerBase
    {
        private readonly APIFurnitureStoreContext _context;

        public ProductCategoriesController(APIFurnitureStoreContext context)
        {
            _context = context;
        }

        [HttpGet]

        public async Task<IEnumerable<ProductCategory>> Get()
        {
            return await _context.productCategories.ToListAsync();
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> getDetails(int id)
        {
            var category = await _context.productCategories.FirstOrDefaultAsync(p => p.Id == id);

            if (category == null) return NotFound();

            return Ok(category);

        }
        [HttpPost]
        public async Task<IActionResult> Post(ProductCategory category)
        {
            await _context.productCategories.AddAsync(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction("post", category.Id, category);
        }

        [HttpPut]
        public async Task<IActionResult> put(ProductCategory category)
        {
            _context.productCategories.Update(category);

            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(ProductCategory category)
        {
            if(category == null) return NotFound();
            
            _context.productCategories.Remove(category);

            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}

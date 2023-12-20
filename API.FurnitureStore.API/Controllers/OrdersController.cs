using API.FurnitureStore.Data;
using API.FurnitureStore.shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.FurnitureStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly APIFurnitureStoreContext _context;

        public OrdersController(APIFurnitureStoreContext context)
        {
            _context = context;
        }

        [HttpGet]

        public async Task<IEnumerable<Order>> Get()
        {
            return await _context.orders.Include(o => o.OrderDetails).ToListAsync();
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetDetails(int id)
        {
            var order = await _context.orders.Include(o => o.OrderDetails).FirstOrDefaultAsync(o => o.Id == id);

            if (order == null) return NotFound();

            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> post(Order order)
        {
            if (order.OrderDetails == null)
            {
                return BadRequest("order should hace at least one details");

            }
            await _context.orders.AddAsync(order);
            await _context.OrderDetails.AddRangeAsync(order.OrderDetails);

            await _context.SaveChangesAsync();

            return CreatedAtAction("post", order.Id, order);
        }

        [HttpPut]

        public async Task<IActionResult> put(Order order)
        {
            // acá vamos a actualizar el order que viene de la base de datos a la order que viene por parametros
            if (order == null) return NotFound();

            if (order.Id <= 0) return NotFound();

            var existingOrder = await _context.orders.Include(o => o.OrderDetails).FirstOrDefaultAsync(o => o.Id == order.Id);

            if (existingOrder == null) return NotFound();

            existingOrder.OrderNumber = order.OrderNumber;
            existingOrder.orderDate = order.orderDate;
            existingOrder.DeliveryOrder = order.DeliveryOrder;
            existingOrder.ClientId = order.ClientId;

            // acá vamos a eliminar todo lo que este en la lista
            _context.OrderDetails.RemoveRange(existingOrder.OrderDetails);

            _context.orders.Update(existingOrder);
            _context.OrderDetails.AddRange(order.OrderDetails);

            await _context.SaveChangesAsync();

            return NoContent();

        }

        [HttpDelete]

        public async Task<IActionResult> Delete(Order order)
        {
            if (order == null)
            {
                return NotFound();
            }

            var existingOrder = await _context.orders.Include(o => o.OrderDetails).FirstOrDefaultAsync(o => o.Id == order.Id);

            if (existingOrder == null)
            {
                return NotFound();
            }

            _context.OrderDetails.RemoveRange(existingOrder.OrderDetails);
            _context.orders.Remove(existingOrder);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI_Entity_framework_core.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI_Entity_framework_core.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly OrderContext _orderContext;

        public OrderController(OrderContext context)
        {
            _orderContext = context;

            if (_orderContext.OrderItems.Count() == 0)
            {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TodoItems.
                _orderContext.OrderItems.Add(new Order { ProductName = "Product1", ProductPrice=90, ProductNumber="sdsafsdfsg" });
                _orderContext.SaveChanges();
            }

        }

        // GET: api/<controller>
        // GET: api/Todo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetTodoOrders()
        {
            return await _orderContext.OrderItems.ToListAsync();
        }

        // GET: api/Todo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetTodoItem(long id)
        {
            var orderItem = await _orderContext.OrderItems.FindAsync(id);

            if (orderItem == null)
            {
                return NotFound();
            }

            return orderItem;
        }

        // POST: api/Todo
        [HttpPost]
        public async Task<ActionResult<Order>> PostTodoItem([FromBody] Order o)
        {
            _orderContext.OrderItems.Add(o);
            await _orderContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTodoOrders), new { id = o.Id }, o);
        }


        // PUT: api/Todo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, [FromBody] Order item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _orderContext.Entry(item).State = EntityState.Modified;
            await _orderContext.SaveChangesAsync();

            return NoContent();
        }

       
         [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var todoItem = await _orderContext.OrderItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            _orderContext.OrderItems.Remove(todoItem);
            await _orderContext.SaveChangesAsync();

            return NoContent();
        }
    }
}

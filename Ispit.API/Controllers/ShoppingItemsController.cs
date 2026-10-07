using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ispit.Data.Entities;
using Ispit.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ispit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingItemsController(AppDbContext dbContext) : ControllerBase
    {
        // GET: api/<ShoppingItemController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShoppingItem>>> GetAsync()
        {
            var result = await dbContext.ShoppingItems.ToListAsync();

            if (result.Count == 0)
            {
                return NoContent();
            }

            return Ok(result);
        }

        // GET api/<ShoppingItemController>/5
        [HttpGet("{id}")]
        public ActionResult<ShoppingItem> GetShoppingItem(int id)
        {
            var shoppingItem = dbContext.ShoppingItems.Find(id);

            if (shoppingItem == null)
            {
                return NotFound();
            }

            return Ok(shoppingItem);
        }

        // POST api/<ShoppingItemController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ShoppingItem shoppingItem)
        {
            dbContext.ShoppingItems.Add(shoppingItem);
            await dbContext.SaveChangesAsync();

            return CreatedAtAction("GetShoppingItem", new { id = shoppingItem.Id }, shoppingItem);

        }

        // PUT api/<ShoppingItemController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ShoppingItem model)
        {

            if (id != model.Id)
            {
                return BadRequest();
            }

            var shoppingItem = await dbContext.ShoppingItems.FindAsync(id);

            if (shoppingItem == null)
            {
                return NotFound();
            }

            shoppingItem.Manufacturer = model.Manufacturer;
            shoppingItem.Name = model.Name;
            shoppingItem.Price = model.Price;

            await dbContext.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/<ShoppingItemController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var shoppingItem = await dbContext.ShoppingItems.FindAsync(id);

            if (shoppingItem == null)
            {
                return NotFound();
            }

            dbContext.ShoppingItems.Remove(shoppingItem);
            await dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}

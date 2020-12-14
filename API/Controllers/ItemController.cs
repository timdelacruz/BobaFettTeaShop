using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly DataContext _context;
        public ItemController(DataContext context)
        {
            _context = context;
        }
        
        //items
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Item>>> GetItems(int id)
        {
            var items = _context.Item.Where(x => x.TransactionId == id).ToListAsync();

            return await items;
        }

        /*[HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItem(int id)
        {
            var item = _context.Item.FindAsync(id);

            return await item;
        }*/

        //itemtoppings
        [HttpGet("toppings/{id}")]
        public async Task<ActionResult<List<ItemToppings>>> GetItemToppings(int id)
        {
            var itemToppings = _context.ItemToppings.Where(x => x.ItemId == id);

            return await itemToppings.ToListAsync();
        }

    }
}
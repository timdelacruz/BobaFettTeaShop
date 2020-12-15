using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Dtos;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class ItemController : BaseApiController
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

        [HttpPost("add")]
        public async Task<IActionResult> AddItem(ItemDto itemDto)
        {
            var itemToAdd = new Item
            {
                TransactionId = itemDto.TransactionId,
                BaseTea = itemDto.BaseTea,
                Flavor = itemDto.Flavor,
                Size = itemDto.Size,
                Price = itemDto.Price
            };

            await _context.Item.AddAsync(itemToAdd);
            await _context.SaveChangesAsync();

            return StatusCode(201);
        }

        [HttpPost("edit")]
        public async Task<IActionResult> EditItem(ItemDto itemDto, int id)
        {
            var item = await _context.Item.FirstOrDefaultAsync(x => x.Id == id);

            if (item == null)
                return null;

            item.BaseTea = itemDto.BaseTea;
            item.Flavor = itemDto.Flavor;
            item.Size = itemDto.Size;
            item.Price = itemDto.Price;

            await _context.SaveChangesAsync();

            return StatusCode(200);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var item = new Item {Id = id};
            _context.Item.Attach(item);
            _context.Item.Remove(item);

            await _context.SaveChangesAsync();

            return StatusCode(200);
        }

    }
}
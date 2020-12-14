using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MiscController : ControllerBase
    {
        private readonly DataContext _context;
        public MiscController(DataContext context)
        {
            _context = context;
        }

        //base teas
        [HttpGet("teas")]
        public async Task<ActionResult<IEnumerable<Tea>>> GetTeas()
        {
            var teas = _context.Tea.ToListAsync();

            return await teas;
        }

        [HttpGet("teas/{id}")]
        public async Task<ActionResult<Tea>> GetTea(int id)
        {
            var tea = _context.Tea.FindAsync(id);

            return await tea;
        }

        //flavors
        [HttpGet("flavors")]
        public async Task<ActionResult<IEnumerable<Flavor>>> GetFlavors()
        {
            var flavors = _context.Flavor.ToListAsync();

            return await flavors;
        }

        [HttpGet("flavors/{id}")]
        public async Task<ActionResult<Flavor>> GetFlavor(int id)
        {
            var flavor = _context.Flavor.FindAsync(id);

            return await flavor;
        }

        //toppings
        [HttpGet("toppings")]
        public async Task<ActionResult<IEnumerable<Toppings>>> GetToppings()
        {
            var toppings = _context.Toppings.ToListAsync();

            return await toppings;
        }

        [HttpGet("toppings/{id}")]
        public async Task<ActionResult<Toppings>> GetTopping(int id)
        {
            var topping = _context.Toppings.FindAsync(id);

            return await topping;
        }

        //sizes
        [HttpGet("sizes")]
        public async Task<ActionResult<IEnumerable<Size>>> GetSizes()
        {
            var sizes = _context.Size.ToListAsync();

            return await sizes;
        }

        [HttpGet("sizes/{id}")]
        public async Task<ActionResult<Size>> GetSize(int id)
        {
            var size = _context.Size.FindAsync(id);

            return await size;
        }

    }
}
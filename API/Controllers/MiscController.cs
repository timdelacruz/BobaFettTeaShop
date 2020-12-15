using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.Dtos;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class MiscController : BaseApiController
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

        [HttpPost("teas/add")]
        public async Task<IActionResult> AddTea(TeaDto teaDto)
        {
            var tea = new Tea
            {
                Name = teaDto.Name,
                Price = teaDto.Price
            };

            await _context.Tea.AddAsync(tea);
            await _context.SaveChangesAsync();

            return StatusCode(201);
        }

        [HttpPost("teas/edit")]
        public async Task<IActionResult> EditTea(TeaDto teaDto, int id)
        {
            var tea = await _context.Tea.FirstOrDefaultAsync(x => x.Id == id);

            if (tea == null)
                return null;

            tea.Name = teaDto.Name;
            tea.Price = teaDto.Price;

            await _context.SaveChangesAsync();

            return StatusCode(200);
        }

        [HttpPost("teas/delete")]
        public async Task<IActionResult> DeleteTea(int id)
        {
            var tea = new Tea {Id = id};
            _context.Tea.Attach(tea);
            _context.Tea.Remove(tea);

            await _context.SaveChangesAsync();

            return StatusCode(200);
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

        [HttpPost("flavors/add")]
        public async Task<IActionResult> AddFlavor(FlavorDto flavorDto)
        {
            var flavor = new Flavor
            {
                Name = flavorDto.Name,
                Price = flavorDto.Price
            };

            await _context.Flavor.AddAsync(flavor);
            await _context.SaveChangesAsync();

            return StatusCode(201);
        }

        [HttpPost("flavors/edit")]
        public async Task<IActionResult> EditFlavor(FlavorDto flavorDto, int id)
        {
            var flavor = await _context.Flavor.FirstOrDefaultAsync(x => x.Id == id);

            if (flavor == null)
                return null;

            flavor.Name = flavorDto.Name;
            flavor.Price = flavorDto.Price;

            await _context.SaveChangesAsync();

            return StatusCode(200);
        }

        [HttpPost("flavors/delete")]
        public async Task<IActionResult> DeleteFlavor(int id)
        {
            var flavor = new Flavor {Id = id};
            _context.Flavor.Attach(flavor);
            _context.Flavor.Remove(flavor);

            await _context.SaveChangesAsync();

            return StatusCode(200);
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

        [HttpPost("toppings/add")]
        public async Task<IActionResult> AddTopping(ToppingDto toppingDto)
        {
            var topping = new Toppings
            {
                Name = toppingDto.Name,
                Price = toppingDto.Price
            };

            await _context.Toppings.AddAsync(topping);
            await _context.SaveChangesAsync();

            return StatusCode(201);
        }

        [HttpPost("toppings/edit")]
        public async Task<IActionResult> EditTopping(ToppingDto toppingDto, int id)
        {
            var topping = await _context.Toppings.FirstOrDefaultAsync(x => x.Id == id);

            if (topping == null)
                return null;

            topping.Name = toppingDto.Name;
            topping.Price = toppingDto.Price;

            await _context.SaveChangesAsync();

            return StatusCode(200);
        }

        [HttpPost("toppings/delete")]
        public async Task<IActionResult> DeleteTopping(int id)
        {
            var topping = new Toppings {Id = id};
            _context.Toppings.Attach(topping);
            _context.Toppings.Remove(topping);

            await _context.SaveChangesAsync();

            return StatusCode(200);
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

        [HttpPost("sizes/add")]
        public async Task<IActionResult> AddSize(SizeDto sizeDto)
        {
            var size = new Size
            {
                Name = sizeDto.Name,
                Price = sizeDto.Price
            };

            await _context.Size.AddAsync(size);
            await _context.SaveChangesAsync();

            return StatusCode(201);
        }

        [HttpPost("sizes/edit")]
        public async Task<IActionResult> EditSize(SizeDto sizeDto, int id)
        {
            var size = await _context.Size.FirstOrDefaultAsync(x => x.Id == id);

            if (size == null)
                return null;

            size.Name = sizeDto.Name;
            size.Price = sizeDto.Price;

            await _context.SaveChangesAsync();

            return StatusCode(200);
        }

        [HttpPost("sizes/delete")]
        public async Task<IActionResult> DeleteSize(int id)
        {
            var size = new Size {Id = id};
            _context.Size.Attach(size);
            _context.Size.Remove(size);

            await _context.SaveChangesAsync();

            return StatusCode(200);
        }

    }
}
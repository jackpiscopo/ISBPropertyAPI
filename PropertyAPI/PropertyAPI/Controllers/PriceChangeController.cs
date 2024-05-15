using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PropertyAPI.Data;

namespace PropertyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceChangeController : ControllerBase
    {
        private readonly DataContext _context;

        public PriceChangeController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddPriceChange([FromBody] PriceChangeDto priceChangeDto)
        {
            var priceChange = new PropertyPriceChange
            {
                PropertyId = priceChangeDto.PropertyId,
                NewPrice = priceChangeDto.NewPrice,
                ChangeDate = priceChangeDto.ChangeDate ?? DateTime.UtcNow
            };

            _context.PropertyPriceChange.Add(priceChange);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetPriceChange", new { id = priceChange.Id }, priceChange);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PropertyPriceChange>> GetPriceChange(int id)
        {
            var priceChange = await _context.PropertyPriceChange
                .Include(pc => pc.Property)
                .FirstOrDefaultAsync(pc => pc.Id == id);

            if (priceChange == null)
            {
                return NotFound();
            }

            return priceChange;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePriceChange(int id, [FromBody] PriceChangeDto priceChangeDto)
        {
            var priceChange = await _context.PropertyPriceChange.FindAsync(id);
            if (priceChange == null)
            {
                return NotFound();
            }

            priceChange.NewPrice = priceChangeDto.NewPrice;
            priceChange.ChangeDate = priceChangeDto.ChangeDate ?? DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePriceChange(int id)
        {
            var priceChange = await _context.PropertyPriceChange.FindAsync(id);
            if (priceChange == null)
            {
                return NotFound();
            }

            _context.PropertyPriceChange.Remove(priceChange);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

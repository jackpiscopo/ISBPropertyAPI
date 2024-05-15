using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PropertyAPI.Data;

namespace PropertyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesNewController : ControllerBase
    {
        private readonly DataContext _context;

        public PropertiesNewController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProperty([FromBody] PropertyDto propertyDto)
        {
            var property = new Property
            {
                PropertyName = propertyDto.PropertyName,
                PropertyAddress = "Address from frontend",
                Price = (float)propertyDto.Price,
                RegistrationDate = DateTime.UtcNow
            };

            _context.Properties.Add(property);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProperty), new { id = property.PropertyId }, property);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Property>> GetProperty(Guid id)
        {
            var property = await _context.Properties.FindAsync(id);
            if (property == null) return NotFound();
            return property;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProperty(Guid id, [FromBody] PropertyDto propertyDto)
        {
            var property = await _context.Properties.FindAsync(id);
            if (property == null) return NotFound();

            property.PropertyName = propertyDto.PropertyName;
            property.Price = (float)propertyDto.Price;
            // Continue updating other fields

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProperty(Guid id)
        {
            var property = await _context.Properties.FindAsync(id);
            if (property == null) return NotFound();

            _context.Properties.Remove(property);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

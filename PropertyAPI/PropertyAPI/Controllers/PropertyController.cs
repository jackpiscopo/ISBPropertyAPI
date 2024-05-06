using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PropertyAPI.Data;

namespace PropertyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly DataContext _context;

        public PropertyController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Property>>> GetProperties()
        {
            return Ok(await _context.Properties.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<List<Property>>> CreateProperty(Property property)
        {
            _context.Properties.Add(property);
            await _context.SaveChangesAsync();

            return Ok(await _context.Properties.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Property>>> UpdateProperty(Property property)
        {
            var dbProperty = await _context.Properties.FindAsync(property.PropertyId);
            if (dbProperty == null)
            {
                return BadRequest("Property not found.");
            }

            dbProperty.PropertyName = property.PropertyName;
            dbProperty.PropertyAddress = property.PropertyAddress;
            dbProperty.Price = property.Price;
            dbProperty.RegistrationDate = property.RegistrationDate;

            await _context.SaveChangesAsync();

            return Ok(await _context.Properties.ToListAsync());
        }

        [HttpDelete("propertyId")]
        public async Task<ActionResult<List<Property>>> DeleteProperty(int propertyId)
        {
            var dbProperty = await _context.Properties.FindAsync(propertyId);
            if (dbProperty == null)
            {
                return BadRequest("Property not found.");
            }

            _context.Properties.Remove(dbProperty);
            await _context.SaveChangesAsync();

            return Ok(await _context.Properties.ToListAsync());
        }
    }
}

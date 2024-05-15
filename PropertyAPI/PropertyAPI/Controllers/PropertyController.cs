using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PropertyAPI.Data;
using PropertyAPI.Models;

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
        public async Task<ActionResult<List<PropertyDetailDto>>> GetProperties()
        {
            return Ok(await _context.Properties.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<PropertyDto>>> UpdateProperty(PropertyDto propertyDto)
        {
            var dbProperty = await _context.Properties.FindAsync(propertyDto.PropertyId);
            if (dbProperty == null)
            {
                return BadRequest("Property not found.");
            }

            if ((decimal)dbProperty.Price != propertyDto.Price)
            {
                var priceChange = new PropertyPriceChange
                {
                    PropertyId = propertyDto.PropertyId,
                    NewPrice = (decimal)propertyDto.Price,
                    ChangeDate = propertyDto.RegistrationDate ?? DateTime.UtcNow,
                };

                _context.PropertyPriceChange.Add(priceChange);
                await _context.SaveChangesAsync();
            }
            
            dbProperty.PropertyName = propertyDto.PropertyName;
            dbProperty.PropertyAddress = propertyDto.PropertyAddress;
            dbProperty.Price = (float)propertyDto.Price;
            dbProperty.RegistrationDate = propertyDto.RegistrationDate ?? DateTime.UtcNow;

            await _context.SaveChangesAsync();

            var properties = await _context.Properties
                .Select(p => new PropertyDto
                {
                    PropertyId = p.PropertyId,
                    PropertyName = p.PropertyName,
                    PropertyAddress = p.PropertyAddress,
                    Price = (decimal)p.Price,
                    RegistrationDate = p.RegistrationDate
                }).ToListAsync();

            return Ok(properties);
        }

        [HttpDelete("{propertyId}")]
        public async Task<ActionResult<List<Property>>> DeleteProperty(Guid propertyId)
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

        [HttpPost]
        public async Task<ActionResult> AddActualProperty([FromBody] PropertyDto propertyDto)
        {
            var property = new Property
            {
                PropertyName = propertyDto.PropertyName,
                PropertyAddress = propertyDto.PropertyAddress,
                Price = (float)propertyDto.Price,
                RegistrationDate = DateTime.UtcNow
            };

            _context.Properties.Add(property);
            await _context.SaveChangesAsync();
            return Ok(await _context.Properties.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Property>> GetActualProperty(Guid id)
        {
            var property = await _context.Properties.FindAsync(id);
            if (property == null)
            {
                return NotFound();
            }
            return property;
        }

        [HttpGet("propertiesDropdown")]
        public async Task<ActionResult<List<PropertyDetailDto>>> GetPropertiesDropdown()
        {
           var propertyDropdownItems = await _context.Properties
                .OrderByDescending(p => p.RegistrationDate)
                .Take(10)
                .Select(p => new DropdownDto
                {
                    Title = p.PropertyName + " (" + p.PropertyId + ")",
                    Value = p.PropertyId.ToString()
                })
                .ToListAsync();
            propertyDropdownItems.Add(new DropdownDto
            {
                Title = "Other",
                Value = "Other"
            });

            return Ok(propertyDropdownItems);
        }
    }
}

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

        /*[HttpGet]
        public async Task<ActionResult<List<Property>>> GetProperties()
        {
            return Ok(await _context.Properties.ToListAsync());
        }*/

        /*[HttpGet]
        public async Task<ActionResult<IEnumerable<PropertyDetailDto>>> GetProperties()
        {*/
            /*var properties = await _context.Properties
                .Include(p => p.PropertyOwnerships)
                    .ThenInclude(o => o.Contact)
                .Include(p => p.PriceChanges)
                .Select(p => new PropertyDetailDto
                {
                    PropertyId = p.PropertyId,
                    PropertyName = p.PropertyName,
                    AskingPrice = (decimal)p.Price,
                    Owner = p.PropertyOwnerships.OrderByDescending(o => o.EffectiveFrom).FirstOrDefault() != null ?
                            p.PropertyOwnerships.OrderByDescending(o => o.EffectiveFrom).FirstOrDefault().Contact.FirstName + " " +
                            p.PropertyOwnerships.OrderByDescending(o => o.EffectiveFrom).FirstOrDefault().Contact.LastName : null,
                    PurchaseDate = p.PropertyOwnerships.OrderByDescending(o => o.EffectiveFrom).FirstOrDefault() != null ?
                            p.PropertyOwnerships.OrderByDescending(o => o.EffectiveFrom).FirstOrDefault().EffectiveFrom : null,
                    SoldPrice = p.PropertyOwnerships.OrderByDescending(o => o.EffectiveFrom).FirstOrDefault() != null ?
                        p.PropertyOwnerships.OrderByDescending(o => o.EffectiveFrom).FirstOrDefault().AcquisitionPrice : null
                })
                .ToListAsync();*/

            /*var properties = await _context.Properties
                .Include(p => p.PropertyOwnerships)
                .ThenInclude(po => po.Contact)
                .Select(p => new PropertyDetailDto
                {
                    PropertyId = p.PropertyId,
                    PropertyName = p.PropertyName,
                    AskingPrice = (decimal)p.Price,
                    Owner = p.PropertyOwnerships
                    .OrderByDescending(po => po.EffectiveFrom) // Assuming the last ownership is the current/last
                    .Select(po => po.Contact.FirstName + " " + po.Contact.LastName)
                    .FirstOrDefault(),
                    PurchaseDate = p.PropertyOwnerships
                    .OrderByDescending(po => po.EffectiveFrom)
                    .Select(po => po.EffectiveFrom)
                    .FirstOrDefault(),
                    SoldPrice = p.PropertyOwnerships
                    .OrderByDescending(po => po.EffectiveFrom)
                    .Select(po => po.AcquisitionPrice)
                    .FirstOrDefault()
                })
                .ToListAsync();

            return Ok(properties);
        }*/

        [HttpGet]
        public async Task<ActionResult<List<PropertyDetailDto>>> GetProperties()
        {
            return Ok(await _context.Properties.ToListAsync());
        }

        /*[HttpPost]
        public async Task<ActionResult<List<Property>>> CreateProperty(Property property)
        {
            _context.Properties.Add(property);
            await _context.SaveChangesAsync();

            return Ok(await _context.Properties.ToListAsync());
        }*/

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

            //return Ok(await _context.Properties.ToListAsync());
            // Fetch updated list and map to DTO
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
            //return Ok(CreatedAtAction("GetActualProperty", new { id = property.PropertyId }, property));
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

        [HttpPost("{id}/change-price")]
        public async Task<IActionResult> ChangePropertyPrice(Guid id, [FromBody] PriceChangeDto priceChangeDto)
        {
            // Retrieve the property from the database
            var property = await _context.Properties.FindAsync(id);
            if (property == null)
            {
                return NotFound();
            }

            // Update the property's current price
            property.Price = (float)priceChangeDto.NewPrice;
            _context.Properties.Update(property);

            // Add a new price change record
            var priceChange = new PropertyPriceChange
            {
                PropertyId = id,
                NewPrice = priceChangeDto.NewPrice,
                ChangeDate = priceChangeDto.ChangeDate ?? DateTime.UtcNow  // Use the provided date or the current date
            };
            _context.PropertyPriceChange.Add(priceChange);

            // Save changes to the database
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("propertiesDropdown")]
        public async Task<ActionResult<List<PropertyDetailDto>>> GetPropertiesDropdown()
        {
            //return Ok(await _context.Properties.ToListAsync());
            // Query the database and project each Property into a formatted string
            /*var propertyDropdownItems = await _context.Properties
                .OrderByDescending(p => p.RegistrationDate)
                .Take(10)
                .Select(p => p.PropertyName + " (" + p.PropertyId + ")")
                .ToListAsync();*/

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

        /*[HttpPut("{id}")]
        public async Task<IActionResult> UpdateActualProperty(Guid id, [FromBody] PropertyDto propertyDto)
        {
            var property = await _context.Properties.FindAsync(id);
            if (property == null)
            {
                return NotFound();
            }

            property.PropertyName = propertyDto.PropertyName;
            property.PropertyAddress = propertyDto.PropertyAddress;
            property.Price = (float)propertyDto.Price;
            // Assume you don't update the registration date

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActualProperty(Guid id)
        {
            var property = await _context.Properties.FindAsync(id);
            if (property == null)
            {
                return NotFound();
            }

            _context.Properties.Remove(property);
            await _context.SaveChangesAsync();
            return NoContent();
        }*/

        /*[HttpPost]
        public async Task<IActionResult> CreateProperty([FromBody] PropertyDetailDto propertyDto)
        {
            var property = new Property
            {
                PropertyName = propertyDto.PropertyName,
                PropertyAddress = propertyDto.PropertyAddress,
                Price = propertyDto.AskingPrice,
                RegistrationDate = propertyDto.RegistrationDate,
                PropertyOwnerships = propertyDto.Ownerships.Select(o => new PropertyOwnership
                {
                    ContactId = o.ContactId,
                    EffectiveFrom = o.EffectiveFrom,
                    EffectiveTill = o.EffectiveTill,
                    AcquisitionPrice = o.AcquisitionPrice
                }).ToList(),
                PriceChanges = propertyDto.PriceChanges.Select(pc => new PriceChange
                {
                    NewPrice = pc.NewPrice,
                    ChangeDate = pc.ChangeDate
                }).ToList()
            };

            _context.Properties.Add(property);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProperty), new { id = property.PropertyId }, property);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PropertyDetailDto>> GetProperty(Guid id)
        {
            var property = await _context.Properties
                .Where(p => p.PropertyId == id)
                .Include(p => p.PropertyOwnerships)
                    .ThenInclude(po => po.Contact)
                .Include(p => p.PriceChanges)
                .Select(p => new PropertyDetailDto
                {
                    PropertyId = p.PropertyId,
                    PropertyName = p.PropertyName,
                    PropertyAddress = p.PropertyAddress,
                    AskingPrice = p.Price,
                    DateOfRegistration = p.DateOfRegistration,
                    Ownerships = p.PropertyOwnerships.Select(po => new OwnershipDetailDto
                    {
                        ContactId = po.ContactId,
                        EffectiveFrom = po.EffectiveFrom,
                        EffectiveTill = po.EffectiveTill,
                        AcquisitionPrice = po.AcquisitionPrice
                    }).ToList(),
                    PriceChanges = p.PriceChanges.Select(pc => new PriceChangeDetailDto
                    {
                        NewPrice = pc.NewPrice,
                        ChangeDate = pc.ChangeDate
                    }).ToList()
                }).FirstOrDefaultAsync();

            if (property == null)
            {
                return NotFound();
            }
            return property;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProperty(Guid id, [FromBody] PropertyDetailDto propertyDto)
        {
            var property = await _context.Properties
                .Include(p => p.PropertyOwnerships)
                .Include(p => p.PriceChanges)
                .FirstOrDefaultAsync(p => p.PropertyId == id);

            if (property == null)
            {
                return NotFound();
            }

            // Update property basic info
            property.PropertyName = propertyDto.PropertyName;
            property.PropertyAddress = propertyDto.PropertyAddress;
            property.Price = (float)propertyDto.AskingPrice;
            // Update or Add new ownerships and price changes as needed
            // Similar to the create, but includes checks for existing entries and removal of old entries

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProperty(Guid id)
        {
            var property = await _context.Properties.FindAsync(id);
            if (property == null)
            {
                return NotFound();
            }

            _context.Properties.Remove(property);
            await _context.SaveChangesAsync();
            return NoContent();
        }*/

        /*[HttpPut("{propertyId}")]
        public async Task<IActionResult> UpdatePropertyDetails(Guid propertyId, [FromBody] CombinedPropertyDetailDto details)
        {
            // Simplified for demonstration. Implement checks and updates for each ownership and price change.
            return NoContent();
        }

        [HttpDelete("ownership/{ownershipId}")]
        public async Task<IActionResult> DeleteOwnership(int ownershipId)
        {
            // Simplified for demonstration. Implement the actual deletion logic.
            return NoContent();
        }

        [HttpDelete("priceChange/{priceChangeId}")]
        public async Task<IActionResult> DeletePriceChange(int priceChangeId)
        {
            // Simplified for demonstration. Implement the actual deletion logic.
            return NoContent();
        }*/
    }
}

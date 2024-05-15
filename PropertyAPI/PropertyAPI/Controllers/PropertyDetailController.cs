using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PropertyAPI.Data;

namespace PropertyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyDetailController : ControllerBase
    {
        private readonly DataContext _context;

        public PropertyDetailController(DataContext context)
        {
            _context = context;
        }

        /*[HttpPost]
        public async Task<IActionResult> AddPropertyDetails([FromBody] CombinedPropertyDetailDto details)
        {
            // Check if property exists
            var property = await _context.Properties.FindAsync(details.PropertyId);
            if (property == null)
                return NotFound("Property not found.");

            foreach (var ownership in details.Ownerships)
            {
                _context.PropertyOwnership.Add(new PropertyOwnership
                {
                    PropertyId = details.PropertyId,
                    ContactId = ownership.ContactId,
                    EffectiveFrom = ownership.EffectiveFrom,
                    EffectiveTill = ownership.EffectiveTill ?? DateTime.Now,
                    AcquisitionPrice = ownership.AcquisitionPrice
                });
            }

            foreach (var change in details.PriceChanges)
            {
                _context.PropertyPriceChange.Add(new PropertyPriceChange
                {
                    PropertyId = details.PropertyId,
                    NewPrice = change.NewPrice,
                    ChangeDate = change.ChangeDate
                });
            }

            await _context.SaveChangesAsync();
            return CreatedAtAction("GetPropertyDetails", new { propertyId = details.PropertyId }, details);
        }

        [HttpGet("{propertyId}")]
        public async Task<ActionResult<CombinedPropertyDetailDto>> GetPropertyDetails(Guid propertyId)
        {
            var property = await _context.Properties
                .Where(p => p.PropertyId == propertyId)
                .Include(p => p.PropertyOwnerships)
                .ThenInclude(po => po.Contact)
                .Include(p => p.PriceChanges)
                .FirstOrDefaultAsync();

            if (property == null)
                return NotFound("Property not found.");

            var result = new CombinedPropertyDetailDto
            {
                PropertyId = property.PropertyId,
                Ownerships = property.PropertyOwnerships.Select(po => new OwnershipDetailDto
                {
                    PropertyId = po.PropertyId,
                    ContactId = po.ContactId,
                    EffectiveFrom = po.EffectiveFrom,
                    EffectiveTill = po.EffectiveTill,
                    AcquisitionPrice = po.AcquisitionPrice
                }).ToList(),
                PriceChanges = property.PriceChanges.Select(pc => new PriceChangeDto
                {
                    PropertyId = pc.PropertyId,
                    NewPrice = pc.NewPrice,
                    ChangeDate = pc.ChangeDate
                }).ToList()
            };

            return result;
        }*/

        [HttpGet("all-details")]
        public async Task<ActionResult<IEnumerable<PropertyDetailDto>>> GetAllPropertyDetails()
        {
            var properties = await _context.Properties
                .Include(p => p.PropertyOwnerships)
                    .ThenInclude(po => po.Contact)
                .Include(p => p.PriceChanges)
                .Select(p => new PropertyDetailDto
                {
                    PropertyId = p.PropertyId,
                    PropertyName = p.PropertyName,
                    //PropertyAddress = p.PropertyAddress,
                    AskingPrice = (decimal)p.Price,
                    //RegistrationDate = p.RegistrationDate,
                    Ownerships = p.PropertyOwnerships.Select(po => new OwnershipDetailDto
                    {
                        PropertyId = po.PropertyId,
                        ContactId = po.ContactId,
                        EffectiveFrom = po.EffectiveFrom,
                        EffectiveTill = po.EffectiveTill,
                        AcquisitionPrice = po.AcquisitionPrice,
                        //ContactName = po.Contact.FirstName + " " + po.Contact.LastName // assuming you want the contact's name here
                    }).ToList(),
                    PriceChanges = p.PriceChanges.Select(pc => new PriceChangeDto
                    {
                        PropertyId = pc.PropertyId,
                        NewPrice = pc.NewPrice,
                        ChangeDate = pc.ChangeDate
                    }).ToList()
                }).ToListAsync();

            return Ok(properties);
        }
    }
}

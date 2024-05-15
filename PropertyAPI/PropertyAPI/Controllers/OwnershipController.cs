using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PropertyAPI.Data;

namespace PropertyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnershipController : ControllerBase
    {
        private readonly DataContext _context;

        public OwnershipController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CombinedPropertyDetailDto>>> GetOwnerships()
        {
            /*var details = await _context.Properties.Include(p => p.PropertyOwnerships)
                .ThenInclude(po => po.Contact)
                .Select(p => new CombinedPropertyDetailDto
                {
                    Id = p.PropertyOwnerships
                        .Select(po => po.Id).FirstOrDefault(),
                    PropertyId = p.PropertyId,
                    PropertyName = p.PropertyName,
                    AskingPrice = (decimal)p.Price,
                    Owner = p.PropertyOwnerships.OrderByDescending(po => po.EffectiveFrom)
                        .Select(po => po.Contact.FirstName + " " + po.Contact.LastName).FirstOrDefault(),
                    DateOfPurchase = p.PropertyOwnerships.OrderByDescending(po => po.EffectiveFrom)
                        .Select(po => po.EffectiveFrom).FirstOrDefault(),
                    SoldPriceInEur = p.PropertyOwnerships.OrderByDescending(po => po.EffectiveFrom)
                        .Select(po => po.AcquisitionPrice).FirstOrDefault(),
                    SoldPriceInUsd = p.PropertyOwnerships.OrderByDescending(po => po.EffectiveFrom)
                        .Select(po => po.AcquisitionPrice * 1.2M).FirstOrDefault() // Example currency conversion
                }).ToListAsync();*/

            /*var details = await _context.Properties.Include(p => p.PropertyOwnerships)
                .ThenInclude(po => po.Contact)
                .Select(p => new
                {
                    Property = p,
                    Owner = p.PropertyOwnerships.OrderByDescending(po => po.EffectiveFrom)
                        .Select(po => new { po.Contact.FirstName, po.Contact.LastName }).FirstOrDefault()
                })
                .Where(x => !string.IsNullOrEmpty(x.Owner.FirstName) && !string.IsNullOrEmpty(x.Owner.LastName)) // Filter out empty owners
                .Select(x => new CombinedPropertyDetailDto
                {
                    Id = x.Property.PropertyOwnerships
                        .Select(po => po.Id).FirstOrDefault(),
                    PropertyId = x.Property.PropertyId,
                    PropertyName = x.Property.PropertyName,
                    AskingPrice = (decimal)x.Property.Price,
                    Owner = x.Owner.FirstName + " " + x.Owner.LastName,
                    DateOfPurchase = x.Property.PropertyOwnerships.OrderByDescending(po => po.EffectiveFrom)
                        .Select(po => po.EffectiveFrom).FirstOrDefault(),
                    SoldPriceInEur = x.Property.PropertyOwnerships.OrderByDescending(po => po.EffectiveFrom)
                        .Select(po => po.AcquisitionPrice).FirstOrDefault(),
                    SoldPriceInUsd = x.Property.PropertyOwnerships.OrderByDescending(po => po.EffectiveFrom)
                        .Select(po => po.AcquisitionPrice * 1.2M).FirstOrDefault()
                }).ToListAsync();*/

            var details = await _context.PropertyOwnership
                .Include(po => po.Property)
                    .ThenInclude(p => p.PropertyOwnerships)
                        .ThenInclude(po => po.Contact)
                .Select(po => new CombinedPropertyDetailDto
                {
                    Id = po.Id,
                    PropertyId = po.PropertyId,
                    PropertyName = po.Property.PropertyName,
                    AskingPrice = (decimal)po.Property.Price,
                    Owner = po.Contact.FirstName + " " + po.Contact.LastName,
                    DateOfPurchase = po.EffectiveFrom,
                    SoldPriceInEur = po.AcquisitionPrice,
                    SoldPriceInUsd = po.AcquisitionPrice * 1.2M
                }).ToListAsync();

            return Ok(details);
        }

        [HttpPost]
        public async Task<IActionResult> AddOwnership([FromBody] OwnershipDetailDto ownershipDto)
        {
            var ownership = new PropertyOwnership
            {
                PropertyId = ownershipDto.PropertyId,
                ContactId = ownershipDto.ContactId,
                EffectiveFrom = ownershipDto.EffectiveFrom,
                EffectiveTill = ownershipDto.EffectiveTill ?? DateTime.Now,
                AcquisitionPrice = ownershipDto.AcquisitionPrice
            };

            _context.PropertyOwnership.Add(ownership);
            await _context.SaveChangesAsync();
            //return CreatedAtAction("GetOwnership", new { id = ownership.Id }, ownership);
            //return Ok(await _context.Properties.ToListAsync());
            /*var details = await _context.Properties.Include(p => p.PropertyOwnerships)
                .ThenInclude(po => po.Contact)
                .Select(p => new
                {
                    Property = p,
                    Owner = p.PropertyOwnerships.OrderByDescending(po => po.EffectiveFrom)
                        .Select(po => new { po.Contact.FirstName, po.Contact.LastName }).FirstOrDefault()
                })
                .Where(x => !string.IsNullOrEmpty(x.Owner.FirstName) && !string.IsNullOrEmpty(x.Owner.LastName)) // Filter out empty owners
                .Select(x => new CombinedPropertyDetailDto
                {
                    Id = x.Property.PropertyOwnerships
                        .Select(po => po.Id).FirstOrDefault(),
                    PropertyId = x.Property.PropertyId,
                    PropertyName = x.Property.PropertyName,
                    AskingPrice = (decimal)x.Property.Price,
                    Owner = x.Owner.FirstName + " " + x.Owner.LastName,
                    DateOfPurchase = x.Property.PropertyOwnerships.OrderByDescending(po => po.EffectiveFrom)
                        .Select(po => po.EffectiveFrom).FirstOrDefault(),
                    SoldPriceInEur = x.Property.PropertyOwnerships.OrderByDescending(po => po.EffectiveFrom)
                        .Select(po => po.AcquisitionPrice).FirstOrDefault(),
                    SoldPriceInUsd = x.Property.PropertyOwnerships.OrderByDescending(po => po.EffectiveFrom)
                        .Select(po => po.AcquisitionPrice * 1.2M).FirstOrDefault()
                }).ToListAsync();*/

            var details = await _context.PropertyOwnership
                .Include(po => po.Property)
                    .ThenInclude(p => p.PropertyOwnerships)
                        .ThenInclude(po => po.Contact)
                .Select(po => new CombinedPropertyDetailDto
                {
                    Id = po.Id,
                    PropertyId = po.PropertyId,
                    PropertyName = po.Property.PropertyName,
                    AskingPrice = (decimal)po.Property.Price,
                    Owner = po.Contact.FirstName + " " + po.Contact.LastName,
                    DateOfPurchase = po.EffectiveFrom,
                    SoldPriceInEur = po.AcquisitionPrice,
                    SoldPriceInUsd = po.AcquisitionPrice * 1.2M
                }).ToListAsync();

            return Ok(details);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PropertyOwnership>> GetOwnership(int id)
        {
            var ownership = await _context.PropertyOwnership
                .Include(o => o.Contact)
                .Include(o => o.Property)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (ownership == null)
            {
                return NotFound();
            }

            return ownership;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOwnership(OwnershipDetailDto ownershipDto)
        {
            var ownership = await _context.PropertyOwnership.FindAsync(ownershipDto.Id);
            if (ownership == null)
            {
                return NotFound();
            }

            //ownership.ContactId = ownershipDto.ContactId;
            ownership.EffectiveFrom = ownershipDto.EffectiveFrom;
            ownership.EffectiveTill = ownershipDto.EffectiveTill ?? DateTime.Now;
            ownership.AcquisitionPrice = ownershipDto.AcquisitionPrice;

            await _context.SaveChangesAsync();
            //return NoContent();
            var details = await _context.PropertyOwnership
                .Include(po => po.Property)
                    .ThenInclude(p => p.PropertyOwnerships)
                        .ThenInclude(po => po.Contact)
                .Select(po => new CombinedPropertyDetailDto
                {
                    Id = po.Id,
                    PropertyId = po.PropertyId,
                    PropertyName = po.Property.PropertyName,
                    AskingPrice = (decimal)po.Property.Price,
                    Owner = po.Contact.FirstName + " " + po.Contact.LastName,
                    DateOfPurchase = po.EffectiveFrom,
                    SoldPriceInEur = po.AcquisitionPrice,
                    SoldPriceInUsd = po.AcquisitionPrice * 1.2M
                }).ToListAsync();

            return Ok(details);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOwnership(int id)
        {
            var ownership = await _context.PropertyOwnership.FindAsync(id);
            if (ownership == null)
            {
                return NotFound();
            }

            _context.PropertyOwnership.Remove(ownership);
            await _context.SaveChangesAsync();
            //return NoContent();
            var details = await _context.PropertyOwnership
                .Include(po => po.Property)
                    .ThenInclude(p => p.PropertyOwnerships)
                        .ThenInclude(po => po.Contact)
                .Select(po => new CombinedPropertyDetailDto
                {
                    Id = po.Id,
                    PropertyId = po.PropertyId,
                    PropertyName = po.Property.PropertyName,
                    AskingPrice = (decimal)po.Property.Price,
                    Owner = po.Contact.FirstName + " " + po.Contact.LastName,
                    DateOfPurchase = po.EffectiveFrom,
                    SoldPriceInEur = po.AcquisitionPrice,
                    SoldPriceInUsd = po.AcquisitionPrice * 1.2M
                }).ToListAsync();

            return Ok(details);
        }
    }
}

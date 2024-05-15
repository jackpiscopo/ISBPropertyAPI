using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PropertyAPI.Data;
using PropertyAPI.Models;

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

            ownership.EffectiveFrom = ownershipDto.EffectiveFrom;
            ownership.EffectiveTill = ownershipDto.EffectiveTill ?? DateTime.Now;
            ownership.AcquisitionPrice = ownershipDto.AcquisitionPrice;

            await _context.SaveChangesAsync();
            
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

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PropertyAPI.Data;

namespace PropertyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CombinedDetailsNewController : ControllerBase
    {
        private readonly DataContext _context;

        public CombinedDetailsNewController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CombinedPropertyDetailDto>>> GetAllCombinedDetails()
        {
            var details = await _context.Properties.Include(p => p.PropertyOwnerships)
                .ThenInclude(po => po.Contact)
                .Select(p => new CombinedPropertyDetailDto
                {
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
                }).ToListAsync();

            return Ok(details);
        }
    }
}

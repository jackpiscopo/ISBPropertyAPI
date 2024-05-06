using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PropertyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Property>>> GetProperties()
        {
            return new List<Property>
            { 
                new Property
                {
                    PropertyName = "3 Bedroom Apartment",
                    PropertyAddress = "1 Road",
                    Price = 300000f,
                    RegistrationDate = new DateTime(new DateOnly(2024, 3, 31), new TimeOnly(0))
                }
            };
        }
    }
}

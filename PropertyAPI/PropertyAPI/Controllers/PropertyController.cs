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
                    PropertyAddress = "3 Road",
                    Price = 300000f,
                    RegistrationDate = DateTime.Today
                }
            };
        }
    }
}

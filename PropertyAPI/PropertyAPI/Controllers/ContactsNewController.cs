using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PropertyAPI.Data;

namespace PropertyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsNewController : ControllerBase
    {
        private readonly DataContext _context;

        public ContactsNewController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateContact([FromBody] ContactDto contactDto)
        {
            var contact = new Contact
            {
                FirstName = contactDto.FirstName,
                LastName = contactDto.LastName
            };

            _context.Contact.Add(contact);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetContact), new { id = contact.Id }, contact);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> GetContact(Guid id)
        {
            var contact = await _context.Contact.FindAsync(id);
            if (contact == null) return NotFound();
            return contact;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact(Guid id, [FromBody] ContactDto contactDto)
        {
            var contact = await _context.Contact.FindAsync(id);
            if (contact == null) return NotFound();

            contact.FirstName = contactDto.FirstName;
            contact.LastName = contactDto.LastName;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(Guid id)
        {
            var contact = await _context.Contact.FindAsync(id);
            if (contact == null) return NotFound();

            _context.Contact.Remove(contact);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PropertyAPI.Data;
using PropertyAPI.Models;

namespace PropertyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly DataContext _context;

        public ContactController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Contact>>> GetContacts()
        {
            return Ok(await _context.Contact.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<List<Contact>>> AddContact([FromBody] ContactDto contactDto)
        {
            var contact = new Contact
            {
                FirstName = contactDto.FirstName,
                LastName = contactDto.LastName,
                PhoneNumber = contactDto.PhoneNumber,
                EmailAddress = contactDto.EmailAddress
            };

            _context.Contact.Add(contact);
            await _context.SaveChangesAsync();
            return Ok(await _context.Contact.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> GetContact(Guid id)
        {
            var contact = await _context.Contact.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            return contact;
        }

        [HttpPut]
        public async Task<ActionResult<List<ContactDto>>> UpdateContact(ContactDto contactDto)
        {
            var contact = await _context.Contact.FindAsync(contactDto.Id);
            if (contact == null)
            {
                return NotFound();
            }

            contact.FirstName = contactDto.FirstName;
            contact.LastName = contactDto.LastName;
            contact.PhoneNumber = contactDto.PhoneNumber;
            contact.EmailAddress = contactDto.EmailAddress;

            await _context.SaveChangesAsync();
            return Ok(await _context.Contact.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Contact>>> DeleteContact(Guid id)
        {
            var contact = await _context.Contact.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            _context.Contact.Remove(contact);
            await _context.SaveChangesAsync();
            return Ok(await _context.Contact.ToListAsync());
        }

        [HttpGet("contactsDropdown")]
        public async Task<ActionResult<List<ContactDto>>> GetContactsDropdown()
        {
            var contactDropdownItems = await _context.Contact
                .Take(10)
                .Select(p => new DropdownDto
                {
                    Title = p.FirstName + " " + p.LastName + " (" + p.Id + ")",
                    Value = p.Id.ToString()
                })
                .ToListAsync();
            contactDropdownItems.Add(new DropdownDto
            {
                Title = "Other",
                Value = "Other"
            });

            return Ok(contactDropdownItems);
        }
    }
}

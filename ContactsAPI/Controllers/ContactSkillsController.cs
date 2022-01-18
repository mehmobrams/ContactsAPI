#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContactsAPI.Data;
using ContactsAPI.Models;

namespace ContactsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactSkillsController : ControllerBase
    {
        private readonly ContactsdbContext _context;

        public ContactSkillsController(ContactsdbContext context)
        {
            _context = context;
        }

        // GET: api/ContactSkills
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactSkill>>> GetContactSkills()
        {
            return await _context.ContactSkills.ToListAsync();
        }

        // GET: api/ContactSkills/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactSkill>> GetContactSkill(int id)
        {
            var contactSkill = await _context.ContactSkills.FindAsync(id);

            if (contactSkill == null)
            {
                return NotFound();
            }

            return contactSkill;
        }

        // PUT: api/ContactSkills/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContactSkill(int id, ContactSkill contactSkill)
        {
            if (id != contactSkill.ContactId)
            {
                return BadRequest();
            }

            _context.Entry(contactSkill).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactSkillExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ContactSkills
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ContactSkill>> PostContactSkill(ContactSkill contactSkill)
        {
            _context.ContactSkills.Add(contactSkill);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ContactSkillExists(contactSkill.ContactId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetContactSkill", new { id = contactSkill.ContactId }, contactSkill);
        }

        // DELETE: api/ContactSkills/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContactSkill(int id)
        {
            var contactSkill = await _context.ContactSkills.FindAsync(id);
            if (contactSkill == null)
            {
                return NotFound();
            }

            _context.ContactSkills.Remove(contactSkill);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContactSkillExists(int id)
        {
            return _context.ContactSkills.Any(e => e.ContactId == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebCore_5._0.Data;
using WebCore_5._0.Entities.School;

namespace WebCore_5._0.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrouppsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GrouppsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Groupps
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Groupp>>> GetGroups()
        {
            return await _context.Groups.ToListAsync();
        }

        // GET: api/Groupps/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Groupp>> GetGroupp(int id)
        {
            var groupp = await _context.Groups.FindAsync(id);

            if (groupp == null)
            {
                return NotFound();
            }

            return groupp;
        }

        // PUT: api/Groupps/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGroupp(int id, Groupp groupp)
        {
            if (id != groupp.Id)
            {
                return BadRequest();
            }

            _context.Entry(groupp).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GrouppExists(id))
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

        // POST: api/Groupps
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Groupp>> PostGroupp(Groupp groupp)
        {
            _context.Groups.Add(groupp);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGroupp", new { id = groupp.Id }, groupp);
        }

        // DELETE: api/Groupps/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroupp(int id)
        {
            var groupp = await _context.Groups.FindAsync(id);
            if (groupp == null)
            {
                return NotFound();
            }

            _context.Groups.Remove(groupp);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GrouppExists(int id)
        {
            return _context.Groups.Any(e => e.Id == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeCapture.Context;
using TimeCapture.Models;

namespace TimeCapture.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MattersController : ControllerBase
    {
        private readonly TimeRecordingContext _context;

        public MattersController(TimeRecordingContext context)
        {
            _context = context;
        }

        // GET: api/Matters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Matter>>> GetMatters()
        {
            return await _context.Matters.ToListAsync();
        }

        // GET: api/Matters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Matter>> GetMatter(int id)
        {
            var matter = await _context.Matters.FindAsync(id);

            if (matter == null)
            {
                return NotFound();
            }

            return matter;
        }

        // PUT: api/Matters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMatter(int id, Matter matter)
        {
            if (id != matter.Id)
            {
                return BadRequest();
            }

            _context.Entry(matter).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatterExists(id))
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

        // POST: api/Matters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Matter>> PostMatter(Matter matter)
        {
            _context.Matters.Add(matter);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMatter", new { id = matter.Id }, matter);
        }

        // DELETE: api/Matters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatter(int id)
        {
            var matter = await _context.Matters.FindAsync(id);
            if (matter == null)
            {
                return NotFound();
            }

            _context.Matters.Remove(matter);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MatterExists(int id)
        {
            return _context.Matters.Any(e => e.Id == id);
        }
    }
}

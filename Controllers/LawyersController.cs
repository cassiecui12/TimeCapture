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
    public class LawyersController : ControllerBase
    {
        private readonly TimeRecordingContext _context;

        public LawyersController(TimeRecordingContext context)
        {
            _context = context;
        }

        // GET: api/Lawyers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lawyer>>> GetLawyers()
        {
            return await _context.Lawyers.ToListAsync();
        }

        // GET: api/Lawyers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Lawyer>> GetLawyer(int id)
        {
            var lawyer = await _context.Lawyers.FindAsync(id);

            if (lawyer == null)
            {
                return NotFound();
            }

            return lawyer;
        }

        // PUT: api/Lawyers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLawyer(int id, Lawyer lawyer)
        {
            if (id != lawyer.Id)
            {
                return BadRequest();
            }

            _context.Entry(lawyer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LawyerExists(id))
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

        // POST: api/Lawyers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Lawyer>> PostLawyer(Lawyer lawyer)
        {
            _context.Lawyers.Add(lawyer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLawyer", new { id = lawyer.Id }, lawyer);
        }

        // DELETE: api/Lawyers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLawyer(int id)
        {
            var lawyer = await _context.Lawyers.FindAsync(id);
            if (lawyer == null)
            {
                return NotFound();
            }

            _context.Lawyers.Remove(lawyer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LawyerExists(int id)
        {
            return _context.Lawyers.Any(e => e.Id == id);
        }
    }
}

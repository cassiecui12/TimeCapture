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
    public class TimeRecordingsController : ControllerBase
    {
        private readonly TimeRecordingContext _context;

        public TimeRecordingsController(TimeRecordingContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        // GET: api/TimeRecordings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TimeRecording>>> GetTimeRecordings()
        {
            return await _context.TimeRecordings.ToListAsync();
        }

        // GET: api/TimeRecordings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TimeRecording>> GetTimeRecording(int id)
        {
            var timeRecording = await _context.TimeRecordings.FindAsync(id);

            if (timeRecording == null)
            {
                return NotFound();
            }

            return timeRecording;
        }

        // PUT: api/TimeRecordings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTimeRecording(int id, TimeRecording timeRecording)
        {
            if (id != timeRecording.Id)
            {
                return BadRequest();
            }

            _context.Entry(timeRecording).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TimeRecordingExists(id))
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

        // POST: api/TimeRecordings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TimeRecording>> PostTimeRecording(TimeRecording timeRecording)
        {
            _context.TimeRecordings.Add(timeRecording);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTimeRecording", new { id = timeRecording.Id }, timeRecording);
        }

        [HttpPost("/{clientId:int}/{lawyerId:int}/{matterId:int}/{categoryId:int}/{description}/{to}/{from}")]
        public async Task<ActionResult<TimeRecording>> PostTimeRecording2(int clientId, int lawyerId, int matterId, int categoryId, string description, string to, string from)
        {

            Client client = _context.Clients.Where(client => client.Id == clientId).FirstOrDefault();
            Lawyer lawyer = _context.Lawyers.Where(lawyer => lawyer.Id == lawyerId).FirstOrDefault();
            Matter matter = _context.Matters.Where(matter => matter.Id == matterId).FirstOrDefault();


            TimeRecording timeRecording = new TimeRecording
            {
                StartDate = DateTime.Parse(to),
                EndDate = DateTime.Parse(from),
                Client = client,
                Lawyer = lawyer,
                Matter = matter,
                Category = (Category)categoryId,
                Description = description
            };
            _context.TimeRecordings.Add(timeRecording);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTimeRecording", new { id = timeRecording.Id }, timeRecording);
        }

        // DELETE: api/TimeRecordings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTimeRecording(int id)
        {
            var timeRecording = await _context.TimeRecordings.FindAsync(id);
            if (timeRecording == null)
            {
                return NotFound();
            }

            _context.TimeRecordings.Remove(timeRecording);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TimeRecordingExists(int id)
        {
            return _context.TimeRecordings.Any(e => e.Id == id);
        }
    }
}

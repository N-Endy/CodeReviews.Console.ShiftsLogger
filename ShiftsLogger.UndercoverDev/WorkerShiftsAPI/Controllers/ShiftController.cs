using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkerShiftsAPI.Models;

namespace WorkerShiftsAPI.Controllers
{
    [Route("api/shifts")]
    [ApiController]
    public class ShiftController : ControllerBase
    {
        private readonly WorkerShiftContext _context;

        public ShiftController(WorkerShiftContext context)
        {
            _context = context;
        }

        // GET: api/Shift
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shift>>> GetShifts()
        {
            return await _context.Shifts.ToListAsync();
        }

        // GET: api/Shift/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Shift>> GetShift(int id)
        {
            var shift = await _context.Shifts.FindAsync(id);

            if (shift == null)
            {
                return NotFound();
            }

            return shift;
        }

        // PUT: api/Shift/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShift(int id, Shift shift)
        {
            if (id != shift.ShiftId)
            {
                return BadRequest();
            }

            _context.Entry(shift).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShiftExists(id))
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

        // POST: api/Shift
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Shift>> PostShift(Shift shift)
        {
            _context.Entry(shift.Worker).State = EntityState.Unchanged;
            _context.Shifts.Add(shift);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetShift), new { id = shift.ShiftId }, shift);
        }

        // DELETE: api/Shift/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShift(int id)
        {
            var shift = await _context.Shifts.FindAsync(id);
            if (shift == null)
            {
                return NotFound();
            }

            _context.Shifts.Remove(shift);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShiftExists(int id)
        {
            return _context.Shifts.Any(e => e.ShiftId == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ArtGalleryAPI.Models;

namespace ArtGalleryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtController : ControllerBase
    {
        private readonly ACE42023Context _context;

        public ArtController(ACE42023Context context)
        {
            _context = context;
        }

        // GET: api/Art
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Painting>>> GetPaintings()
        {
            return await _context.Paintings.ToListAsync();
        }

        // GET: api/Art/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Painting>> GetPainting(int id)
        {
            var painting = await _context.Paintings.FindAsync(id);

            if (painting == null)
            {
                return NotFound();
            }

            return painting;
        }

        // PUT: api/Art/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPainting(int id, Painting painting)
        {
            if (id != painting.Pid)
            {
                return BadRequest();
            }

            _context.Entry(painting).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaintingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(painting);
        }

        // POST: api/Art
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Painting>> PostPainting(Painting painting)
        {
            _context.Paintings.Add(painting);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PaintingExists(painting.Pid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetPainting", new { id = painting.Pid }, painting);
        }

        // DELETE: api/Art/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePainting(int id)
        {
            var painting = await _context.Paintings.FindAsync(id);
            if (painting == null)
            {
                return NotFound();
            }

            _context.Paintings.Remove(painting);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PaintingExists(int id)
        {
            return _context.Paintings.Any(e => e.Pid == id);
        }
    }
}

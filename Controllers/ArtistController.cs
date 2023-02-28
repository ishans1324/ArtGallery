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
    public class ArtistController : ControllerBase
    {
        private readonly ACE42023Context _context;

        public ArtistController(ACE42023Context context)
        {
            _context = context;
        }

        // GET: api/Artist
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Artistishan>>> GetArtistishans()
        {
            return await _context.Artistishans.ToListAsync();
        }

        // GET: api/Artist/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Artistishan>> GetArtistishan(int id)
        {
            var artistishan = await _context.Artistishans.FindAsync(id);

            if (artistishan == null)
            {
                return NotFound();
            }

            return artistishan;
        }

        // PUT: api/Artist/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtistishan(int id, Artistishan artistishan)
        {
            if (id != artistishan.Aid)
            {
                return BadRequest();
            }

            _context.Entry(artistishan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArtistishanExists(id))
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

        // POST: api/Artist
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Artistishan>> PostArtistishan(Artistishan artistishan)
        {
            _context.Artistishans.Add(artistishan);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ArtistishanExists(artistishan.Aid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetArtistishan", new { id = artistishan.Aid }, artistishan);
        }

        // DELETE: api/Artist/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtistishan(int id)
        {
            var artistishan = await _context.Artistishans.FindAsync(id);
            if (artistishan == null)
            {
                return NotFound();
            }

            _context.Artistishans.Remove(artistishan);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ArtistishanExists(int id)
        {
            return _context.Artistishans.Any(e => e.Aid == id);
        }
    }
}

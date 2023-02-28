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

    public class RegisterException:ApplicationException
    {
        public RegisterException(string message):base(message)
        {

        }
    }



    [Route("api/[controller]")]
    [ApiController]
    public class ArtUserController : ControllerBase
    {
        private readonly ACE42023Context _context;

        public ArtUserController(ACE42023Context context)
        {
            _context = context;
        }

        // GET: api/ArtUser
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArtUser>>> GetArtUsers()
        {
            return await _context.ArtUsers.ToListAsync();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<ArtUser>> Login(ArtUser a)
        {
            if(a.Uname==""||a.Password=="")
            {
                return BadRequest(new Exception("Username and password are required"));
            }
            
            var Un = a.Uname;
            var Pa = a.Password;
            
            var result = await _context.ArtUsers.SingleOrDefaultAsync((e) => e.Uname == Un && e.Password == Pa);
            
            if (result == null)
                return BadRequest(new Exception("Invalid Username/Password"));
            return Ok(result);
        }


        // GET: api/ArtUser/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ArtUser>> GetArtUser(int id)
        {
            var artUser = await _context.ArtUsers.FindAsync(id);

            if (artUser == null)
            {
                return NotFound();
            }

            return artUser;
        }

        
        // POST: api/ArtUser
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<ArtUser>> PostArtUser(ArtUser artUser)
        {
            try{
           
                foreach(var item in _context.ArtUsers)
                {
                    if(item.Uname==artUser.Uname)
                    {
                        throw new RegisterException("Username already exists");
                        
                    }
                }
            
                _context.ArtUsers.Add(artUser);
                await _context.SaveChangesAsync();
            }
            catch(Exception e)
            {
                return Conflict(e.Message);
            }
           

            return CreatedAtAction("GetArtUser", new { id = artUser.Uid }, artUser);
        }


    }
}

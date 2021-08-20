using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quipu.Core.BLL;
using Quipu.Core.DAL;
using Quipu.Core.DomainModel;

namespace Quipu.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPermissionOverridesController : ControllerBase
    {
        private readonly QContext _context;
        private readonly IModelService<UserPermissionOverride> modelService;

        public UserPermissionOverridesController(QContext context,
                                     IModelService<UserPermissionOverride> service)
        {
            _context = context;
            modelService = service;
        }

        // GET: api/UserPermissionOverrides
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserPermissionOverride>>> GetUserPermissionOverrides()
        {
            return await _context.UserPermissionOverrides.ToListAsync();
        }

        // GET: api/UserPermissionOverrides/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserPermissionOverride>> GetUserPermissionOverride(int id)
        {
            var userPermissionOverride = await _context.UserPermissionOverrides.FindAsync(id);

            if (userPermissionOverride == null)
            {
                return NotFound();
            }

            return userPermissionOverride;
        }

        // PUT: api/UserPermissionOverrides/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserPermissionOverride(int id, UserPermissionOverride userPermissionOverride)
        {
            if (id != userPermissionOverride.ID)
            {
                return BadRequest();
            }

            _context.Entry(userPermissionOverride).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserPermissionOverrideExists(id))
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

        // POST: api/UserPermissionOverrides
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserPermissionOverride>> PostUserPermissionOverride(UserPermissionOverride userPermissionOverride)
        {
            _context.UserPermissionOverrides.Add(userPermissionOverride);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserPermissionOverride", new { id = userPermissionOverride.ID }, userPermissionOverride);
        }

        // DELETE: api/UserPermissionOverrides/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserPermissionOverride(int id)
        {
            var userPermissionOverride = await _context.UserPermissionOverrides.FindAsync(id);
            if (userPermissionOverride == null)
            {
                return NotFound();
            }

            _context.UserPermissionOverrides.Remove(userPermissionOverride);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserPermissionOverrideExists(int id)
        {
            return _context.UserPermissionOverrides.Any(e => e.ID == id);
        }
    }
}

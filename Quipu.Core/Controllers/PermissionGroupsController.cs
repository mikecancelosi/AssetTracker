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
    public class PermissionGroupsController : ControllerBase
    {
        private readonly QContext _context;
        private readonly IModelService<PermissionGroup> modelService;

        public PermissionGroupsController(QContext context,
                                     IModelService<PermissionGroup> service)
        {
            _context = context;
            modelService = service;
        }

        // GET: api/PermissionGroups
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PermissionGroup>>> GetPermissionGroups()
        {
            return await _context.PermissionGroups.ToListAsync();
        }

        // GET: api/PermissionGroups/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PermissionGroup>> GetPermissionGroup(int id)
        {
            var permissionGroup = await _context.PermissionGroups.FindAsync(id);

            if (permissionGroup == null)
            {
                return NotFound();
            }

            return permissionGroup;
        }

        // PUT: api/PermissionGroups/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPermissionGroup(int id, PermissionGroup permissionGroup)
        {
            if (id != permissionGroup.ID)
            {
                return BadRequest();
            }

            _context.Entry(permissionGroup).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PermissionGroupExists(id))
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

        // POST: api/PermissionGroups
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PermissionGroup>> PostPermissionGroup(PermissionGroup permissionGroup)
        {
            _context.PermissionGroups.Add(permissionGroup);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPermissionGroup", new { id = permissionGroup.ID }, permissionGroup);
        }

        // DELETE: api/PermissionGroups/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePermissionGroup(int id)
        {
            var permissionGroup = await _context.PermissionGroups.FindAsync(id);
            if (permissionGroup == null)
            {
                return NotFound();
            }

            _context.PermissionGroups.Remove(permissionGroup);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PermissionGroupExists(int id)
        {
            return _context.PermissionGroups.Any(e => e.ID == id);
        }
    }
}

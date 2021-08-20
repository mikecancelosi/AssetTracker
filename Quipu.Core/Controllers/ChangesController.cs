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
    public class ChangesController : ControllerBase
    {
        private readonly QContext _context;
        private readonly IModelService<Change> modelService;

        public ChangesController(QContext context,
                                 IModelService<Change> service)
        {
            _context = context;
            modelService = service;
        }

        // GET: api/Changes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Change>>> GetChanges()
        {
            return await _context.Changes.ToListAsync();
        }

        // GET: api/Changes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Change>> GetChange(int id)
        {
            var change = await _context.Changes.FindAsync(id);

            if (change == null)
            {
                return NotFound();
            }

            return change;
        }

        // PUT: api/Changes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChange(int id, Change change)
        {
            if (id != change.ID)
            {
                return BadRequest();
            }

            _context.Entry(change).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChangeExists(id))
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

        // POST: api/Changes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Change>> PostChange(Change change)
        {
            _context.Changes.Add(change);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetChange", new { id = change.ID }, change);
        }

        // DELETE: api/Changes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChange(int id)
        {
            var change = await _context.Changes.FindAsync(id);
            if (change == null)
            {
                return NotFound();
            }

            _context.Changes.Remove(change);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChangeExists(int id)
        {
            return _context.Changes.Any(e => e.ID == id);
        }
    }
}

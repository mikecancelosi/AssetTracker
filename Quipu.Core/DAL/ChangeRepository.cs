using Quipu.Core.DomainModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Quipu.Core.DAL
{
    public class ChangeRepository : IRepository<Change>
    {
        private readonly QContext _context;

        public async Task<Change> Create(Change instance)
        {
            _context.Changes.Add(instance);
            await _context.SaveChangesAsync();
            return instance;
        }

        public async System.Threading.Tasks.Task Delete(int id)
        {
            var ChangesToDelete = await _context.Changes.FindAsync(id);
            _context.Changes.Remove(ChangesToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Change>> Get()
        {
            return await _context.Changes.ToListAsync();
        }

        public async System.Threading.Tasks.Task Update(Change instance)
        {
            _context.Entry(instance).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}

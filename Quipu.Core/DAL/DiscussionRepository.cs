using Quipu.Core.DomainModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Quipu.Core.DAL
{
    public class DiscussionRepository : IRepository<Discussion>
    {
        private readonly QContext _context;

        public async Task<Discussion> Create(Discussion instance)
        {
            _context.Discussions.Add(instance);
            await _context.SaveChangesAsync();
            return instance;
        }

        public async System.Threading.Tasks.Task Delete(int id)
        {
            var InstanceToDelete = await _context.Discussions.FindAsync(id);
            _context.Discussions.Remove(InstanceToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Discussion>> Get()
        {
            return await _context.Discussions.ToListAsync();
        }

        public async System.Threading.Tasks.Task Update(Discussion instance)
        {
            _context.Entry(instance).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}

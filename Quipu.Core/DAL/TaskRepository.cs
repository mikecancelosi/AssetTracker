using Quipu.Core.DomainModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Quipu.Core.DAL
{
    public class TaskRepository : IRepository<DomainModel.Task>
    {
        private readonly QContext _context;

        public async Task<DomainModel.Task> Create(DomainModel.Task instance)
        {
            _context.Tasks.Add(instance);
            await _context.SaveChangesAsync();
            return instance;
        }

        public async System.Threading.Tasks.Task Delete(int id)
        {
            var InstanceToDelete = await _context.Tasks.FindAsync(id);
            _context.Tasks.Remove(InstanceToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<DomainModel.Task>> Get()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async System.Threading.Tasks.Task Update(DomainModel.Task instance)
        {
            _context.Entry(instance).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}

using Quipu.Core.DomainModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Quipu.Core.DAL
{
    public class PermissionRepository : IRepository<Permission>
    {
        private readonly QContext _context;

        public async Task<Permission> Create(Permission instance)
        {
            _context.Permissions.Add(instance);
            await _context.SaveChangesAsync();
            return instance;
        }

        public async System.Threading.Tasks.Task Delete(int id)
        {
            var InstanceToDelete = await _context.Permissions.FindAsync(id);
            _context.Permissions.Remove(InstanceToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Permission>> Get()
        {
            return await _context.Permissions.ToListAsync();
        }

        public async System.Threading.Tasks.Task Update(Permission instance)
        {
            _context.Entry(instance).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}

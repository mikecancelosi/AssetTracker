using Quipu.Core.DomainModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Quipu.Core.DAL
{
    public class PermissionGroupRepository : IRepository<PermissionGroup>
    {
        private readonly QContext _context;

        public async Task<PermissionGroup> Create(PermissionGroup instance)
        {
            _context.PermissionGroups.Add(instance);
            await _context.SaveChangesAsync();
            return instance;
        }

        public async System.Threading.Tasks.Task Delete(int id)
        {
            var InstanceToDelete = await _context.PermissionGroups.FindAsync(id);
            _context.PermissionGroups.Remove(InstanceToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PermissionGroup>> Get()
        {
            return await _context.PermissionGroups.ToListAsync();
        }

        public async System.Threading.Tasks.Task Update(PermissionGroup instance)
        {
            _context.Entry(instance).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}

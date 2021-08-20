using Quipu.Core.DomainModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Quipu.Core.DAL
{
    public class UserPermissionOverrideRepository : IRepository<UserPermissionOverride>
    {
        private readonly QContext _context;

        public async Task<UserPermissionOverride> Create(UserPermissionOverride instance)
        {
            _context.UserPermissionOverrides.Add(instance);
            await _context.SaveChangesAsync();
            return instance;
        }

        public async System.Threading.Tasks.Task Delete(int id)
        {
            var InstanceToDelete = await _context.UserPermissionOverrides.FindAsync(id);
            _context.UserPermissionOverrides.Remove(InstanceToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserPermissionOverride>> Get()
        {
            return await _context.UserPermissionOverrides.ToListAsync();
        }

        public async System.Threading.Tasks.Task Update(UserPermissionOverride instance)
        {
            _context.Entry(instance).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}


using Quipu.Core.DomainModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Quipu.Core.DAL
{
    public class SettingsRepository : IRepository<Settings>
    {
        private readonly QContext _context;

        public async Task<Settings> Create(Settings instance)
        {
            _context.Settings.Add(instance);
            await _context.SaveChangesAsync();
            return instance;
        }

        public async System.Threading.Tasks.Task Delete(int id)
        {
            var InstanceToDelete = await _context.Settings.FindAsync(id);
            _context.Settings.Remove(InstanceToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Settings>> Get()
        {
            return await _context.Settings.ToListAsync();
        }

        public async System.Threading.Tasks.Task Update(Settings instance)
        {
            _context.Entry(instance).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}

using Quipu.Core.DomainModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Quipu.Core.DAL
{
    public class AlertRepository : IRepository<Alert>
    {
        private readonly QContext _context;

        public async Task<Alert> Create(Alert instance)
        {
            _context.Alerts.Add(instance);
            await _context.SaveChangesAsync();
            return instance;
        }

        public async System.Threading.Tasks.Task Delete(int id)
        {
            var AlertToDelete = await _context.Alerts.FindAsync(id);
            _context.Alerts.Remove(AlertToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Alert>> Get()
        {
            return await _context.Alerts.ToListAsync();
        }

        public async System.Threading.Tasks.Task Update(Alert instance)
        {
            _context.Entry(instance).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}

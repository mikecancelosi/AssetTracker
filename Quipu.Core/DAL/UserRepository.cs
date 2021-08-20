using Quipu.Core.DomainModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Quipu.Core.DAL
{
    public class UserRepository : IRepository<User>
    {
        private readonly QContext _context;

        public async Task<User> Create(User instance)
        {
            _context.Users.Add(instance);
            await _context.SaveChangesAsync();
            return instance;
        }

        public async System.Threading.Tasks.Task Delete(int id)
        {
            var UserToDelete = await _context.Users.FindAsync(id);
            _context.Users.Remove(UserToDelete);
            await _context.SaveChangesAsync();
        }

        public async  Task<IEnumerable<User>> Get()
        {
            return await _context.Users.ToListAsync();
        }

        public async System.Threading.Tasks.Task Update(User instance)
        {
            _context.Entry(instance).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}

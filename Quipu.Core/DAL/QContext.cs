using Microsoft.EntityFrameworkCore;
using Quipu.Core.DomainModel;

namespace Quipu.Core.DAL
{
    public class QContext : DbContext
    {
        public QContext(DbContextOptions<QContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Alert> Alerts { get; set; }
        public DbSet<Change> Changes { get; set; }
        public DbSet<Discussion> Discussions { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<PermissionGroup> PermissionGroups { get; set; }
        public DbSet<Settings> Settings { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserPermissionOverride> UserPermissionOverrides { get; set; }
    }
}

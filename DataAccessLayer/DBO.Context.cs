//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccessLayer
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Common;
    using DomainModel;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class TrackerContext : DbContext
    {
        public TrackerContext()
            : base("name=TrackerContext")
        {
        }
    
         public TrackerContext(string connectionString)
            : base(connectionString)
        {
        }
    
          public TrackerContext(DbConnection connection)
            : base(connection,false)
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Asset> Assets { get; set; }
        public virtual DbSet<AssetCategory> AssetCategories { get; set; }
        public virtual DbSet<AssetLink> AssetLinks { get; set; }
        public virtual DbSet<AssetLinkType> AssetLinkTypes { get; set; }
        public virtual DbSet<Phase> Phases { get; set; }
        public virtual DbSet<SecPermission> SecPermissions { get; set; }
        public virtual DbSet<SecPermission2> SecPermission2 { get; set; }
        public virtual DbSet<SecPermission3> SecPermission3 { get; set; }
        public virtual DbSet<SecRole> SecRoles { get; set; }
        public virtual DbSet<Setting> Settings { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Change> Changes { get; set; }
        public virtual DbSet<Discussion> Discussions { get; set; }
        public virtual DbSet<Metadata> Metadata { get; set; }
        public virtual DbSet<Alert> Alerts { get; set; }
        public virtual DbSet<SecPermission4> SecPermission4 { get; set; }
    
        public virtual ObjectResult<assetGetInformation_Result> assetGetInformation()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<assetGetInformation_Result>("assetGetInformation");
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AssetTracker.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Asset
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Asset()
        {
            this.Discussions = new HashSet<Discussion>();
            this.Children = new HashSet<Asset>();
            this.Alerts = new HashSet<Alert>();
        }
    
        public int as_id { get; set; }
        public string as_displayname { get; set; }
        public string as_description { get; set; }
        public Nullable<int> as_parentid { get; set; }
        public int as_caid { get; set; }
        public Nullable<int> as_usid { get; set; }
        public Nullable<int> as_phid { get; set; }
    
        public virtual AssetCategory AssetCategory { get; set; }
        public virtual Phase Phase { get; set; }
        public virtual User AssignedToUser { get; set; }
        public virtual AssetLink AssetLink { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Discussion> Discussions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Asset> Children { get; set; }
        public virtual Asset Parent { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Alert> Alerts { get; set; }
    }
}

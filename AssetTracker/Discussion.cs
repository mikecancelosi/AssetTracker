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
    
    public partial class Discussion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Discussion()
        {
            this.Children = new HashSet<Discussion>();
        }
    
        public int di_id { get; set; }
        public string di_contents { get; set; }
        public int di_asid { get; set; }
        public Nullable<int> di_parentid { get; set; }
        public Nullable<int> di_usid { get; set; }
        public System.DateTime di_date { get; set; }
    
        public virtual Asset Asset { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Discussion> Children { get; set; }
        public virtual Discussion Parent { get; set; }
        public virtual User User { get; set; }
    }
}

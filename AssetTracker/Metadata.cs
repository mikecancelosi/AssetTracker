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
    
    public partial class Metadata
    {
        public int md_id { get; set; }
        public string md_value { get; set; }
        public int md_asid { get; set; }
    
        public virtual Asset Asset { get; set; }
    }
}

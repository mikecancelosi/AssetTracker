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
    
    public partial class SecPermission3
    {
        public int p3_id { get; set; }
        public int p3_roid { get; set; }
        public int p3_prid { get; set; }
        public bool p3_allow { get; set; }
    
        public virtual SecPermission SecPermission { get; set; }
        public virtual SecRole SecRole { get; set; }
    }
}

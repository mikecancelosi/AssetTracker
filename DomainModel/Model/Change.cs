using System.Collections.Generic;
using System.Linq;

namespace DomainModel
{
    /// <summary>
    /// A change is logged after asset saves in order for users to
    /// be able to view a changelog ( left hand side of asset detail )
    /// </summary>
    public partial class Change : DatabaseBackedObject
    {
        public override int ID
        {
            get => ch_id;            
            set => ch_id = value;
        }

        public override string Name         
        {
            get => ch_field;
            set => ch_field = value;
        }
    }
}

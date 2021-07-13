using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTracker.Model
{
   public partial class AssetLink : DatabaseBackedObject
    {
        public override int ID
        {
            get
            {
                return li_id;
            }
            set
            {
                li_id = value;
            }
        }
        public override string Name
        {
            get
            {
                return li_location;
            }
            set
            {
                li_location = value;
            }
        }
    }
}

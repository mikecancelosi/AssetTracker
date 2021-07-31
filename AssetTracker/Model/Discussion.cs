using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTracker.Model
{
    public partial class Discussion : DatabaseBackedObject
    {
        public override int ID
        {
            get => di_id;
            set => di_id = value;
        }

        public override string Name
        {
            get => di_contents;
            set => di_contents = value;
        }
    }
}

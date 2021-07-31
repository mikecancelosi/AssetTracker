using AssetTracker.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTracker.Model
{
    public partial class Alert : DatabaseBackedObject
    {
        public override int ID
        {
            get => ar_id;
            set => ar_id = value;
        }

        public override string Name
        {
            get => ar_header;
            set => ar_header = value;
        }

    }
}

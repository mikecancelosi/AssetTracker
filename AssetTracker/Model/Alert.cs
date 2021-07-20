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
        public override int ID => ar_id;
        public override string Name { get => base.Name; set => base.Name = value; }      

    }
}

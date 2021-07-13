using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTracker.Model
{
    public partial class SecPermission3 : DatabaseBackedObject
    {
        public override int ID => p3_id;
        public override string Name => SecPermission.pr_name + "Override";

    }
}

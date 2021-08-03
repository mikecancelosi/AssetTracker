using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTracker.Model
{
    /// <summary>
    /// User Permission Overrides 
    /// </summary>
    public partial class SecPermission3 : DatabaseBackedObject
    {
        public override int ID
        {
            get => p3_id;
            set => p3_id = value;
        }

        public override string Name
        {
            get => SecPermission.pr_name + "Override";
            set => throw new NotImplementedException();
        }

    }
}

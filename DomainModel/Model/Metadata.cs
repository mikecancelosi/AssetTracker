using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public partial class Metadata : DatabaseBackedObject
    {
        public override int ID
        {
            get => md_id;
            set => md_id = value;
        }

        public override string Name 
        {
            get => md_value;
            set => md_value = value;
        }
    }
}

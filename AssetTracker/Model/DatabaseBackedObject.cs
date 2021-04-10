using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTracker.Model
{
    public abstract class DatabaseBackedObject
    {
        public virtual int ID
        {
            get
            {
                return 0;
            }
        }
        public virtual string Name
        {
            get
            {
                return "";
            }
        }

        public virtual bool IsValid(out List<Violation> violations)
        {          
            violations = new List<Violation>();
            
            /*if(ID <= 0)
            {
                violations.Add(new Violation("ID must be greater than 0", "IDNotSet"));
            }*/

            return violations.Count() == 0;
        }

        public DatabaseBackedObject()
        {

        }
    }
}

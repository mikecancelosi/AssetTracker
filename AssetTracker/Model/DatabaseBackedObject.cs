using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            set
            {

            }
        }
        public virtual string Name
        {
            get
            {
                return "";
            }
            set
            {

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

        public static void CopyProperties(DatabaseBackedObject copyFrom, DatabaseBackedObject copyTo)
        {
            Type dboType = copyFrom.GetType().BaseType;
            if(copyTo.GetType().BaseType != dboType &&
               copyTo.GetType() != dboType)
            {
                Console.Error.WriteLine("Dbo types do not match!");
                return;
            }

            PropertyInfo[] properties = dboType.GetProperties();

            foreach(PropertyInfo prop in properties)
            {
                object fromVal = prop.GetValue(copyFrom);
                prop.SetValue(copyTo, fromVal);
            }


        }
    }
}

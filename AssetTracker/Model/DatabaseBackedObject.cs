using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AssetTracker.Model
{
    public abstract class DatabaseBackedObject 
    {
        public DatabaseBackedObject()
        {

        }

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

            return violations.Count() == 0;
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
                if (prop.SetMethod != null)
                {
                    object fromVal = prop.GetValue(copyFrom);
                    prop.SetValue(copyTo, fromVal);
                }
            }
        }

        public virtual bool Save(TrackerContext context, out List<Violation> violations)
        {
            try
            {
                if (IsValid(out violations))
                {
                    if (context.Entry(this).State == System.Data.Entity.EntityState.Detached)
                    {
                        context.Set(GetType()).Attach(this);
                    }
                    context.SaveChanges();

                    return true;
                }
            }
            catch (DbEntityValidationException e)
            {
                throw;
            }

            //Handle Violations
            return false;
        }

        
        public virtual List<Change> GetChanges(DatabaseBackedObject beforeObject)
        {
            // TODO: Load user id from current session
            // TODO: Add changes automatically, using long names from db + "Changed" e.g 'as_usid Changed'

            List<Change> output = new List<Change>();
            int UserId = 0;           


            if(ID != beforeObject.ID)
            {
                output.Add(new Change()
                {
                    ch_datetime = DateTime.Now,
                    ch_field = "ID",
                    ch_oldvalue = beforeObject.ID.ToString(),
                    ch_newvalue = ID.ToString(),
                    ch_description = "ID Changed",
                    ch_recid = ID,
                    ch_usid = UserId
                });
            }
            if(Name != beforeObject.Name)
            {
                output.Add(new Change()
                {
                    ch_datetime = DateTime.Now,
                    ch_field = "Name",
                    ch_oldvalue = beforeObject.Name,
                    ch_newvalue = Name,
                    ch_description = "Name Changed",
                    ch_recid = ID,
                    ch_usid = UserId
                });
            }

            return output;
        }

    }
}

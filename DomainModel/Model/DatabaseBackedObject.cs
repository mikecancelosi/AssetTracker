using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Reflection;

namespace DomainModel
{
    /// <summary>
    /// A DatabaseBackedObject (DBO) serves to extend our model classes and
    /// implement some common features that are necessary for dealing with these models.
    /// </summary>
    public abstract class DatabaseBackedObject
    {      
        /// <summary>
        /// ID is the identity column
        /// </summary>
        public abstract int ID { get; set; }
        /// <summary>
        /// Name can be any of the columns and serves to give a common way to display the information 
        /// </summary>
        public abstract string Name { get; set; }
        /// <summary>
        /// Before saving or displaying a model, we may want to check the validity of the object.
        /// </summary>
        /// <param name="violations">Violations in the formation of the object</param>
        /// <returns>Whether or not the object is valid </returns>
        public virtual bool IsValid(out List<Violation> violations)
        {
            violations = new List<Violation>();

            return violations.Count() == 0;
        }

        /// <summary>
        /// Copy properties of on DBO to another DBO of the same type
        /// </summary>
        /// <param name="copyFrom">Source DBO</param>
        /// <param name="copyTo">Destination DBO</param>
        public static void CopyProperties(DatabaseBackedObject copyFrom, DatabaseBackedObject copyTo)
        {
            Type dboType = copyFrom.GetType().BaseType;

            if (copyTo.GetType().BaseType != dboType &&
               copyTo.GetType() != dboType)
            {
                Console.Error.WriteLine("Dbo types do not match!");
                return;
            }

            PropertyInfo[] properties = dboType.GetProperties();

            foreach (PropertyInfo prop in properties)
            {
                if (prop.SetMethod != null)
                {
                    object fromVal = prop.GetValue(copyFrom);
                    prop.SetValue(copyTo, fromVal);
                }
            }
        }      

        //TODO: Upgrade to .NET 5 for covariant return type support -- ASP
        /// <summary>
        /// Clone object and return it
        /// </summary>
        /// <returns>Clone object that is NOT attached to dbcontext</returns>
        public virtual DatabaseBackedObject Clone()
        {
            object instance = Activator.CreateInstance(this.GetType());
            ((DatabaseBackedObject)instance).Name = Name + "Clone";
            return (DatabaseBackedObject)instance;
        }

    }
}

using AssetTracker.ViewModels;
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

        /// <summary>
        /// Save DBO to database.
        /// </summary>
        /// <param name="context">Context to use in order to conect tothe database</param>
        /// <param name="violations">Violations found when trying to save</param>
        /// <returns>If the save performed properly</returns>
        public virtual bool Save(TrackerContext context, out List<Violation> violations)
        {
            try
            {
                if (IsValid(out violations))
                {
                    if (ID == 0)
                    {
                        context.Set(GetType()).Add(this);
                    }
                    else
                    {
                        var entity = context.Set(GetType()).Find(ID);
                        context.Entry(entity).CurrentValues.SetValues(this);
                    }

                    context.SaveChanges();

                    return true;
                }
            }
            catch (DbEntityValidationException e)
            {
                //TODO: Add DB validation exceptions to violations
                throw new NotImplementedException();
            }

            return false;
        }


        /// <summary>
        /// Delete object from database
        /// </summary>
        /// <param name="context">Context to use in order to connect to the database</param>
        public virtual void Delete(TrackerContext context)
        {
            try
            {
                context.Set(GetType()).Remove(this);
                context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                throw new NotImplementedException();
            }
        }

        //TODO: Upgrade to .NET 5 for covariant return type support
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

        /// <summary>
        /// Get changes of database values and the current state
        /// </summary>
        /// <param name="beforeObject">Database value of the object</param>
        /// <returns>List of  changes marking the notable changes</returns>
        public virtual List<Change> GetChanges(DatabaseBackedObject beforeObject)
        {          
            // TODO: Add changes automatically, using long names from db + "Changed" e.g 'as_usid Changed'

            List<Change> output = new List<Change>();
            int UserId = MainViewModel.Instance.CurrentUser.ID;

            if (ID != beforeObject.ID)
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
            if (Name != beforeObject.Name)
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

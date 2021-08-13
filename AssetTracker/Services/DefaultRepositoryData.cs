using DomainModel;
using System;
using System.Collections.Generic;

namespace AssetTracker.Services
{
    /// <summary>
    /// This class serves to set up test data for an empty database.
    /// </summary>
    public class DefaultRepositoryData
    {
        public static List<DatabaseBackedObject> BuildTestData()
        {
            List<DatabaseBackedObject> output = new List<DatabaseBackedObject>();

            #region SecRoles
            output.Add(new SecRole()
            {
                 ro_id = 1,
                 ro_name = "Supervisor",
            });

            output.Add(new SecRole()
            {
                ro_id = 2,
                ro_name = "Level 1",
            });
            #endregion

            #region Users
            output.Add(new User()
            {
                us_fname = "Robert",
                us_lname = "Thornington",
                us_displayname = "Billy Bob",
                us_email = "Billy@bobthorton.com",
                us_roid = 1,
                us_id = 1
               
            });

            output.Add(new User()
            {
                us_fname = "Emily",
                us_lname = "Thornington",
                us_displayname = "Emington the 3rd",
                us_email = "Emily@gmail.com",
                us_roid = 1,
                us_id = 2
            });

            output.Add(new User()
            {
                us_fname = "Joseph",
                us_lname = "Banks",
                us_displayname = "jos A Bank",
                us_email = "Joseph@AWhos.com",
                us_roid = 2,
                us_id = 3

            });

            #endregion

            #region Phases
            output.Add(new Phase()
            {
                ph_id = 1,
                ph_name = "Concept",
            });

            output.Add(new Phase()
            {
                ph_id = 2,
                ph_name = "Blocking",
            });

            output.Add(new Phase()
            {
                ph_id = 3,
                ph_name = "First Draft",
            });

            output.Add(new Phase()
            {
                ph_id = 4,
                ph_name = "Polish",
            });
            #endregion



            #region Categories
            output.Add(new AssetCategory()
            {
                ca_name = "Modeling",
                ca_id = 1,
            });

            output.Add(new AssetCategory()
            {
                ca_name = "VFX",
                ca_id = 2,
            });


            output.Add(new AssetCategory()
            {
                ca_name = "UI",
                ca_id = 3,
            });

            output.Add(new AssetCategory()
            {
                ca_name = "Sound Design",
                ca_id = 4,
            });

            #endregion


            #region Assets
            output.Add(new Asset()
            {
                as_displayname = "World 1",
                as_description = "Intro world environment",
                as_usid = 1,
                as_phid = 1,
                as_caid = 1,
                as_id = 1,
            });

            output.Add(new Asset()
            {
                as_displayname = "World 2",
                as_description = "Water world environment",
                as_usid = 1,
                as_phid = 1,
                as_caid = 1,
                as_id = 2,
            });

            output.Add(new Asset()
            {
                as_displayname = "Main womenu",
                as_description = "Main menu UI",
                as_usid = 3,
                as_phid = 1,
                as_caid = 3,
                as_id = 3,
            });

            output.Add(new Asset()
            {
                as_displayname = "Main menu buttons",
                as_description = "Main menu button design",
                as_usid = 3,
                as_phid = 1,
                as_caid = 3,
                as_id = 4,
                as_parentid = 3,
            });

            output.Add(new Asset()
            {
                as_displayname = "Menu music",
                as_description = "Music for main menu",
                as_usid = 2,
                as_phid = 1,
                as_caid = 4,
                as_id = 5,
                as_parentid = 2
            });

            #endregion

            return output;
        }
    }
}

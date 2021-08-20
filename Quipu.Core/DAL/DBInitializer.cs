using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quipu.Core.DomainModel;

namespace Quipu.Core.DAL
{
    public static class DBInitializer
    {
        /// <summary>
        /// Seed the database with initial values
        /// </summary>
        /// <param name="context">Database context to use</param>
        public static void Seed(QContext context)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any())
            {
                return; // DB has been seeded
            }


            var users = new User[]
            {
                new User
                {
                First_Name = "Robert",
                Last_Name = "Thornington",
                Display_Name = "Billy Bob",
                Login = "admin",
                Password = "12345"
                },
                new User
                {
                First_Name = "Emily",
                Last_Name = "Thornington",
                Display_Name = "Emington the 3rd",
                Login = "Emily@gmail.com"
                },
                new User
                {
                First_Name = "Joseph",
                Last_Name = "Banks",
                Display_Name = "jos A Bank",
                Login = "Joseph@AWhos.com"
                },
                new User
                {
                First_Name = "Mike",
                Last_Name = "Cancelosi",
                Display_Name = "Mike Cancelosi",
                Login = "mcancelosi",
                Password = "12345"
                }
            };

            foreach (User u in users)
            {
                context.Users.Add(u);
            }

            var tasks = new DomainModel.Task[]
            {
                new DomainModel.Task
                {
                    Name = "Create World 1",
                    Description = "We need to generate the first world in such a way that " +
                                  "promotes learning. A tutorial level without a straight up" +
                                  "tutorial."
                },
                new DomainModel.Task
                {
                    Name = "Main menu",
                    Description = "We need the main menu to use for navigating to each feature."
                },
                new DomainModel.Task
                {
                    Name = "Idle animation for Jerry",
                    Description = "Remember, Jerry is large and out of shap, his idle should be" +
                                  " something that reflects this. Labored breathing, eating " +
                                  "something?"
                }
            };

            foreach(DomainModel.Task t in tasks)
            {
                context.Tasks.Add(t);
            }

            var permissionGroups = new PermissionGroup[]
            {
                new PermissionGroup{Name = "General"},
                new PermissionGroup{Name = "Users"},
                new PermissionGroup{Name = "Project"},
                new PermissionGroup{Name = "Planning"}
            };

            foreach(PermissionGroup group in permissionGroups)
            {
                context.PermissionGroups.Add(group);
            }

            context.SaveChanges();

            var permissions = new Permission[]
            {
                new Permission{Name="Create Task",Default= true, Header_ID = permissionGroups.Single(x=>x.Name == "Planning").ID},
                new Permission{Name="View Task",Default= true, Header_ID = permissionGroups.Single(x=>x.Name == "Planning").ID},
                new Permission{Name="Update Task -- Unassigned", Default= true, Header_ID = permissionGroups.Single(x=>x.Name == "Planning").ID},
                new Permission{Name="Delete Task",Default= true, Header_ID = permissionGroups.Single(x=>x.Name == "Planning").ID},
                new Permission{Name="Delete Task -- Unassigned", Default= true, Header_ID = permissionGroups.Single(x=>x.Name == "Planning").ID},

                new Permission{Name="Create User",Default= true, Header_ID = permissionGroups.Single(x=>x.Name == "Users").ID},
                new Permission{Name="View User Info",Default= true, Header_ID = permissionGroups.Single(x=>x.Name == "Users").ID},
                new Permission{Name="Update User", Default= true, Header_ID = permissionGroups.Single(x=>x.Name == "Users").ID},
                new Permission{Name="Delete User",Default= true, Header_ID = permissionGroups.Single(x=>x.Name == "Users").ID},

                new Permission{Name="Create Alert",Default= false, Header_ID = permissionGroups.Single(x=>x.Name == "Project").ID},
                new Permission{Name="Update Alert",Default= false, Header_ID = permissionGroups.Single(x=>x.Name == "Project").ID},
                new Permission{Name="Delete Alert", Default= false, Header_ID = permissionGroups.Single(x=>x.Name == "Project").ID}
            };

            foreach(Permission permission in permissions)
            {
                context.Permissions.Add(permission);
            }

            context.SaveChanges();

        }
    }
}

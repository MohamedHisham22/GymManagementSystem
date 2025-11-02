using GymManagementSystemDAL.Data.DbContexts;
using GymManagementSystemDAL.Models;
using GymManagementSystemDAL.Models.AuthModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemDAL.Data.DataSeed
{
    public class GymAuthDbContextDataSeeder
    {
        public static bool SeedData(RoleManager<IdentityRole> roleManager , UserManager<ApplicationUser> userManager )
        {
            try
            {
                var hasRoles = roleManager.Roles.Any();
                var hasUsers = userManager.Users.Any();

                if (hasRoles && hasUsers) return false;

                #region Roles Seeding
                if (!hasRoles)
                {
                    var roles = new List<IdentityRole>()
                    {
                        new IdentityRole() { Name = "SuperAdmin" },
                        new IdentityRole() { Name = "Admin" },
                    };
                    
                    foreach (var role in roles)
                    {
                        if (!roleManager.RoleExistsAsync(role.Name!).Result) 
                        {
                            roleManager.CreateAsync(role).Wait();
                        }
                        
                    }

                }
                #endregion

                #region Users Seeding
                if (!hasUsers)
                {
                    var users = new List<ApplicationUser>()
                    {
                        new ApplicationUser()
                        {
                        FirstName = "Mohamed",
                        LastName = "Hisham",
                        UserName = "MohamedHisham",
                        PhoneNumber = "01023793349",
                        Email = "mohamed.hisham301@gmail.com"

                        },
                        new ApplicationUser()
                        {
                        FirstName = "Mohamed",
                        LastName = "Hisham",
                        UserName = "MohamedHisham22",
                        PhoneNumber = "01023793348",
                        Email = "mhisham0101@gmail.com"

                        }
                    };

                    foreach (var user in users) 
                    {
                        userManager.CreateAsync(user, "P@ssw0rd").Wait();
                    }
                    userManager.AddToRoleAsync(users[0], "SuperAdmin").Wait();
                    userManager.AddToRoleAsync(users[1], "Admin").Wait();

                }
                #endregion
                
                return true;


            }
            catch
            {
                return false;
            }

        }
    }
}

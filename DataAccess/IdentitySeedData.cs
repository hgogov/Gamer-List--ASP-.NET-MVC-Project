using DataAccess;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace DataAccess
{
    public class IdentitySeedData //: DropCreateDatabaseIfModelChanges<DbEntitiesContext>
    {
        protected static void Seed(DbEntitiesContext context)
        {
            PerformInitialSetup(context);
            //base.Seed(context);
        }
        public static void PerformInitialSetup(DbEntitiesContext context)
        {
            using (var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context)))
            {
                using (var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context)))
                {
                    //system Role
                    if (!roleManager.RoleExists("admin"))
                    {
                        roleManager.Create(new IdentityRole("admin"));
                    }
                    if (!roleManager.RoleExists("normal user"))
                    {
                        roleManager.Create(new IdentityRole("normal user"));
                    }
                }
                var user = new ApplicationUser() { UserName = "admin", Email = "admin@admin.com", PhoneNumber = "00000000000" };

                //IdentityResult userResult = userManager.Create(user, "123456");

                //if (userResult != IdentityResult.Success)
                //    throw new Exception("failed");

                if (userManager.FindByEmail(user.Email)?.Email != "admin@admin.com")
                {
                    if (userManager.Create(user, "1234Admin!") != IdentityResult.Success)
                    {
                        throw new Exception("failed");
                    }
                    // role
                    userManager.AddToRole(user.Id, "admin");
                }

                context.SaveChanges();
            }
        }

        //protected override void Seed(DbEntitiesContext context)
        //{
        //    var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
        //    var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

        //    string name = "admin";
        //    string email = "admin@admin.com";
        //    string password = "admin";
        //    string role = "admin";

        //    //Create Role Test and User Test
        //    RoleManager.Create(new IdentityRole(role));
        //    UserManager.Create(new ApplicationUser() { UserName = name, Email = email });

        //    //Create Role Admin if it does not exist
        //    if (!RoleManager.RoleExists(name))
        //    {
        //        var roleresult = RoleManager.Create(new IdentityRole(name));
        //    }

        //    //Create User=Admin with password=123456
        //    var user = new ApplicationUser();
        //    user.UserName = name;
        //    user.Email = email;
        //    var adminresult = UserManager.Create(user, password);

        //    //Add User Admin to Role Admin
        //    if (adminresult.Succeeded)
        //    {
        //        var result = UserManager.AddToRole(user.Id, name);
        //    }
        //    base.Seed(context);
        //}


        //private static DbEntitiesContext context;

        //public IdentitySeedData()
        //    :this(new DbEntitiesContext())
        //{
        //}

        //public IdentitySeedData(DbEntitiesContext _context)
        //{
        //    context = _context;
        //}

        //public void Initialize()
        //{
        //    string[] roles = new string[] {"Admin", "Normal User"};

        //    foreach (string role in roles)
        //    {
        //        var roleStore = new RoleStore<IdentityRole>(context);

        //        if (!context.Roles.Any(r => r.Name == role))
        //        {
        //            roleStore.CreateAsync(new IdentityRole(role));
        //        }
        //    }


        //    var user = new ApplicationUser
        //    {
        //        Email = "admin@admin.com",
        //        UserName = "admin",
        //        PhoneNumber = "+111111111111",
        //        EmailConfirmed = true,
        //        PhoneNumberConfirmed = true,
        //        SecurityStamp = Guid.NewGuid().ToString("D")
        //    };


        //    if (!context.Users.Any(u => u.UserName == user.UserName))
        //    {
        //        var password = new PasswordHasher();
        //        var hashed = password.HashPassword("admin");
        //        user.PasswordHash = hashed;

        //        var userStore = new UserStore<ApplicationUser>(context);
        //        var result = userStore.CreateAsync(user);

        //    }


        //    AssignRoles(user.Email, roles);

        //    context.SaveChangesAsync();
        //}

        //public async Task<IdentityResult> AssignRoles(string email, string[] roles)
        //{
        //    var roleStore = new UserStore<ApplicationUser>();

        //    UserManager<ApplicationUser> _userManager = new UserManager<ApplicationUser>(roleStore);
        //    ApplicationUser user = await _userManager.FindByEmailAsync(email);
        //    var result = await _userManager.AddToRolesAsync(user.ToString(), roles);

        //    return result;
        //}
    }
}
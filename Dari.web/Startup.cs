using Dari.web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System.Web.Services.Description;
using static Dari.web.Models.ApplicationUser;

[assembly: OwinStartupAttribute(typeof(Dari.web.Startup))]
namespace Dari.web
{
    public partial class Startup
    {
       
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
        }
        private void createRolesandUsers()
        {
           ApplicationDbContext context = new ApplicationDbContext()  ;
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));


            // In Startup iam creating first Admin Role and creating a default Admin User     
            if (!roleManager.RoleExists("Admin"))
            {

                // first we create Admin rool    
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website                   

                var user = new ApplicationUser();
                user.FirstName = "marouan";
                user.LastName = "Beltaief";
                user.PhoneNumber = "20637676";
                user.Email = "marouan.beltaief@esprit.tn";
                user.Country = "Tunisia";

                user.UserName = "marouan.beltaief@esprit.tn";

                string userPWD = "@Azertyy123";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin    
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");

                }
            }

            // creating Creating Client role     
            if (!roleManager.RoleExists("Client"))
            {
                var role = new IdentityRole();
                role.Name = "Client";
                roleManager.Create(role);

            }

            // creating Creating Non-Verified role     
            if (!roleManager.RoleExists("Non-Verified"))
            {
                var role = new IdentityRole();
                role.Name = "Non-Verified";
                roleManager.Create(role);

            }
        }
        
    }
}

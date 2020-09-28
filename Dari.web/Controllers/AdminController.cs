using Dari.Data;
using Dari.web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static Dari.web.Models.ApplicationUser;

namespace Dari.web.Controllers
{
    [Authorize(Roles = "Admin")]


    public class AdminController : ApplicationBaseController
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private DariContext db0 = new DariContext();
        private ApplicationDbContext db = new ApplicationDbContext();


        public AdminController()
        {

        }
        public AdminController(RoleManager<IdentityRole> _roleManager)
        {
            this._roleManager = _roleManager;
        }
        public ApplicationRoleManager RoleManager 
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            }
        }

        // GET: Admin
        public ActionResult Index()
        {
            DashboardViewModel dashboard = new DashboardViewModel();

            dashboard.users_count = db.Users.Count();
            dashboard.consulors_count = db.Roles.Where(x => x.Name == "Verified").Count();
            dashboard.annonce_count = db0.AnnonceVentes.Count();
            dashboard.roles_count = db.Roles.Count();
            dashboard.request_count = db0.Requests.Count();
            return View(dashboard);
        }
        [HttpGet]
        public ActionResult GetRoles()
        {
            var roles = RoleManager.Roles.ToList();
            return View(roles);

        }
        [HttpGet]
        public ActionResult CreateRoles()
        {
            return View(new RolesModels());

        }
        [HttpPost]
        public async Task<ActionResult> CreateRoles(RolesModels model)
        {
            var roleExist = await RoleManager.RoleExistsAsync(model.Name);
            if (!roleExist)
            {
                var result = await RoleManager.CreateAsync(new IdentityRole(model.Name));
            }
            return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public  ActionResult Delete(string Name)
        {
            var r = RoleManager.FindByName(Name);
            var result =  RoleManager.Delete(r);
            return RedirectToAction("Admin","GetRoles");
        }
        [HttpGet]
        public ActionResult AddUserToRole()
        {

            return View();
        }

        [HttpPost]
        public ActionResult AddUserToRole(AddUserToRole model, string roleName)
        {
            var roleStore = new RoleStore<IdentityRole>(db);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            var userStore = new UserStore<ApplicationUser>(db);
            var userManager = new UserManager<ApplicationUser>(userStore);
            var user = userManager.FindByName(model.username);
            userManager.AddToRole(user.Id, roleName);
            return RedirectToAction("GetRoles","Admin");

        }
    }

}

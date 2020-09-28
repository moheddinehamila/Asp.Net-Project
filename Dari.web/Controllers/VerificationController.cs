using Dari.Data;
using Dari.Data.Infrastructure;
using Dari.Domain;
using Dari.Service;
using Dari.web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using static Dari.web.Models.ApplicationUser;

namespace Dari.web.Controllers
{
    public class VerificationController : ApplicationBaseController
    {
        IDataBaseFactory dbf;
        IUnitOfWork uow;
        IServiceVerification SV;
        private ApplicationUserManager _userManager;
        private DariContext db = new DariContext();
        private ApplicationDbContext context = new ApplicationDbContext();


        public VerificationController()
        {
            dbf = new DataBaseFactory();
            uow = new UnitOfWork(dbf);
            SV = new ServiceVerification(uow);


        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        // GET: Verification

        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Non-Verified")]

        public ActionResult Create()
        {
            var currentUserId = User.Identity.GetUserId();
            if (db.Verificaitions.Where(x => x.userId == currentUserId).Count() == 0) 
            {
                return View();

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Non-Verified")]
        public ActionResult Create(Verification f, HttpPostedFileBase Image)
        {

            try
            {
                var currentUserId = User.Identity.GetUserId();
                // TODO: Add insert logic here
                f.image = Image.FileName;
                f.userId = currentUserId;
               // f.status = "0";
                SV.Add(f);
                SV.Commit();


                if (Image.ContentLength > 0)
                {
                    var path = Path.Combine(Server.MapPath("~/Content/upload/Verification/"), Image.FileName);
                    Image.SaveAs(path);
                }

                return Json(new { success = true, message = "Sent Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        // [Authorize(Roles = "Admin")]
        [Authorize(Roles = "Admin")]

        public ActionResult GetData()
        {
            
                List<Verification> empList = db.Verificaitions.ToList<Verification>();
                return Json(new { data = empList }, JsonRequestBehavior.AllowGet);
            
        }
        [Authorize(Roles = "Admin")]

        [HttpGet]
        public ActionResult Inspect(int id = 0)
        {
                using (DariContext db = new DariContext())
                {
                    return View(db.Verificaitions.Where(x => x.Id == id).FirstOrDefault<Verification>());
                }

        }
        [Authorize(Roles = "Admin")]

        [HttpPost]
        public ActionResult Validate(int id = 0)
        {
            
                 var t= db.Verificaitions.Where(x => x.Id == id).FirstOrDefault<Verification>();
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);

                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                userManager.AddToRole(t.userId, "Counselors");
                userManager.RemoveFromRole(t.userId, "Non-Verified");
            Counselor c = new Counselor();
            c.FirstName = t.FirstName;
            c.LastName = t.LastName;
            c.Email = t.Email;
            c.PhoneNumber = t.PhoneNumber;
            c.Country = t.Country;
            c.address = t.address;
            c.cin = t.cin;
            c.userId = t.userId;
            c.City = t.City;
            c.image = t.image;
            c.fb = t.fb;
            c.gg = t.gg;
            c.tw = t.tw;
            db.Counselors.Add(c);
            db.SaveChanges();

            Email em = new Email();
            em.to = t.Email;
            em.subject = "New Verification Request";
            em.body = "Hello Mr : " + t.FirstName + " "+t.LastName + "\n\nYour Verification Request Have Been Validated At : " + System.DateTime.Now + "\nBy The Admin\n \n \n ------------------------------------- \nThanks For Your Support \nFor More Informations Contact us :\nPhone : +216 24 25 38 72 \nEmail : Dari.immovable@dari.tn";
            mail_send(em);

            var user = UserManager.FindById(t.userId);

            // Update it with the values from the view model
            user.FirstName = t.FirstName;
            user.LastName = t.LastName;
            user.Email = t.Email;
            user.PhoneNumber = t.PhoneNumber;
            user.Country = t.Country;
            user.UserName = t.Email;
            UserManager.Update(user);

            db.Verificaitions.Remove(t);
            db.SaveChanges();
            return Json(new { success = true, message = "Validated Successfully" }, JsonRequestBehavior.AllowGet);

        }
        [Authorize(Roles = "Admin")]

        [HttpPost]
        public ActionResult Delete(int id)
        {
           
                Verification v = db.Verificaitions.Where(x => x.Id == id).FirstOrDefault<Verification>();
            Email em = new Email();
            em.to = v.Email;
            em.subject = "Your Verification Request";
            em.body = "Hello Mr : " + v.FirstName + " " + v.LastName + "\n\nYour Verification Request Have Been Rejected At : " + System.DateTime.Now + "\nBy The Admin For lack of seriousness \n \n \n ------------------------------------- \nThanks For Your Support \nFor More Informations Contact us :\nPhone : +216 24 25 38 72 \nEmail : Dari.immovable@dari.tn";
            mail_send(em);
            db.Verificaitions.Remove(v);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            
        }


        public void mail_send(Email model)
        {
            MailMessage mm = new MailMessage("houssem.entre@gmail.com", model.to);
            mm.Subject = model.subject;
            mm.Body = model.body;
            mm.IsBodyHtml = false;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            NetworkCredential nc = new NetworkCredential("houssem.entre@gmail.com", "63845026");
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = nc;
            smtp.Send(mm);
            ViewBag.Message = "Mail has been send successfully!";

        }
    }
    }

   




    


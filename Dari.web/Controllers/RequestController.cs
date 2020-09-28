using Dari.Data.Infrastructure;
using Dari.Domain;
using Dari.Service;
using Dari.web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Dari.web.Extensions;
using System.Net.Mail;
using System.Net;
using static Dari.web.Models.ApplicationUser;
using Dari.Data;

namespace Dari.web.Controllers
{
    public class RequestController : Controller
    {
        IDataBaseFactory dbf;
        IUnitOfWork uow;
        ApplicationDbContext db = new ApplicationDbContext();

        IService<Recommendation> ServiceRecommendation;
        IServiceCountries ServiceCoutries;
        IServiceRegions ServiceRegion;
        IService<Request> ServiceRequest;
        public RequestController()
        {
            dbf = new DataBaseFactory();
            uow = new UnitOfWork(dbf);
            ServiceRecommendation = new ServiceRecommendation(uow);
            ServiceCoutries = new ServiceCountries(uow);
            ServiceRegion = new ServiceRegions(uow);
            ServiceRequest = new Service<Request>(uow);

        }
        // GET: Customer
        public ActionResult Index(String searchstring, String YourRadioButton, String min, String max)
        {

            ///////////////////
            bool val = User.Identity.IsAuthenticated;
            bool a = User.IsInRole("Client");

            ViewData["passenger"] = 0;


            if (val == true && a == false)
            {
                String ch = User.Identity.GetUserId().ToString();
                ViewData["user"] = ch;
                IEnumerable<Recommendation> rr = ServiceRecommendation.GetMany2(c => c.UserId == ch);


                // TempData["recom"] = rr;
                ViewBag.val = rr;


                List<Request> list = new List<Request>();
                var listSearch = ServiceRequest.GetAll();

                var YourRadioButt = Request.Form["s-type"];
                String aa = YourRadioButt;

                if (!String.IsNullOrEmpty(searchstring) && !String.IsNullOrEmpty(min) && !String.IsNullOrEmpty(max) && aa == "A") { var con = Convert.ToInt32(min); var con2 = Convert.ToInt32(max); listSearch = ServiceRequest.GetMany2(f => f.TypeCh.ToString() == "Buy" && f.Title.Contains(searchstring) && f.Budget >= con && f.Budget <= con2); return View(listSearch); }
                if (!String.IsNullOrEmpty(searchstring) && !String.IsNullOrEmpty(min) && !String.IsNullOrEmpty(max) && aa == "B") { var con = Convert.ToInt32(min); var con2 = Convert.ToInt32(max); listSearch = ServiceRequest.GetMany2(f => f.TypeCh.ToString() == "Rent" && f.Title.Contains(searchstring) && f.Budget >= con && f.Budget <= con2); return View(listSearch); }
                if (!String.IsNullOrEmpty(searchstring) && !String.IsNullOrEmpty(min) && !String.IsNullOrEmpty(max) && aa == "C") { var con = Convert.ToInt32(min); var con2 = Convert.ToInt32(max); listSearch = ServiceRequest.GetMany2(f => f.Title.Contains(searchstring) && f.Budget >= con && f.Budget <= con2); return View(listSearch); }
                if (!String.IsNullOrEmpty(searchstring) && !String.IsNullOrEmpty(min) && !String.IsNullOrEmpty(max) && aa != "A" && aa != "B" && aa != "C") { var con = Convert.ToInt32(min); var con2 = Convert.ToInt32(max); listSearch = ServiceRequest.GetMany2(f => f.Title.Contains(searchstring) && f.Budget >= con && f.Budget <= con2); return View(listSearch); }


                if (!String.IsNullOrEmpty(min) && !String.IsNullOrEmpty(max) && aa == "A") { var con = Convert.ToInt32(min); var con2 = Convert.ToInt32(max); listSearch = ServiceRequest.GetMany2(f => f.Budget >= con && f.Budget <= con2 && f.TypeCh.ToString() == "Buy"); return View(listSearch); }
                if (!String.IsNullOrEmpty(min) && !String.IsNullOrEmpty(max) && aa == "B") { var con = Convert.ToInt32(min); var con2 = Convert.ToInt32(max); listSearch = ServiceRequest.GetMany2(f => f.Budget >= con && f.Budget <= con2 && f.TypeCh.ToString() == "Rent"); return View(listSearch); }
                if (!String.IsNullOrEmpty(min) && !String.IsNullOrEmpty(max) && aa == "C") { var con = Convert.ToInt32(min); var con2 = Convert.ToInt32(max); listSearch = ServiceRequest.GetMany2(f => f.Budget >= con && f.Budget <= con2); return View(listSearch); }
                if (!String.IsNullOrEmpty(min) && !String.IsNullOrEmpty(max) && aa != "A" && aa != "B" && aa != "C") { var con = Convert.ToInt32(min); var con2 = Convert.ToInt32(max); listSearch = ServiceRequest.GetMany2(f => f.Budget >= con && f.Budget <= con2); return View(listSearch); }

                if (!String.IsNullOrEmpty(searchstring) && aa == "A") { listSearch = ServiceRequest.GetMany2(f => f.TypeCh.ToString() == "Buy" && f.Title.Contains(searchstring)); return View(listSearch); }
                if (!String.IsNullOrEmpty(searchstring) && aa == "B") { listSearch = ServiceRequest.GetMany2(f => f.TypeCh.ToString() == "Rent" && f.Title.Contains(searchstring)); return View(listSearch); }
                if (!String.IsNullOrEmpty(searchstring) && aa == "C") { listSearch = ServiceRequest.GetMany2(f => f.Title.Contains(searchstring)); return View(listSearch); }
                if (!String.IsNullOrEmpty(searchstring) && aa != "A" && aa != "B" && aa != "C") { listSearch = ServiceRequest.GetMany2(f => f.Title.Contains(searchstring)); return View(listSearch); }

                if (aa == "A") { listSearch = ServiceRequest.GetMany2(f => f.TypeCh.ToString() == "Buy"); return View(listSearch); }
                if (aa == "B") { listSearch = ServiceRequest.GetMany2(f => f.TypeCh.ToString() == "Rent"); return View(listSearch); }
                if (aa == "C") { listSearch = ServiceRequest.GetAll(); return View(listSearch); }
                if (aa != "A" && aa != "B" && aa != "C") { listSearch = ServiceRequest.GetAll(); return View(listSearch); }





            }
            if (val == true && a == true)
            {

                String ch = User.Identity.GetUserId().ToString();

                ViewData["user"] = ch;
                IEnumerable<Recommendation> rr = ServiceRecommendation.GetMany2(c => c.UserId == ch);


                //       TempData["recom"] = rr;
                ViewBag.val = rr;




                List<Request> list = new List<Request>();
                var listSearch = ServiceRequest.GetAll();

                var YourRadioButt = Request.Form["s-type"];
                String aa = YourRadioButt;

                if (!String.IsNullOrEmpty(searchstring) && !String.IsNullOrEmpty(min) && !String.IsNullOrEmpty(max) && aa == "A") { var con = Convert.ToInt32(min); var con2 = Convert.ToInt32(max); listSearch = ServiceRequest.GetMany2(f => f.UserId == ch && f.TypeCh.ToString() == "Buy" && f.Title.Contains(searchstring) && f.Budget >= con && f.Budget <= con2); return View(listSearch); }
                if (!String.IsNullOrEmpty(searchstring) && !String.IsNullOrEmpty(min) && !String.IsNullOrEmpty(max) && aa == "B") { var con = Convert.ToInt32(min); var con2 = Convert.ToInt32(max); listSearch = ServiceRequest.GetMany2(f => f.UserId == ch && f.TypeCh.ToString() == "Rent" && f.Title.Contains(searchstring) && f.Budget >= con && f.Budget <= con2); return View(listSearch); }
                if (!String.IsNullOrEmpty(searchstring) && !String.IsNullOrEmpty(min) && !String.IsNullOrEmpty(max) && aa == "C") { var con = Convert.ToInt32(min); var con2 = Convert.ToInt32(max); listSearch = ServiceRequest.GetMany2(f => f.UserId == ch && f.Title.Contains(searchstring) && f.Budget >= con && f.Budget <= con2); return View(listSearch); }
                if (!String.IsNullOrEmpty(searchstring) && !String.IsNullOrEmpty(min) && !String.IsNullOrEmpty(max) && aa != "A" && aa != "B" && aa != "C") { var con = Convert.ToInt32(min); var con2 = Convert.ToInt32(max); listSearch = ServiceRequest.GetMany2(f => f.UserId == ch && f.Title.Contains(searchstring) && f.Budget >= con && f.Budget <= con2); return View(listSearch); }


                if (!String.IsNullOrEmpty(min) && !String.IsNullOrEmpty(max) && aa == "A") { var con = Convert.ToInt32(min); var con2 = Convert.ToInt32(max); listSearch = ServiceRequest.GetMany2(f => f.UserId == ch && f.Budget >= con && f.Budget <= con2 && f.TypeCh.ToString() == "Buy"); return View(listSearch); }
                if (!String.IsNullOrEmpty(min) && !String.IsNullOrEmpty(max) && aa == "B") { var con = Convert.ToInt32(min); var con2 = Convert.ToInt32(max); listSearch = ServiceRequest.GetMany2(f => f.UserId == ch && f.Budget >= con && f.Budget <= con2 && f.TypeCh.ToString() == "Rent"); return View(listSearch); }
                if (!String.IsNullOrEmpty(min) && !String.IsNullOrEmpty(max) && aa == "C") { var con = Convert.ToInt32(min); var con2 = Convert.ToInt32(max); listSearch = ServiceRequest.GetMany2(f => f.UserId == ch && f.Budget >= con && f.Budget <= con2); return View(listSearch); }
                if (!String.IsNullOrEmpty(min) && !String.IsNullOrEmpty(max) && aa != "A" && aa != "B" && aa != "C") { var con = Convert.ToInt32(min); var con2 = Convert.ToInt32(max); listSearch = ServiceRequest.GetMany2(f => f.UserId == ch && f.Budget >= con && f.Budget <= con2); return View(listSearch); }

                if (!String.IsNullOrEmpty(searchstring) && aa == "A") { listSearch = ServiceRequest.GetMany2(f => f.UserId == ch && f.TypeCh.ToString() == "Buy" && f.Title.Contains(searchstring)); return View(listSearch); }
                if (!String.IsNullOrEmpty(searchstring) && aa == "B") { listSearch = ServiceRequest.GetMany2(f => f.UserId == ch && f.TypeCh.ToString() == "Rent" && f.Title.Contains(searchstring)); return View(listSearch); }
                if (!String.IsNullOrEmpty(searchstring) && aa == "C") { listSearch = ServiceRequest.GetMany2(f => f.UserId == ch && f.Title.Contains(searchstring)); return View(listSearch); }
                if (!String.IsNullOrEmpty(searchstring) && aa != "A" && aa != "B" && aa != "C") { listSearch = ServiceRequest.GetMany2(f => f.UserId == ch && f.Title.Contains(searchstring)); return View(listSearch); }

                if (aa == "A") { listSearch = ServiceRequest.GetMany2(f => f.UserId == ch && f.TypeCh.ToString() == "Buy"); return View(listSearch); }
                if (aa == "B") { listSearch = ServiceRequest.GetMany2(f => f.UserId == ch && f.TypeCh.ToString() == "Rent"); return View(listSearch); }
                if (aa == "C") { listSearch = ServiceRequest.GetMany2(c => c.UserId == ch); return View(listSearch); }
                if (aa != "A" && aa != "B" && aa != "C") { listSearch = ServiceRequest.GetMany2(c => c.UserId == ch); return View(listSearch); }






                return View(ServiceRequest.GetMany2(c => c.UserId == ch));
            }
            else { ViewData["passenger"] = 1; return View(); }

        }

        [HttpGet]
        public ActionResult GetRegions(string iso3)
        {
            if (!string.IsNullOrWhiteSpace(iso3) && iso3.Length == 3)
            {
                IEnumerable<SelectListItem> regions = ServiceRegion.GetRegions(iso3);
                return Json(regions, JsonRequestBehavior.AllowGet);
            }
            return null;
        }


        // GET: Customer/Details/5
        public ActionResult Details(int id)
        {
            User.Identity.GetUserName();




            String ch = User.Identity.GetUserId().ToString();

            //    int val2 = ServiceUser.Connectedid();

            ViewData["val"] = 1;
            //    ViewData["val1"] = ServiceUser.GetById(val2).Country;
            //       String ch = ServiceUser.GetById(val2).Country;
            if (ServiceRequest.GetById(id).UserId != ch)
            {
                String c = ServiceRequest.GetById(id).UserId;

                var uu = db.Users.FirstOrDefault(u => u.Id == c);
                string chhh = uu.Email;
                string chhhh = uu.UserName;
                Email em = new Email();
                em.to = chhh;
                em.subject = "New Request View";
                em.body = "Hello Mr : " + chhhh + "\n\nYour Request Title : " + ServiceRequest.GetById(id).Title + " \nHave Been Seen At : " + System.DateTime.Now + "\nBy Mr : " + User.Identity.Name + "\n \n \n ------------------------------------- \nThanks For Your Support \nFor More Informations Contact us :\nPhone : +216 24 25 38 72 \nEmail : Dari.immovable@dari.tn";
                mail_send(em);



                ViewData["val"] = 0;

            }

            String chh = User.Identity.Country();
            ViewData["requestsid"] = ServiceRequest.GetById(id);
            return View(ServiceRequest.GetMany2(obj => obj.CountryIso3.ToString() == chh));
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            String ch = User.Identity.GetUserId().ToString();

            IEnumerable<Recommendation> rr = ServiceRecommendation.GetMany2(c => c.UserId == ch);


            // TempData["recom"] = rr;
            ViewBag.val = rr;

            var request = new RequestViewModel()
            {

                Countries = ServiceCoutries.GetCountries(),
                Regions = ServiceRegion.GetRegions()
            };

            return View(request);
        }

        // POST: Customer/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "RequestId,Budget,Phone,TypeCh,Type,Description,Area,Date,Image,Title, SelectedCountryIso3, SelectedRegionCode")] RequestViewModel model)
        {

            try
            {
                if (ModelState.IsValid)
                {

                    Request r = new Request();
                    r.UserId = User.Identity.GetUserId();
                    r.Title = model.Title;
                    r.Area = model.Area;
                    r.Budget = model.Budget;
                    r.Date = System.DateTime.Now;
                    r.Description = model.Description;
                    r.Phone = model.Phone;
                    r.RequestId = model.RequestId;
                    r.Type = (Dari.Domain.TypeDari)model.Type;
                    r.TypeCh = (Dari.Domain.TypeChoix)model.TypeCh;

                    if (model.Budget >= 1 && model.Budget <= 70000 && model.TypeCh.ToString() == "Buy") { r.Image = "buy1.jpg"; }
                    if (model.Budget > 70000 && model.Budget <= 300000 && model.TypeCh.ToString() == "Buy") { r.Image = "buy2.jpg"; }
                    if (model.Budget > 300000 && model.TypeCh.ToString() == "Buy") { r.Image = "buy3.jpg"; }

                    if (model.Budget >= 1 && model.Budget <= 300 && model.TypeCh.ToString() == "Rent") { r.Image = "rent1.jpg"; }
                    if (model.Budget > 300 && model.Budget <= 1000 && model.TypeCh.ToString() == "Rent") { r.Image = "rent2.jpg"; }
                    if (model.Budget >= 1000 && model.TypeCh.ToString() == "Rent") { r.Image = "rent3.jpg"; }

                    r.CountryIso3 = model.SelectedCountryIso3;
                    r.RegionCode = model.SelectedRegionCode;
                    ServiceRequest.Add(r);
                    ServiceRequest.Commit();
                    return RedirectToAction("Index");

                }

                throw new ApplicationException("Invalid model");
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            String ch = User.Identity.GetUserId().ToString();

            IEnumerable<Recommendation> rr = ServiceRecommendation.GetMany2(c => c.UserId == ch);


            // TempData["recom"] = rr;
            ViewBag.val = rr;
            return View(ServiceRequest.GetById(id));

        }

        // POST: Customer/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Request model)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    if (model.Budget >= 1 && model.Budget <= 70000 && model.TypeCh.ToString() == "Buy") { model.Image = "buy1.jpg"; }
                    if (model.Budget > 70000 && model.Budget <= 300000 && model.TypeCh.ToString() == "Buy") { model.Image = "buy2.jpg"; }
                    if (model.Budget > 300000 && model.TypeCh.ToString() == "Buy") { model.Image = "buy3.jpg"; }

                    if (model.Budget >= 1 && model.Budget <= 300 && model.TypeCh.ToString() == "Rent") { model.Image = "rent1.jpg"; }
                    if (model.Budget > 300 && model.Budget <= 1000 && model.TypeCh.ToString() == "Rent") { model.Image = "rent2.jpg"; }
                    if (model.Budget >= 1000 && model.TypeCh.ToString() == "Rent") { model.Image = "rent3.jpg"; }
                    model.Date = System.DateTime.Now;
                    model.UserId = User.Identity.GetUserId();
                    ServiceRequest.Update(model);
                    ServiceRequest.Commit();
                    return RedirectToAction("Index");

                }

                throw new ApplicationException("Invalid model");
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }

        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            String ch = User.Identity.GetUserId().ToString();

            IEnumerable<Recommendation> rr = ServiceRecommendation.GetMany2(c => c.UserId == ch);


            // TempData["recom"] = rr;
            ViewBag.val = rr;
            return View(ServiceRequest.GetById(id));
        }
        // POST: Customer/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Request r)
        {

            r = ServiceRequest.GetById(id);
            ServiceRequest.Deleteq(r);
            ServiceRequest.Commit();
            return RedirectToAction("Index");
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
        public ActionResult Index_WS(String role, String user)
        {
            if (role.Equals("owner"))
            {
                var listSearch = ServiceRequest.GetAll();
                var json = listSearch.ToList();
                return Json(json.ToArray(), JsonRequestBehavior.AllowGet);
            }

            var listSearchh = ServiceRequest.GetMany2(c => c.UserId == user);
            var jsonn = listSearchh.ToList();
            return Json(jsonn.ToArray(), JsonRequestBehavior.AllowGet);
        }



        public ActionResult Index_Request_WS(String searchstring, String aa, int min, int max, String role, String user)
        {

            ///////////////////

            List<Request> list = new List<Request>();
            var listSearch = ServiceRequest.GetAll();




            if (role.Equals("owner"))
            {



                if (!String.IsNullOrEmpty(searchstring) && aa == "A") { listSearch = ServiceRequest.GetMany2(f => f.TypeCh.ToString() == "Buy" && f.Title.Contains(searchstring) && f.Budget >= min && f.Budget <= max); var json = listSearch.ToList(); return Json(json.ToArray(), JsonRequestBehavior.AllowGet); }
                if (!String.IsNullOrEmpty(searchstring) && aa == "B") { listSearch = ServiceRequest.GetMany2(f => f.TypeCh.ToString() == "Rent" && f.Title.Contains(searchstring) && f.Budget >= min && f.Budget <= max); var json = listSearch.ToList(); return Json(json.ToArray(), JsonRequestBehavior.AllowGet); }
                if (!String.IsNullOrEmpty(searchstring) && aa == "C") { listSearch = ServiceRequest.GetMany2(f => f.Title.Contains(searchstring) && f.Budget >= min && f.Budget <= max); var json = listSearch.ToList(); return Json(json.ToArray(), JsonRequestBehavior.AllowGet); }
                if (!String.IsNullOrEmpty(searchstring) && aa != "A" && aa != "B" && aa != "C") { listSearch = ServiceRequest.GetMany2(f => f.Title.Contains(searchstring) && f.Budget >= min && f.Budget <= max); var json = listSearch.ToList(); return Json(json.ToArray(), JsonRequestBehavior.AllowGet); }


                if (aa == "A") { listSearch = ServiceRequest.GetMany2(f => f.Budget >= min && f.Budget <= max && f.TypeCh.ToString() == "Buy"); var json = listSearch.ToList(); return Json(json.ToArray(), JsonRequestBehavior.AllowGet); }
                if (aa == "B") { listSearch = ServiceRequest.GetMany2(f => f.Budget >= min && f.Budget <= max && f.TypeCh.ToString() == "Rent"); var json = listSearch.ToList(); return Json(json.ToArray(), JsonRequestBehavior.AllowGet); }
                if (aa == "C") { listSearch = ServiceRequest.GetMany2(f => f.Budget >= min && f.Budget <= max); var json = listSearch.ToList(); return Json(json.ToArray(), JsonRequestBehavior.AllowGet); }
                if (aa != "A" && aa != "B" && aa != "C") { listSearch = ServiceRequest.GetMany2(f => f.Budget >= min && f.Budget <= max); var json = listSearch.ToList(); return Json(json.ToArray(), JsonRequestBehavior.AllowGet); }
                else
                {
                    var json = listSearch.ToList(); return Json(json.ToArray(), JsonRequestBehavior.AllowGet);
                }


            }




            if (!String.IsNullOrEmpty(searchstring) && aa == "A") { listSearch = ServiceRequest.GetMany2(f => f.TypeCh.ToString() == "Buy" && f.Title.Contains(searchstring) && f.Budget >= min && f.Budget <= max && f.UserId == user); var json = listSearch.ToList(); return Json(json.ToArray(), JsonRequestBehavior.AllowGet); }
            if (!String.IsNullOrEmpty(searchstring) && aa == "B") { listSearch = ServiceRequest.GetMany2(f => f.TypeCh.ToString() == "Rent" && f.Title.Contains(searchstring) && f.Budget >= min && f.Budget <= max && f.UserId == user); var json = listSearch.ToList(); return Json(json.ToArray(), JsonRequestBehavior.AllowGet); }
            if (!String.IsNullOrEmpty(searchstring) && aa == "C") { listSearch = ServiceRequest.GetMany2(f => f.Title.Contains(searchstring) && f.Budget >= min && f.Budget <= max && f.UserId == user); var json = listSearch.ToList(); return Json(json.ToArray(), JsonRequestBehavior.AllowGet); }
            if (!String.IsNullOrEmpty(searchstring) && aa != "A" && aa != "B" && aa != "C") { listSearch = ServiceRequest.GetMany2(f => f.Title.Contains(searchstring) && f.Budget >= min && f.Budget <= max && f.UserId == user); var json = listSearch.ToList(); return Json(json.ToArray(), JsonRequestBehavior.AllowGet); }


            if (aa == "A") { listSearch = ServiceRequest.GetMany2(f => f.Budget >= min && f.Budget <= max && f.TypeCh.ToString() == "Buy" && f.UserId == user); var json = listSearch.ToList(); return Json(json.ToArray(), JsonRequestBehavior.AllowGet); }
            if (aa == "B") { listSearch = ServiceRequest.GetMany2(f => f.Budget >= min && f.Budget <= max && f.TypeCh.ToString() == "Rent" && f.UserId == user); var json = listSearch.ToList(); return Json(json.ToArray(), JsonRequestBehavior.AllowGet); }
            if (aa == "C") { listSearch = ServiceRequest.GetMany2(f => f.Budget >= min && f.Budget <= max && f.UserId == user); var json = listSearch.ToList(); return Json(json.ToArray(), JsonRequestBehavior.AllowGet); }
            if (aa != "A" && aa != "B" && aa != "C") { listSearch = ServiceRequest.GetMany2(f => f.Budget >= min && f.Budget <= max && f.UserId == user); var json = listSearch.ToList(); return Json(json.ToArray(), JsonRequestBehavior.AllowGet); }

            listSearch = ServiceRequest.GetMany2(c => c.UserId == user);
            var json2 = listSearch.ToList(); return Json(json2.ToArray(), JsonRequestBehavior.AllowGet);



        }



        public ActionResult Create_Request_WS(String userid, int Budget, String Phone, int TypeCh, int Type, String Description, int Area, String Title, String SelectedCountryIso3, String SelectedRegionCode)
        {


            Request r = new Request();
            r.UserId = userid;
            r.Title = Title;
            r.Area = Area;
            r.Budget = Budget;
            r.Date = System.DateTime.Now;
            r.Description = Description;
            r.Phone = Phone;

            r.Type = (Dari.Domain.TypeDari)Type;
            r.TypeCh = (Dari.Domain.TypeChoix)TypeCh;

            if (Budget >= 1 && Budget <= 70000 && TypeCh == 0) { r.Image = "buy1.jpg"; }
            if (Budget > 70000 && Budget <= 300000 && TypeCh == 0) { r.Image = "buy2.jpg"; }
            if (Budget > 300000 && TypeCh == 0) { r.Image = "buy3.jpg"; }

            if (Budget >= 1 && Budget <= 300 && TypeCh == 1) { r.Image = "rent1.jpg"; }
            if (Budget > 300 && Budget <= 1000 && TypeCh == 1) { r.Image = "rent2.jpg"; }
            if (Budget >= 1000 && TypeCh == 1) { r.Image = "rent3.jpg"; }

            r.CountryIso3 = SelectedCountryIso3;
            r.RegionCode = SelectedRegionCode;
            ServiceRequest.Add(r);
            ServiceRequest.Commit();
            return Json("create success", JsonRequestBehavior.AllowGet);


        }
        public ActionResult Edit_Request_WS(int id, int Budget, String Phone, int TypeCh, int Type, String Description, int Area, String Title)
        {
            Request r = ServiceRequest.GetById(id);
            if (Budget >= 1 && Budget <= 70000 && TypeCh == 0) { r.Image = "buy1.jpg"; }
            if (Budget > 70000 && Budget <= 300000 && TypeCh == 0) { r.Image = "buy2.jpg"; }
            if (Budget > 300000 && TypeCh == 0) { r.Image = "buy3.jpg"; }

            if (Budget >= 1 && Budget <= 300 && TypeCh == 1) { r.Image = "rent1.jpg"; }
            if (Budget > 300 && Budget <= 1000 && TypeCh == 1) { r.Image = "rent2.jpg"; }
            if (Budget >= 1000 && TypeCh == 1) { r.Image = "rent3.jpg"; }
            r.Date = System.DateTime.Now;
            r.Budget = Budget; r.Phone = Phone; r.Type = (Domain.TypeDari)Type; r.TypeCh = (Domain.TypeChoix)TypeCh; r.Description = Description; r.Area = Area; r.Title = Title;
            ServiceRequest.Update(r);
            ServiceRequest.Commit();
            return Json("edit success", JsonRequestBehavior.AllowGet);


        }
        public ActionResult Delete_Request_WS(int id)
        {
            Request r = ServiceRequest.GetById(id);
            ServiceRequest.Deleteq(r);
            ServiceRequest.Commit();
            return Json("delete success", JsonRequestBehavior.AllowGet);
        }





        public ActionResult Suggestion_WS(String ch)
        {
            DariContext d = new DariContext();

            var listSearch = d.AnnonceVentes.Where(x=>x.Adress==ch).ToList();
            var json2 = listSearch.ToList();
            return Json(json2.ToArray(), JsonRequestBehavior.AllowGet);
        }



        public ActionResult Stat_all_WS()
        {

            var listSearch = ServiceRequest.GetAll();
            var json2 = listSearch.ToList();
            return Json(json2.ToArray(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Stat_country_all_WS(String ch)
        {

            var listSearch = ServiceRequest.GetMany2(c => c.CountryIso3 == ch);
            var json2 = listSearch.ToList();
            return Json(json2.ToArray(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Stat_typech_all_WS(String ch)
        {

            var listSearch = ServiceRequest.GetMany2(c => c.TypeCh.ToString() == ch);
            var json2 = listSearch.ToList();
            return Json(json2.ToArray(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Stat_user_WS(String usr)
        {

            var listSearch = ServiceRequest.GetMany2(c => c.UserId == usr);
            var json2 = listSearch.ToList();
            return Json(json2.ToArray(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Stat_country_user_WS(String ch, String usr)
        {

            var listSearch = ServiceRequest.GetMany2(c => c.CountryIso3 == ch && c.UserId == usr);
            var json2 = listSearch.ToList();
            return Json(json2.ToArray(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Stat_typech_user_WS(String ch, String usr)
        {

            var listSearch = ServiceRequest.GetMany2(c => c.TypeCh.ToString() == ch && c.UserId == usr);
            var json2 = listSearch.ToList();
            return Json(json2.ToArray(), JsonRequestBehavior.AllowGet);
        }

    }
}

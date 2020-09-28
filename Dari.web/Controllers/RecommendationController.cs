using Dari.Data.Infrastructure;
using Dari.Domain;
using Dari.Service;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dari.web.Controllers
{
    public class RecommendationController : Controller
    {
        IDataBaseFactory dbf;
        IUnitOfWork uow;
        IService<Recommendation> ServiceRecommendation;
        IService<Request> ServiceRequest;
        IService<AnnonceVente> ServiceVente;
        public RecommendationController()
        {
            dbf = new DataBaseFactory();
            uow = new UnitOfWork(dbf);
            ServiceRecommendation = new Service<Recommendation>(uow);
            ServiceRequest = new Service<Request>(uow);
            ServiceVente = new Service<AnnonceVente>(uow);

        }
        // GET: Recommendation
        public ActionResult Index()
        {
            bool val = User.Identity.IsAuthenticated;
            ViewData["passenger"] = 0;
            if (val == true)
            {

                String ch = User.Identity.GetUserId().ToString();

                IEnumerable<Request> li = ServiceRequest.GetMany2(c => c.UserId == ch);
                IEnumerable<AnnonceVente> li2 = ServiceVente.GetAll();

                IEnumerable<Recommendation> li3 = ServiceRecommendation.GetMany2(c=>c.UserId==ch);
                foreach (Recommendation item in li3)
                {
                    ServiceRecommendation.Deleteq(item);
                }

                var result = (from a in li
                              join b in li2 on a.TypeCh.ToString() equals b.Category
                              where (a.Budget >= b.Price - ((b.Price * 10) / 100) && a.Budget <= b.Price + ((b.Price * 10) / 100))
                              select new Recommendation
                              { UserId=a.UserId,
                                  Title = a.Title,
                                  Name = b.Name,
                                  Adress = b.Adress,
                                  Budget = a.Budget,
                                  Image = b.Image,
                                  Price = b.Price,
                                  Type = a.TypeCh,
                                  DescriptionA = b.Description,
                                  DescriptionR = a.Description


                              }).ToList();

                foreach (Recommendation item in result)
                {
                    ServiceRecommendation.Add(item);
                }
                ServiceRecommendation.Commit();





                return View(result);

            }
            else { ViewData["passenger"] = 1; return View(); }

        }
        public ActionResult Api_Index_Recommendation()
        {
            bool val = User.Identity.IsAuthenticated;
            ViewData["passenger"] = 0;
            if (val == true)
            {

                String ch = User.Identity.GetUserId().ToString();

                IEnumerable<Request> li = ServiceRequest.GetMany2(c => c.UserId == ch);
                IEnumerable<AnnonceVente> li2 = ServiceVente.GetAll();

                IEnumerable<Recommendation> li3 = ServiceRecommendation.GetMany2(c => c.UserId == ch);
                foreach (Recommendation item in li3)
                {
                    ServiceRecommendation.Deleteq(item);
                }

                var result = (from a in li
                              join b in li2 on a.TypeCh.ToString() equals b.Category
                              where (a.Budget >= b.Price - ((b.Price * 10) / 100) && a.Budget <= b.Price + ((b.Price * 10) / 100))
                              select new Recommendation
                              {
                                  UserId = a.UserId,
                                  Title = a.Title,
                                  Name = b.Name,
                                  Adress = b.Adress,
                                  Budget = a.Budget,
                                  Image = b.Image,
                                  Price = b.Price,
                                  Type = a.TypeCh,
                                  DescriptionA = b.Description,
                                  DescriptionR = a.Description


                              }).ToList();

                foreach (Recommendation item in result)
                {
                    ServiceRecommendation.Add(item);
                }
                ServiceRecommendation.Commit();





                return Json(result.ToArray(), JsonRequestBehavior.AllowGet);

            }
            else
            {
                ViewData["passenger"] = 1; return Json(JsonRequestBehavior.AllowGet);
            }

        }

        // GET: Recommendation/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Recommendation/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Recommendation/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Recommendation/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Recommendation/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Recommendation/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Recommendation/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

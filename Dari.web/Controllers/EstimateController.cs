using Dari.Data.Infrastructure;
using Dari.Domain;
using Dari.Service;
using Dari.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dari.web.Controllers
{
    public class EstimateController : Controller
    {
        IDataBaseFactory dbf;
        IUnitOfWork uow;
        IServiceCountries ServiceCoutries;
        IServiceRegions ServiceRegion;
        IService<Request> ServiceRequest;
        IService<Estimate> ServiceEstimate;


        public EstimateController()
        {
            dbf = new DataBaseFactory();
            uow = new UnitOfWork(dbf);
            ServiceCoutries = new ServiceCountries(uow);
            ServiceRegion = new ServiceRegions(uow);
            ServiceRequest = new Service<Request>(uow);
            ServiceEstimate = new Service<Estimate>(uow);


        }
        // GET: Estimate
        public ActionResult Index(int val)
        {
            ViewData["price"] = val;
            return View();
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


        // GET: Estimate/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Estimate/Create
        public ActionResult Create()
        {

            var request = new RequestViewModel()
            {

                Countries = ServiceCoutries.GetCountries(),
                Regions = ServiceRegion.GetRegions()
            };

            return View(request);
        }

        // POST: Estimate/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "RequestId,Budget,Phone,TypeCh,Type,Description,Area,Date,Image,Title, SelectedCountryIso3, SelectedRegionCode")] RequestViewModel model)
        {
            int price;

            Estimate e = new Estimate();
            e = ServiceEstimate.GetMany2(c => c.RegionCode == model.SelectedRegionCode && c.Type.ToString() == model.TypeCh.ToString()).First<Estimate>();

            price = e.MetrePrice * model.Area;
            if (model.Type.ToString() == "Villa") { price = price + ((price * 10) / 100); }
            if (model.Type.ToString() == "Fond_De_Commerce") { price = price + ((price * 20) / 100); }
            ViewData["price"] = price;

            return RedirectToAction("Index", new { val = ViewData["price"] });



        }
        public ActionResult Create_Estimate_WS(String SelectedRegionCode, String TypeCh, int Area, String Type)
        {
            int price;

            Estimate e = new Estimate();
            e = ServiceEstimate.GetMany2(c => c.RegionCode == SelectedRegionCode && c.Type.ToString() == TypeCh).First<Estimate>();

            price = e.MetrePrice * Area;
            if (Type == "Villa") { price = price + ((price * 10) / 100); }
            if (Type == "Fond_De_Commerce") { price = price + ((price * 20) / 100); }
            ViewData["price"] = price;

            return Json(price, JsonRequestBehavior.AllowGet);



        }

        // GET: Estimate/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Estimate/Edit/5
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

        // GET: Estimate/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Estimate/Delete/5
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

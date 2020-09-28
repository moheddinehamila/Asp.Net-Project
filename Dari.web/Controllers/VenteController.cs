using Dari.Data.Infrastructure;
using Dari.Domain;
using Dari.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;













namespace Dari.web.Controllers
{
    public class VenteController : Controller
    {

        IDataBaseFactory dbf;
        IUnitOfWork uow;
        IServiceVente SV;

        public VenteController()
        {
            dbf = new DataBaseFactory();
            uow = new UnitOfWork(dbf);
            SV = new ServiceVente(uow);
        }

        // GET: Vente
        public ActionResult Index()
        {



            return View(SV.GetAll());
        }

        [HttpPost]
        public ActionResult Index(string rech)
        {
            var list = SV.GetAll();

            if (!String.IsNullOrEmpty(rech))
            {
                list = list.Where(m => m.Name.Contains(rech)).ToList();
            }

            return View(list);

        }

 

 }



        // GET: Vente/Details/5
        public ActionResult Details(int id)
        {
            return View(SV.GetById(id));
        }

        // GET: Vente/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vente/Create
        [HttpPost]
        public ActionResult Create(AnnonceVente v, HttpPostedFileBase Image)
        {
            try
            {
                // TODO: Add insert logic here
                v.Image = Image.FileName;

                v.UserId = "1";

                SV.Add(v);
                SV.Commit();
                if (Image.ContentLength > 0)
                {
                    var path = Path.Combine(Server.MapPath("~/Content/upload/Vente/"), Image.FileName);
                    Image.SaveAs(path);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Vente/Edit/5
        public ActionResult Edit(int id)
        {
            return View(SV.GetById(id));
        }

        // POST: Vente/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, AnnonceVente v, HttpPostedFileBase Image)
        {
            try
            {
                if (Image.FileName.Length > 0)
                {
                    v.Image = Image.FileName;
                    var path = Path.Combine(Server.MapPath("~/Content/upload/Vente/"), Image.FileName);
                    Image.SaveAs(path);
                }
                // TODO: Add update logic here
                SV.Update(v);
                SV.Commit();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        // GET: Vente/Delete/5
        public ActionResult Delete(int id)
        {
            SV.Deleteq(SV.GetById(id));
            SV.Commit();
            return RedirectToAction("Index");
        }

        // POST: Vente/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                SV.Deleteq(SV.GetById(id));
                SV.Commit();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(SV.GetById(id));
            }
        }
    }
}

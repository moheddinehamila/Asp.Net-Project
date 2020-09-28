using Dari.Data.Infrastructure;
using Dari.Domain;
using Dari.Service;
using Dari.web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using static Dari.web.Models.ApplicationUser;

namespace Dari.web.Controllers
{
    public class FurnitureController : Controller
    {
        public string strCurrentUserId { get; private set; }
        IDataBaseFactory dbf;
        IUnitOfWork uow;
        IServiceFurniture SF;
        IServiceOrder SO;
        IServiceBasket SB;
        private List<CategoryFurniture> catgf;
        private List<TypeFurniture> tf;

        public FurnitureController()
        {
            dbf = new DataBaseFactory();
            uow = new UnitOfWork(dbf);
            SF = new ServiceFurniture(uow);
            SB = new ServiceBasket(uow);
            SO = new ServiceOrder(uow);


            catgf = new List<CategoryFurniture>
        {
                new CategoryFurniture(){CategoryFurnitureId = 1, CategoryName= "LivingRoom"},
                new CategoryFurniture(){CategoryFurnitureId = 2, CategoryName = "BedRoom"},
                new CategoryFurniture(){CategoryFurnitureId = 3, CategoryName = "DiningRoom"},
                new CategoryFurniture(){CategoryFurnitureId = 4, CategoryName = "BathRoom"},
                new CategoryFurniture(){CategoryFurnitureId = 5, CategoryName = "Other"}
        };


            tf = new List<TypeFurniture>()
        {
            new TypeFurniture(){TypeFurnitureId = 1,  TypeName = "Sofas",CategoryFurnitureId= 1},
            new TypeFurniture(){TypeFurnitureId = 2,  TypeName = "Tables and Chairs",CategoryFurnitureId= 1},
            new TypeFurniture(){TypeFurnitureId = 3,  TypeName = "Storage",CategoryFurnitureId= 1},
            new TypeFurniture(){TypeFurnitureId = 4,  TypeName = "Decor",CategoryFurnitureId= 1},

            new TypeFurniture(){TypeFurnitureId = 5,  TypeName = "Matresses",CategoryFurnitureId= 2},
            new TypeFurniture(){TypeFurnitureId = 6,  TypeName = "Beds",CategoryFurnitureId= 2},
            new TypeFurniture(){TypeFurnitureId = 7,  TypeName = "Storage",CategoryFurnitureId= 2},
            new TypeFurniture(){TypeFurnitureId = 8,  TypeName = "Decor",CategoryFurnitureId= 2},
            new TypeFurniture(){TypeFurnitureId = 9,  TypeName = "Accessories",CategoryFurnitureId= 2},

            new TypeFurniture(){TypeFurnitureId = 10,  TypeName = "Tables and Chairs",CategoryFurnitureId= 3},
            new TypeFurniture(){TypeFurnitureId = 11,  TypeName = "Storage",CategoryFurnitureId= 3},
            new TypeFurniture(){TypeFurnitureId = 12,  TypeName = "Decor",CategoryFurnitureId= 3},

            new TypeFurniture(){TypeFurnitureId = 13,  TypeName = "Furniture",CategoryFurnitureId= 4},
            new TypeFurniture(){TypeFurnitureId = 14,  TypeName = "Accessories",CategoryFurnitureId= 4},

            new TypeFurniture(){TypeFurnitureId = 15,  TypeName = "Gargen",CategoryFurnitureId= 5},
            new TypeFurniture(){TypeFurnitureId = 16,  TypeName = "Garage",CategoryFurnitureId= 5},
        };
        }

        // GET: Furniture 
        public ActionResult Indexfurniture(string type, string categ)
        {
            IList<Furniture> x = new List<Furniture>();
            foreach (var flist in SF.GetAll())
            {
                if (flist.Qte > 0)
                {
                    x.Add(flist);
                }
            }
            if (x.Count > 0)
            {

                if ((!String.IsNullOrEmpty(categ)) && (!String.IsNullOrEmpty(type)))
                {
                    x = x.Where(m => m.Category.Equals(categ)).ToList();
                    x = x.Where(m => m.Type.Equals(type)).ToList();
                }
            }
            TempData["list"] = x;
            return View(x);

        }



        [HttpPost]
        public ActionResult Indexfurniture(string rech)
        {
            IList<Furniture> x = new List<Furniture>();
            IList<Furniture> list = new List<Furniture>();

            list = TempData["list"] as List<Furniture>;
            foreach (var flist in list)
            {
                x.Add(flist);
            }

            if (!String.IsNullOrEmpty(rech))
            {
                x = x.Where(m => m.Name.ToUpper().Contains(rech.ToUpper())).ToList();
            }

            TempData["list"] = x;
            return View(x);

        }

        public ActionResult Api_indexfurniture()
        {
            IList<Furniture> x = new List<Furniture>();
            foreach (var flist in SF.GetAll())
            {
                if (flist.Qte > 0)
                {
                    x.Add(flist);
                }
            }
            return Json(x.ToArray(), JsonRequestBehavior.AllowGet);

        }

        ///////////////////////////////////////////////////Furniture owner

        [Authorize(Roles = "Owner")]
        public ActionResult Indexfurniture_owner(string type, string categ)
        {

            strCurrentUserId = User.Identity.GetUserId();
            var x = SF.GetByuserid(strCurrentUserId);

            if ((!String.IsNullOrEmpty(categ)) && (!String.IsNullOrEmpty(type)))
            {
                x = x.Where(m => m.Category.Equals(categ)).ToList();
                x = x.Where(m => m.Type.Equals(type)).ToList();
            }
            return View(x);
        }

        [HttpPost]
        [Authorize(Roles = "Owner")]
        public ActionResult Indexfurniture_owner(string rech)
        {
            IList<Furniture> x = new List<Furniture>();

            strCurrentUserId = User.Identity.GetUserId();
            foreach (var flist in SF.GetByuserid(strCurrentUserId))
            {
                if (flist.Qte > 0)
                {
                    x.Add(flist);
                }
            }

            if (!String.IsNullOrEmpty(rech))
            {
                x = x.Where(m => m.Name.ToUpper().Contains(rech.ToUpper())).ToList();
            }

            return View(x);

        }

        [Authorize(Roles = "Owner")]
        public ActionResult Create()
        {
            ViewBag.Category = new SelectList(catgf, "CategoryFurnitureId", "CategoryName").ToList();

            return View();
        }

        public JsonResult GetType(int CategoryFurnitureId)
        {
            return Json(tf.Where(s => s.CategoryFurnitureId == CategoryFurnitureId), JsonRequestBehavior.AllowGet);
        }

        // POST: Furniture/Create
        [HttpPost]
        [Authorize(Roles = "Owner")]
        public ActionResult Create(Furniture f, HttpPostedFileBase Image)
        {
            ViewBag.Category = new SelectList(catgf, "CategoryFurnitureId", "CategoryName").ToList();

            try
            {
                // TODO: Add insert logic here
                f.Image = Image.FileName;

                f.Type = tf[(int.Parse(f.Type) - 1)].TypeName;

                f.Category = catgf[(int.Parse(f.Category) - 1)].CategoryName;

                ApplicationDbContext db = new ApplicationDbContext();
                var user = db.Users.Where(x => x.PhoneNumber == User.Identity.GetUserId()).FirstOrDefault();

                strCurrentUserId = User.Identity.GetUserId();
                f.UserId = strCurrentUserId;
                SF.Add(f);
                SF.Commit();
                if (Image.ContentLength > 0)
                {
                    var path = Path.Combine(Server.MapPath("~/Content/upload/furniture/"), Image.FileName);
                    Image.SaveAs(path);
                }

                return RedirectToAction("Indexfurniture_owner");
            }
            catch
            {
                return View();
            }
        }


        // GET: Furniture/Edit/5
        [Authorize(Roles = "Owner")]
        public ActionResult Edit(int id)
        {

            ViewBag.Category = new SelectList(catgf, "CategoryFurnitureId", "CategoryName").ToList();

            return View(SF.GetById(id));
        }

        // POST: Furniture/Edit/5
        [HttpPost]
        [Authorize(Roles = "Owner")]
        public ActionResult Edit(int id, Furniture f, HttpPostedFileBase Image)
        {
            ViewBag.Category = new SelectList(catgf, "CategoryFurnitureId", "CategoryName").ToList();

            try
            {
                if (Image.FileName.Length > 0)
                {
                    f.Image = Image.FileName;
                    var path = Path.Combine(Server.MapPath("~/Content/upload/furniture/"), Image.FileName);
                    Image.SaveAs(path);
                }
                // TODO: Add update logic here
                f.FurnitureId = id;

                strCurrentUserId = User.Identity.GetUserId();
                f.UserId = strCurrentUserId;
                f.Category = catgf[(int.Parse(f.Category) - 1)].CategoryName;

                f.Type = tf[(int.Parse(f.Type) - 1)].TypeName;

                SF.Update(f);
                SF.Commit();
                return RedirectToAction("Indexfurniture_owner");
            }
            catch
            {
                return View();
            }
        }

        // GET: Furniture/Delete/5
        [Authorize(Roles = "Owner")]
        public ActionResult Delete(int id)
        {
            var f = SF.GetById(id);
            var listb = SB.GetAll();

            if (listb != null)
            {
                foreach (var lb in listb)
                {
                    if (f.FurnitureId == lb.FurnitureId)
                    {
                        SB.Deleteq(lb);
                    }
                }
            }

            var listo = SO.GetAll();

            if (listo != null)
            {
                foreach (var lo in listo)
                {
                    if (f.FurnitureId == lo.FurnitureId)
                    {
                        SO.Deleteq(lo);
                    }
                }
            }
            SF.Deleteq(f);
            SF.Commit();
            return RedirectToAction("Indexfurniture_owner");
        }

        public ActionResult Api_indexfurniture_owner()
        {
            var x = SF.GetByuserid("5802fc60-d45f-4ade-8552-e7c2f6ce3bd2").ToList();

            return Json(x.ToArray(), JsonRequestBehavior.AllowGet);

        }


        public ActionResult Api_getbyid(int id)
        {
            var x = SF.GetByuserid("5802fc60-d45f-4ade-8552-e7c2f6ce3bd2");

            x = x.Where(m => m.FurnitureId.Equals(id));

            return Json(x.ToArray(), JsonRequestBehavior.AllowGet);

        }

        public ActionResult Api_delete(int id)
        {
            var f = SF.GetById(id);
            var listb = SB.GetAll();

            if (listb != null)
            {
                foreach (var lb in listb)
                {
                    if (f.FurnitureId == lb.FurnitureId)
                    {
                        SB.Deleteq(lb);
                    }
                }
            }

            var listo = SO.GetAll();

            if (listo != null)
            {
                foreach (var lo in listo)
                {
                    if (f.FurnitureId == lo.FurnitureId)
                    {
                        SO.Deleteq(lo);
                    }
                }
            }
            SF.Deleteq(f);
            SF.Commit();

            var x = SF.GetByuserid("5802fc60-d45f-4ade-8552-e7c2f6ce3bd2");

            return Json(x.ToArray(), JsonRequestBehavior.AllowGet);

        }

    }
}

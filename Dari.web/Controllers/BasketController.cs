using Dari.Data.Infrastructure;
using Dari.Domain;
using Dari.Service;
using System;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;

namespace Dari.web.Controllers
{
    public class BasketController : Controller
    {
        public string strCurrentUserId { get; private set; }
        IDataBaseFactory dbf;
        IUnitOfWork uow;
        IServiceBasket SB;
        IServiceFurniture SF;
        IServiceCoupon SC;
        IServiceOrder SO;

        public BasketController()
        {

            dbf = new DataBaseFactory();
            uow = new UnitOfWork(dbf);
            SB = new ServiceBasket(uow);
            SC = new ServiceCoupon(uow);
            SF = new ServiceFurniture(uow);
            SO = new ServiceOrder(uow);
        }

        [Authorize(Roles = "Client")]
        public ActionResult Indexbasket()
        {
            strCurrentUserId = User.Identity.GetUserId();
            return View(SB.GetByuserid(strCurrentUserId));
        }


        [Authorize(Roles = "Client")]
        public ActionResult Order()
        {
            int p = Convert.ToInt32(TempData["p"]);
            string u = Convert.ToString(TempData["used"]);
            ViewData["pourcentage"] = p;
            ViewData["used"] = u;
            strCurrentUserId = User.Identity.GetUserId();
            return View(SB.GetByuserid(strCurrentUserId));
        }

        [Authorize(Roles = "Client")]
        public ActionResult coupon(string id)
        {

            var allcpn = SC.GetAll();
            if (allcpn != null)
            {
                foreach (var item in allcpn)
                {
                    if (item.Couponvalue.Equals(id))
                    {
                        if (item.Used)
                        {
                            TempData["used"] = "This key is already used, try another one";

                        }
                        else
                        {
                            item.Used = true;

                            SC.Update(item);
                            TempData["p"] = item.Pourcentage;

                        }
                    }
                }
            }
            SC.Commit();


            return RedirectToAction("order");
        }

        [Authorize(Roles = "Client")]
        public ActionResult Addbill(int id)
        {

            Order o;
            double bprice = 0;
            strCurrentUserId = User.Identity.GetUserId();
            var listb = SB.GetByuserid(strCurrentUserId);
            var listo = SO.GetByuserid(strCurrentUserId);
            int lastofid = 0;

            if (listo != null)
            {
                foreach (var ord in listo)
                {
                    if (lastofid < ord.BillId) { lastofid = ord.BillId; }
                }
            }

            if (listb != null)
            {
                foreach (var b in listb)
                {
                    bprice = bprice + b.TotalPrice;
                }
                foreach (var b in listb)
                {
                    o = new Order();
                    o.FurnitureId = b.FurnitureId;
                    o.Qte = b.Qte;
                    o.UserId = b.UserId;
                    o.TotalPrice = b.TotalPrice;
                    if (id > 0) { o.FinalPrice = bprice - (bprice * id / 100); }
                    else { o.FinalPrice = bprice; }
                    o.BillId = lastofid + 1;
                    SO.Add(o);

                    var f = SF.GetById(b.FurnitureId.Value);

                    f.Qte = f.Qte - b.Qte;
                    SF.Update(f);
                    SB.Deleteq(b);
                }

            }
            SB.Commit();


            TempData["lastofid"] = lastofid;
            return RedirectToAction("bill");
        }

        [Authorize(Roles = "Client")]
        public ActionResult Bill()
        {
            int lastofid = Convert.ToInt32(TempData["lastofid"]);

            IList<Order> x = new List<Order>();

            strCurrentUserId = User.Identity.GetUserId();
            foreach (var flist in SO.GetByuserid(strCurrentUserId))
            {
                if (flist.BillId == (lastofid + 1))
                {
                    x.Add(flist);
                }
            }
            if (x.Count > 0)
            {
                return View(x);
            }
            else { return RedirectToAction("indexbasket"); }
        }

        [Authorize(Roles = "Client")]
        public ActionResult SubstractQte(int id)
        {
            var b = SB.GetById(id);
            var f = SF.GetById(b.FurnitureId.Value);
            if (b.Qte > 1)
            {
                b.Qte--;
                b.TotalPrice = b.Qte * f.Price;

                SB.Update(b);
                SB.Commit();
            }
            return RedirectToAction("indexbasket");
        }

        [Authorize(Roles = "Client")]
        public ActionResult Addqte(int id)
        {
            var b = SB.GetById(id);
            var f = SF.GetById(b.FurnitureId.Value);
            if (b.Qte < f.Qte)
            {
                b.Qte++;
                b.TotalPrice = b.Qte * f.Price;

                SB.Update(b);
                SB.Commit();
            }
            return RedirectToAction("indexbasket");
        }

        [Authorize(Roles = "Client")]
        public ActionResult Addtobasket(int id)
        {
            strCurrentUserId = User.Identity.GetUserId();
            var f = SF.GetById(id);
            var list = SB.GetAll();
            var exist = false;
            if (list != null)
            {
                foreach (var l in list)
                {
                    if (f.FurnitureId == l.FurnitureId && l.UserId.Equals(strCurrentUserId)) { exist = true; }
                }
                if (exist == false)
                {

                    Basket b = new Basket();
                    b.furniture = f;
                    b.UserId = strCurrentUserId;
                    b.Qte = 1;
                    b.FurnitureId = id;
                    b.TotalPrice = f.Price;
                    SB.Add(b);
                    SB.Commit();
                }
            }
            return RedirectToAction("indexbasket");
        }

        [Authorize(Roles = "Client")]
        public ActionResult Delete(int id)
        {
            SB.Deleteq(SB.GetById(id));
            SB.Commit();
            return RedirectToAction("indexbasket");
        }


        public ActionResult Api_indexbasket()
        {
            var x = SB.GetByuserid("6035cc13-edae-410b-9519-cf799f71990a").ToList();
            return Json(x.ToArray(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult api_SubstractQte(int id)
        {
            var b = SB.GetById(id);
            var f = SF.GetById(b.FurnitureId.Value);
            if (b.Qte > 1)
            {
                b.Qte--;
                b.TotalPrice = b.Qte * f.Price;

                SB.Update(b);
                SB.Commit();
            }
            return RedirectToAction("Api_indexbasket");
        }


        public ActionResult Api_Addqte(int id)
        {
            var b = SB.GetById(id);
            var f = SF.GetById(b.FurnitureId.Value);
            if (b.Qte < f.Qte)
            {
                b.Qte++;
                b.TotalPrice = b.Qte * f.Price;

                SB.Update(b);
                SB.Commit();
            }
            return RedirectToAction("Api_indexbasket");
        }

        /*   public ActionResult Addtobasket(int id)
         {
             strCurrentUserId = User.Identity.GetUserId();
             var f = SF.GetById(id);
             var list = SB.GetAll();
             var exist = false;
             if (list != null)
             {
                 foreach (var l in list)
                 {
                     if (f.FurnitureId == l.FurnitureId && l.UserId.Equals(strCurrentUserId)) { exist = true; }
                 }
                 if (exist == false)
                 {

                     Basket b = new Basket();
                     b.furniture = f;
                     b.UserId = strCurrentUserId;
                     b.Qte = 1;
                     b.FurnitureId = id;
                     b.TotalPrice = f.Price;
                     SB.Add(b);
                     SB.Commit();
                 }
             }
             return RedirectToAction("indexbasket");
         }
  */
        public ActionResult Api_Delete(int id)
        {
            SB.Deleteq(SB.GetById(id));
            SB.Commit();
            return RedirectToAction("api_indexbasket");
        }



    }
}

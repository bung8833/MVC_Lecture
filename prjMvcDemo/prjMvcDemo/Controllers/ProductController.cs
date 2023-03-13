using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Profile;

namespace prjMvcDemo.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult List()
        {
            string keyword = Request.Form["txtKeyword"];
            dbDemoEntities db = new dbDemoEntities();
            IEnumerable<tProduct> datas = null;
            if (String.IsNullOrEmpty(keyword))
            {
                datas = from p in db.tProduct
                        select p;
            }
            else
            {
                datas = db.tProduct.Where(p => p.fName.Contains(keyword));
            }

            return View(datas);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(tProduct p)
        {
            dbDemoEntities db = new dbDemoEntities();
            db.tProduct.Add(p);
            db.SaveChanges();
            return RedirectToAction("List");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("List");
            }

            dbDemoEntities db = new dbDemoEntities();
            tProduct prod = db.tProduct.FirstOrDefault(p => p.fId == id);
            if (prod != null)
            {
                db.tProduct.Remove(prod);
                db.SaveChanges();
            }

            return RedirectToAction("List");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("List");
            }

            dbDemoEntities db = new dbDemoEntities();
            tProduct prod = db.tProduct.FirstOrDefault(p => p.fId == id);
            if (prod != null)
            {
                return View(prod);
            }

            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult Edit(tProduct pIn)
        {
            dbDemoEntities db = new dbDemoEntities();
            tProduct prod = db.tProduct.FirstOrDefault(p => p.fId == id);
            if (prod != null)
            {
                prod.fName = pIn.fName;
                prod.fQty = pIn.fQty;
                prod.fCost = pIn.fCost;
                prod.fPrice = pIn.fPrice;
                db.SaveChanges();
            }

            return RedirectToAction("List");
        }
    }
}
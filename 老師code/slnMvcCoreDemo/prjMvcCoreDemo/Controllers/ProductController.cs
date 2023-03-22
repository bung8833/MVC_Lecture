using Microsoft.AspNetCore.Mvc;
using prjMvcCoreDemo.Models;
using prjMvcCoreDemo.ViewModels;

namespace prjMvcCoreDemo.Controllers
{
    public class ProductController : SuperController
    {
        IWebHostEnvironment _enviro;
        public ProductController(IWebHostEnvironment p)
        {
            _enviro=p;
        }

        public IActionResult List(CQueryKeywordViewModel vm)
        {
            dbDemoContext db = new dbDemoContext();

            IEnumerable<TProduct> datas = null;
            if (string.IsNullOrEmpty(vm.txtKeyword))
            {
                datas = from p in db.TProducts
                        select p;
            }
            else
                datas = db.TProducts.Where(p => p.FName.Contains(vm.txtKeyword));

            return View(datas);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
                return RedirectToAction("List");

            dbDemoContext db = new dbDemoContext();
            TProduct prod = db.TProducts.FirstOrDefault(p => p.FId == id);
            if (prod == null)
                return RedirectToAction("List");
            return View(prod);
        }
        [HttpPost]
        public IActionResult Edit(CProductWrap pIn)
        {
            dbDemoContext db = new dbDemoContext();
            TProduct prod = db.TProducts.FirstOrDefault(p => p.FId == pIn.FId);
            if (prod != null)
            {
                if (pIn.photo != null)
                {
                    string photoName = Guid.NewGuid().ToString() + ".jpg";
                    string path = _enviro.WebRootPath + "/images/" + photoName;
                    pIn.photo.CopyTo(new FileStream(path, FileMode.Create));
                    prod.FImagePath = photoName;
                }
                prod.FName = pIn.FName;
                prod.FQty = pIn.FQty;
                prod.FCost = pIn.FCost;
                prod.FPrice = pIn.FPrice;
              
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }
        public IActionResult Delete(int? id)
        {
            if (id != null)
            {
                dbDemoContext db = new dbDemoContext();
                TProduct prod = db.TProducts.FirstOrDefault(p => p.FId == id);
                if (prod != null)
                {
                    db.TProducts.Remove(prod);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("List");
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(TProduct p)
        {
            dbDemoContext db = new dbDemoContext();
            db.TProducts.Add(p);
            db.SaveChanges();
            return RedirectToAction("List");
        }
    }
}

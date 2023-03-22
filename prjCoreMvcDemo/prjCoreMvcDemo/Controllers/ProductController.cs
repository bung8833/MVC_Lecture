using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prjCoreMvcDemo.Models;
using prjCoreMvcDemo.ViewModels;

namespace prjCoreMvcDemo.Controllers
{
    public class ProductController : SuperController
    {
        public IActionResult List(QueryKeywordVM vm)
        {
            dbDemoContext db = new dbDemoContext();

            IEnumerable<TCustomer> datas = null;
            if (String.IsNullOrEmpty(vm.txtKeyword))
            {
                datas = db.TCustomers.Select(p => p);
            }
            else
            {
                datas = db.TCustomers.Where(p => p.FName.Contains(vm.txtKeyword)
                || p.FPhone.Contains(vm.txtKeyword)
                || p.FEmail.Contains(vm.txtKeyword)
                || p.FAddress.Contains(vm.txtKeyword));
            }

            return View(datas);
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

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("List");
            }

            dbDemoContext db = new dbDemoContext();
            TProduct prod = db.TProducts.FirstOrDefault(p => p.FId == id);
            if (prod == null)
            {
                return RedirectToAction("List");
            }

            return View(prod);
        }

        [HttpPost]
        public IActionResult Edit(TProduct pIn)
        {
            dbDemoContext db = new dbDemoContext();
            TProduct prod = db.TProducts.FirstOrDefault(p => p.FId == pIn.FId);
            if (prod != null)
            {
                prod.FName = pIn.FName;
                prod.FQty = pIn.FQty;
                prod.FPrice = pIn.FPrice;
                prod.FCost = pIn.FCost;
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }
    }
}

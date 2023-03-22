using Microsoft.AspNetCore.Mvc;
using prjMvcCoreDemo.Models;
using prjMvcCoreDemo.ViewModels;
using System.Data;

namespace prjMvcCoreDemo.Controllers
{
    public class CustomerController : SuperController
    {
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return RedirectToAction("List");
           
            dbDemoContext db = new dbDemoContext();
            TCustomer cust = db.TCustomers.FirstOrDefault(p => p.FId == id);
            if (cust == null)
                return RedirectToAction("List");
            return View(cust);
        }
        [HttpPost]
        public IActionResult Edit(TCustomer pIn)
        {
            dbDemoContext db = new dbDemoContext();
            TCustomer cust = db.TCustomers.FirstOrDefault(p => p.FId == pIn.FId);
            if (cust != null)
            {
                cust.FName = pIn.FName;
                cust.FPhone = pIn.FPhone;
                cust.FEmail = pIn.FEmail;
                cust.FAddress = pIn.FAddress;
                cust.FPassword = pIn.FPassword;
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }
            public IActionResult Delete(int? id)
        {
            if (id != null)
            {
                dbDemoContext db = new dbDemoContext();
                TCustomer cust = db.TCustomers.FirstOrDefault(p => p.FId == id);
                if (cust != null)
                {
                    db.TCustomers.Remove(cust);
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
        public IActionResult Create(TCustomer p)
        {
            dbDemoContext db = new dbDemoContext();
            db.TCustomers.Add(p);
            db.SaveChanges();
            return RedirectToAction("List");
        }

        public IActionResult List(CQueryKeywordViewModel vm)
        {
            dbDemoContext db = new dbDemoContext();
            
            IEnumerable<TCustomer> datas = null;
            if (string.IsNullOrEmpty(vm.txtKeyword))
            {
                datas = from p in db.TCustomers
                        select p;
            }
            else
                datas = db.TCustomers.Where(p => p.FName.Contains(vm.txtKeyword)
                || p.FPhone.Contains(vm.txtKeyword)
                || p.FEmail.Contains(vm.txtKeyword)
                || p.FAddress.Contains(vm.txtKeyword));

            return View(datas);
        }
    }
}

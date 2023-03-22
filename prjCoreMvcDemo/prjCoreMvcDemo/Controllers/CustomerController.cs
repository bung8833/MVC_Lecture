using Microsoft.AspNetCore.Mvc;
using prjCoreMvcDemo.Models;

namespace prjCoreMvcDemo.Controllers
{
    public class CustomerController : SuperController
    {
        IWebHostEnvironment _envir;
        CustomerController(IWebHostEnvironment p)
        {
            _envir = p;
        }

        public IActionResult List()
        {
            dbDemoContext db = new dbDemoContext();

            var datas = from c in db.TCustomers
                        select c;

            return View(datas);
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

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("List");
            }

            dbDemoContext db = new dbDemoContext();
            TCustomer customer = db.TCustomers.FirstOrDefault(p => p.FId == id);
            if (customer == null)
            {
                return RedirectToAction("List");
            }

            return View(customer);
        }

        [HttpPost]
        public IActionResult Edit(TCustomer pIn)
        {
            dbDemoContext db = new dbDemoContext();
            TCustomer customer = db.TCustomers.FirstOrDefault(p => p.FId == pIn.FId);
            if (customer != null)
            {
                customer.FName = pIn.FName;
                customer.FPhone = pIn.FPhone;
                customer.FEmail = pIn.FEmail;
                customer.FAddress = pIn.FAddress;
                customer.FPassword = pIn.FPassword;
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }
    }
}

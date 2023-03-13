using prjMvcDemo.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace prjMvcDemo.Controllers
{
    public class CustomerController : Controller
    {


        //------------------------------//
        //------------------------------//
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return RedirectToAction("List");

            Customer x = (new CustomerFactory()).QueryById((int)id);
            if (x == null)
                return RedirectToAction("List");

            return View(x);
        }
        [HttpPost]
        public ActionResult Edit(Customer x)
        {
            (new CustomerFactory()).update(x);
            return RedirectToAction("List");
        }
        //------------------------------//
        //------------------------------//

        public ActionResult Delete(int? id)
        {
            if (id == null) return View();

            new CustomerFactory().Delete((int)id);

            return View();
        }


        public ActionResult Save()
        {
            Customer customer = new Customer();
            customer.fName = Request.Form["txtName"];
            customer.fPhone = Request.Form["txtPhone"];
            customer.fEmail = Request.Form["txtEmail"];
            customer.fAddress = Request.Form["txtAddress"];
            customer.fPassword = Request.Form["txtPassword"];

            new CustomerFactory().Create(customer);
            return RedirectToAction("List");
        }


        public ActionResult Create()
        {
            return View();
        }


        public ActionResult List()
        {
            string keyword = Request.Form["txtKeyword"];
            List<Customer> customers = null;

            if (String.IsNullOrEmpty(keyword))
            {
                customers = new CustomerFactory().QueryAll();
            }
            else
            {
                customers = new CustomerFactory().QueryByKeyword(keyword);
            }

            return View(customers);
        }
    }
}
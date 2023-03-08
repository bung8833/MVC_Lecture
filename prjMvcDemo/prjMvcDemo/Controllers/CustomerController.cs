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
            List<Customer> customers = new CustomerFactory().QueryAll();
            return View(customers);
        }
    }
}
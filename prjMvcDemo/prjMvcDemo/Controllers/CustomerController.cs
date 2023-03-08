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
        public ActionResult List()
        {
            List<Customer> customers = new CustomerFactory().QueryAll();
            return View(customers);
        }
    }
}
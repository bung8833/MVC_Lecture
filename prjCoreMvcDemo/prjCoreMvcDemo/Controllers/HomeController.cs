using Microsoft.AspNetCore.Mvc;
using prjCoreMvcDemo.Models;
using prjCoreMvcDemo.ViewModels;
using System.Diagnostics;
using System.Text.Json;

namespace prjCoreMvcDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //-------------------------//

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginVM vm)
        {
            TCustomer user = new dbDemoContext().TCustomers.FirstOrDefault(
                c => c.FEmail.Equals(vm.txtAccount) && c.FPassword.Equals(vm.txtPassword));

            if (user != null)
            {
                string json = JsonSerializer.Serialize(user);
                HttpContext.Session.SetString(SKDictionary.SK_LOGINED_USER, json);
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.Keys.Contains(SKDictionary.SK_LOGINED_USER))
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        //-------------------------//

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using prjMauiDemo.Models;
using prjMvcCoreDemo.Models;
using System.Text.Json;

namespace prjMvcCoreDemo.Controllers
{
    public class AController : Controller
    {
        public string demoObj2Json()
        {
            TCustomer x = new TCustomer()
            {
                FId = 1,
                FName = "Marco",
                FPhone = "0966541254",
                FEmail = "marco@gmail.com",
                FAddress = "Taipei",
                FPassword = "123"
            };
            string json = JsonSerializer.Serialize(x);
            return json;
        }
        public string demoJson2Obj()
        {
            string json = demoObj2Json();
            TCustomer x = JsonSerializer.Deserialize<TCustomer>(json);
            return x.FName + "<br/>" + x.FPhone;
        }

        IWebHostEnvironment _enviro;
        public AController(IWebHostEnvironment p)
        {
            _enviro = p;
        }
        public IActionResult showCountBySession()
        {
            int count = 0;
            if (HttpContext.Session.Keys.Contains("COUNT"))
                count = (int)HttpContext.Session.GetInt32("COUNT");
            count++;
            HttpContext.Session.SetInt32("COUNT", count);
            ViewBag.COUNT = count;
            return View();
        }
        public string sayHello()
        {
            return "Hello Asp.Net MVC core";
        }
        public string lotto()
        {
            return (new CLottoGen()).getNumber();
        }
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult fileUploadDemo()
        {
            return View();
        }
        [HttpPost]
        public ActionResult fileUploadDemo(IFormFile photo)
        {
            string path = _enviro.WebRootPath + "/images/001.jpg" ;
            photo.CopyTo(new FileStream(path, FileMode.Create));

            return View();
        }
    }
}

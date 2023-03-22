using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json;
using System.Text.Json;
using prjCoreMvcDemo.Models;

namespace prjCoreMvcDemo.Controllers
{
    public class AController : Controller
    {
        public string DemoObj2Json()
        {
            TCustomer x = new TCustomer()
            {
                FId = 1,
                FName = "Marco",
                FPhone = "0987654321",
                FEmail = "ba8@gmail.com",
                FAddress = "KH",
                FPassword = "password",
            };

            string json = JsonSerializer.Serialize(x);
            return json;
        }

        public string DemoJson2Obj()
        {
            string json = DemoObj2Json();
            TCustomer x = JsonSerializer.Deserialize<TCustomer>(json);

            return String.Empty;
        }


        public IActionResult ShowCountBySession()
        {
            int count = 0;

            // get value
            if (HttpContext.Session.Keys.Contains("COUNT"))
            {
                count = (int)HttpContext.Session.GetInt32("COUNT");
            }

            count++;

            // set value
            HttpContext.Session.SetInt32("COUNT", count);

            ViewBag.Count = count;
            return View();
        }


        public string SayHello()
        {
            return "ASP.NET MVC Core";
        }
    }
}

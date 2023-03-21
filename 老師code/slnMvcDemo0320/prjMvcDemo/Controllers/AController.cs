using prjMauiDemo.Models;
using prjMvcDemo.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjMvcDemo.Controllers
{
    public class AController : Controller
    {
        static int count=0;
        public ActionResult showCountByCookie()
        {
            
            int count = 0;
            HttpCookie c = Request.Cookies["COUNT"];
            if (c != null)
                count = Convert.ToInt32(c.Value);
            count++;
            c = new HttpCookie("COUNT");
            c.Value = count.ToString();
            c.Expires=DateTime.Now.AddSeconds(20);
            Response.Cookies.Add(c);

            ViewBag.COUNT = count;
            return View();
        }
        public ActionResult showCountBySession()
        {
            int count=0;
            if (Session["COUNT"] != null)
                count = (int)Session["COUNT"];
            count++;
            Session["COUNT"] = count;
            ViewBag.COUNT=count;
            return View();
        }
        public ActionResult showCount()
        {
            count++;
            ViewBag.COUNT = count;
            return View();
        }
        public ActionResult fileUploadDemo()
        {
            return View();
        }
        [HttpPost]
        public ActionResult fileUploadDemo(HttpPostedFileBase photo)
        {
            photo.SaveAs(@"C:\QNote\01.jpg");
            return View();
        }
        public ActionResult demoForm()
        {
            ViewBag.ANS = "?";
            if (!string.IsNullOrEmpty(Request.Form["txtA"]))
            {
                double a = Convert.ToDouble(Request.Form["txtA"]);
                double b = Convert.ToDouble(Request.Form["txtB"]);
                double c = Convert.ToDouble(Request.Form["txtC"]);
                ViewBag.a = a;
                ViewBag.b = b;
                ViewBag.c = c;
                double r = b * b - 4 * a * c;
                r = Math.Sqrt(r);

                ViewBag.ANS = ((-b + r) / (2 * a)).ToString() + "Or X = " +
                    ((-b - r) / (2 * a)).ToString();

            }
            return View();
        }

        public string testingDelete(int? id)
        {
            if (id != null)
                (new CCustomerFactory()).delete((int)id);
            return "刪除資料成功";
        }
        public string testingUpdate()
        {
            CCustomer x = new CCustomer()
            {
                fId = 5,
                fAddress = "Kaoshung",
                fEmail = "john@gmail.com",
                fName = "John",
                //fPassword = "1234",
                fPhone = "0966655532"
            };
            (new CCustomerFactory()).update(x);
            return "修改資料成功";
        }
        public string testingQuery()
        {
            return "客戶數：" + (new CCustomerFactory()).queryAll().Count.ToString();
        }
        public string testingInsert()
        {
            CCustomer x = new CCustomer()
            {
                //fAddress="Kaoshung",
                fEmail = "tom@gmail.com",
                fName = "Tom",
                fPassword = "1234",
                //fPhone="0944556632"
            };
            (new CCustomerFactory()).create(x);
            return "新增資料成功";
        }

        public ActionResult bindingById(int? id)
        {
            CCustomer x = null;
            if (id != null)
            {

                SqlConnection con = new SqlConnection();
                con.ConnectionString = @"Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
                con.Open();

                SqlCommand cmd = new SqlCommand(
                    "SELECT * FROM tCustomer WHERE fId=" + id.ToString(),
                    con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    x = new CCustomer()
                    {
                        fId = (int)reader["fId"],
                        fName = reader["fName"].ToString(),
                        fPhone = reader["fPhone"].ToString()
                    };
                }
                con.Close();
            }

            return View(x);
        }
        public ActionResult showById(int? id)
        {
            if (id != null)
            {

                SqlConnection con = new SqlConnection();
                con.ConnectionString = @"Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
                con.Open();

                SqlCommand cmd = new SqlCommand(
                    "SELECT * FROM tCustomer WHERE fId=" + id.ToString(),
                    con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    CCustomer x = new CCustomer()
                    {
                        fId = (int)reader["fId"],
                        fName = reader["fName"].ToString(),
                        fPhone = reader["fPhone"].ToString()
                    };
                    ViewBag.KK = x;
                }
                con.Close();
            }

            return View();
        }
        public string queryById(int? id)
        {
            if (id == null)
                return "沒有指定id";

            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
            con.Open();

            SqlCommand cmd = new SqlCommand(
                "SELECT * FROM tCustomer WHERE fId=" + id.ToString(),
                con);
            SqlDataReader reader = cmd.ExecuteReader();
            string s = "查無任何資料";
            if (reader.Read())
                s = reader["fName"].ToString() + "<br/>" + reader["fPhone"].ToString();
            con.Close();
            return s;
        }
        public string demoServer()
        {
            return "目前伺服器上的實體位置：" + Server.MapPath(".");
        }
        public string demoParameter(int? id)
        {

            if (id == null)
                return "沒有指定id";
            if (id == 0)
                return "XBox 加入購物車成功";
            else if (id == 1)
                return "PS5 加入購物車成功";
            else if (id == 2)
                return "Nintendo Switch 加入購物車成功";
            return "找不到該產品資料";
        }

        public string demoRequest()
        {
            string id = Request.QueryString["pid"];
            if (id == "0")
                return "XBox 加入購物車成功";
            else if (id == "1")
                return "PS5 加入購物車成功";
            else if (id == "2")
                return "Nintendo Switch 加入購物車成功";
            return "找不到該產品資料";
        }

        public string demoResponse()
        {
            Response.Clear();
            Response.ContentType = "application/octet-stream";
            Response.Filter.Close();
            Response.WriteFile(@"C:\QNote\8000.jpg");
            Response.End();
            return "";
        }

        public string sayHello()
        {
            return "Hello Asp.Net MVC";
        }
        [NonAction]

        public string lotto()
        {
            return (new CLottoGen()).getNumber();
        }
        // GET: A
        public ActionResult Index()
        {
            return View();
        }
    }
}
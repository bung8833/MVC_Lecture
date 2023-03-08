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
        // undone
        public ActionResult DemoForm()
        {
            ViewBag.ANS = "?";
            if (String.IsNullOrEmpty(Request.Form["txtA"]) == false)
            {
                double a = Convert.ToDouble(Request.Form["txtA"]);
                double b = Convert.ToDouble(Request.Form["txtB"]);
                ViewBag.ANS = (a + b).ToString();
            }
            return View();
        }


        public string TestingQuery()
        {
            //Customer customer = new CustomerFactory().QueryById(id);
            //return customer.fId + "<br/>" + customer.fName;

            string result = String.Empty;

            List<Customer> customers = new CustomerFactory().QueryAll();
            customers.ForEach(c => result += $"{c.fId}: {c.fName}<br/>");

            return result;
        }

        public string TestingInsert()
        {
            Customer customer = new Customer()
            {
                fName = "Tom",
                fPhone = "0988777666",
                fAddress = "KH",
                fEmail = "tom@gmail.com",
                fPassword = "1234",
            };

            new CustomerFactory().Create(customer);
            return "資料新增成功";
        }


        public ActionResult BindingById(int? id)
        {
            Customer customer = null;

            if (id == null)
            {
                return View(customer);
            }

            using (var sqlConn = new SqlConnection())
            {
                sqlConn.ConnectionString = "Data Source=P215-2203-NB01;Initial Catalog=dbDemo;Integrated Security=True";

                using (var cmd = sqlConn.CreateCommand())
                {
                    sqlConn.Open();
                    cmd.CommandText = "SELECT * FROM tCustomer WHERE fid=" + id.ToString();

                    SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                    if (reader.Read())
                    {
                        customer = new Customer()
                        {
                            fId = (int)reader["fId"],
                            fName = reader["fName"].ToString(),
                            fPhone = reader["fPhone"].ToString(),
                        };
                    }

                    return View(customer);
                }
            }
        }


        public ActionResult ShowById(int? id)
        {
            if (id == null)
            {
                return View();
            }

            using (var sqlConn = new SqlConnection())
            {
                sqlConn.ConnectionString = "Data Source=P215-2203-NB01;Initial Catalog=dbDemo;Integrated Security=True";

                using (var cmd = sqlConn.CreateCommand())
                {
                    sqlConn.Open();
                    cmd.CommandText = "SELECT * FROM tCustomer WHERE fid=" + id.ToString();

                    SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                    if (reader.Read())
                    {
                        Customer customer = new Customer()
                        {
                            fId = (int)reader["fId"],
                            fName = reader["fName"].ToString(),
                            fPhone = reader["fPhone"].ToString(),
                        };

                        ViewBag.Customer = customer;
                    }

                    return View();
                }
            }
        }


        public string QueryById(int? id)
        {
            if (id == null) return "沒有指定id";

            using (var sqlConn = new SqlConnection())
            {
                sqlConn.ConnectionString = "Data Source=P215-2203-NB01;Initial Catalog=dbDemo;Integrated Security=True";
                
                using (var cmd = sqlConn.CreateCommand())
                {
                    sqlConn.Open();
                    cmd.CommandText = "SELECT * FROM tCustomer WHERE fid=" + id.ToString();

                    string queryResult = "查無任何資料";

                    SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                    if (reader.Read())
                    {
                        queryResult = reader["fName"].ToString() + "<br/>" + reader["fPhone"].ToString();
                    }

                    return queryResult;
                }
            }
        }


        public string demoServer()
        {
            return "目前伺服器上的實體位置：" + Server.MapPath(".");
        }


        public string demoParameter(int? id)
        {
            if (id == null) return "沒有指定id";

            if (id == 0) return "XBox 加入購物車成功";
            else if (id == 1) return "PS5 加入購物車成功";
            return "找不到該產品資料";
        }


        public string demoRequest()
        {
            string id = Request.QueryString["pid"];

            if (id == "0")return "XBox 加入購物車成功";
            else if (id == "1")return "PS5 加入購物車成功";
            return "找不到該產品資料";
        }


        public string demoResponse()
        {
            Response.Clear();
            Response.ContentType = "application/octet-stream";
            Response.Filter.Close();
            Response.WriteFile(@"C:\Users\User\Desktop\powerbi\20230220_movie.gif");
            Response.End();
            return "";
        }


        public string Lottery()
        {
            string result = String.Empty;
            List<int> nums = GetRandomLotteryNumbers(1, 49, 6);
            
            nums.ForEach(num => result += num.ToString().PadLeft(2,'0') + " ");

            return result;
        }


        /// <summary>
        /// 從1~49之間，取得n個不重複的數字
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<int> GetRandomLotteryNumbers(int minNumber, int maxNumber, int count)
        {
            List<int> nums = new List<int>();

            Random rand = new Random(Guid.NewGuid().GetHashCode());

            while(count > 0)
            {
                int num = rand.Next(minNumber, maxNumber+1);

                if ( nums.Contains(num) == false )
                {
                    count--;
                    nums.Add(num);
                }
            }

            nums.Sort();

            return nums;
        }


        public string SayHello()
        {
            return $"Hello ASP.Net MVC \r\nline 2 \r\nline 3";
        }


        // GET: A
        public ActionResult Index()
        {
            return View();
        }
    }
}
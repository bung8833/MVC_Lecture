using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjMvcDemo.Controllers
{
    public class AController : Controller
    {
        public string demoServer()
        {
            return "目前伺服器上的實體位置：" + Server.MapPath(".");
        }


        public string demoParameter(int id)
        {
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
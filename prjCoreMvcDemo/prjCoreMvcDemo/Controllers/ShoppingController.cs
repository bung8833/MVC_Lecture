using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using prjCoreMvcDemo.Models;
using prjCoreMvcDemo.ViewModels;

namespace prjCoreMvcDemo.Controllers
{
    public class ShoppingController : SuperController
    {
        public IActionResult List()
        {
            dbDemoContext db = new dbDemoContext();

            var datas = db.TProducts.Select(p => p);

            List<ProductWrapper> products = new List<ProductWrapper>();

            foreach (var data in datas)
            {
                ProductWrapper product = new ProductWrapper();
                product.Product = data;
                products.Add(product);
            }

            return View(products);
        }


        public IActionResult CartView()
        {
            if (HttpContext.Session.Keys.Contains(SKDictionary.SK_PURCHASED_LIST))
            {
                return RedirectToAction("List");
            }

            string json = HttpContext.Session.GetString(SKDictionary.SK_PURCHASED_LIST);
            List<ShoppingCartItem> items =
                JsonSerializer.Deserialize<List<ShoppingCartItem>>(json);

            if (items == null)
            {
                return RedirectToAction("List");
            }

            return View(items);
        }


        public IActionResult AddToCart()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddToCart(AddToCartVM vm)
        {
            // undone

            dbDemoContext db = new dbDemoContext();

            TProduct prod = db.TProducts.FirstOrDefault(p => p.FId == vm.txtFId);
            if (prod == null)
            {
                return RedirectToAction("List");
            }

            List<ShoppingCartItem> cart = null;
            string json = String.Empty;

            if (HttpContext.Session.Keys.Contains(SKDictionary.SK_PURCHASED_LIST))
            {
                json = HttpContext.Session.GetString(SKDictionary.SK_PURCHASED_LIST);
                cart = JsonConvert.DeserializeObject<List<ShoppingCartItem>>(json);
            }
            else
            {
                cart = new List<ShoppingCartItem>();
            }

            ShoppingCartItem item = new ShoppingCartItem();
            // undone


            return View();
        }
    }
}

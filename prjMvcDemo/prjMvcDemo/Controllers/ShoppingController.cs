using prjMvcDemo.Models;
using prjMvcDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjMvcDemo.Controllers
{
    public class ShoppingController : Controller
    {
        public ActionResult CartView()
        {
            List<ShoppingCartItem> cart = Session["SK_CART_ITEM_LIST"] as List<ShoppingCartItem>;
            // todo

            return View();
        }

        // GET: Shopping
        public ActionResult List()
        {
            var products = from p in (new dbDemoEntities()).tProduct
                           select p;

            return View(products);
        }

        public ActionResult AddToCart(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("List");
            }

            ViewBag.FID = id;
            return View();
        }

        [HttpPost]
        public ActionResult AddToCart(AddToCartVM vm)
        {
            dbDemoEntities db = new dbDemoEntities();
            tProduct prod = db.tProduct.FirstOrDefault(p => p.fId == vm.txtFId);
            if (prod != null)
            {
                List<ShoppingCartItem> cartItems = Session["SK_CART_ITEM_LIST"] as List<ShoppingCartItem>;
                if (cartItems == null)
                {
                    cartItems = new List<ShoppingCartItem>();
                    Session["SK_CART_ITEM_LIST"] = cartItems;
                }
                ShoppingCartItem item = new ShoppingCartItem()
                {
                    productId = vm.txtFId,
                    count = vm.txtCount,
                    price = (decimal)prod.fPrice,
                };
                cartItems.Add(item);
            }
            return RedirectToAction("List");
        }
    }
}
using BookShop.Data;
using Microsoft.AspNetCore.Mvc;
using BookShop.Models;
using Newtonsoft.Json;

using Microsoft.AspNetCore.Http;

namespace BookShop.Controllers
{
    public class ProductController : Controller
    {
        private BookDbContext _db;
        private readonly IWebHostEnvironment webHostEnvironment;
        public ProductController(BookDbContext db)
        {
            this._db = db;
        }
        public ActionResult Index()
        {
            var _product = getAllProduct();
            ViewBag.product = _product;
            return View();
        }
        //GET ALL PRODUCT
        public List<Book> getAllProduct()
        {
            return _db.Products.ToList();
        }
        //GET DETAIL PRODUCT
        public Book getDetailProduct(int id)
        {
            var product = _db.Products.Find(id);
            return product;
        }
        //ADD CART

        public IActionResult addCart(int id)
        {

            var cart = HttpContext.Session.GetString("cart");//get key cart
            if (cart == null)
            {
                var product = getDetailProduct(id);
                List<Cart> listCart = new List<Cart>()
               {
                   new Cart
                   {
                       Product = product,
                       Quantity = 1
                   }
               };
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(listCart));

            }
            else
            {
                List<Cart> dataCart = JsonConvert.DeserializeObject<List<Cart>>(cart);
                bool check = true;
                for (int i = 0; i < dataCart.Count; i++)
                {
                    if (dataCart[i].Product.Id == id)
                    {
                        dataCart[i].Quantity++;
                        check = false;
                    }
                }
                if (check)
                {
                    dataCart.Add(new Cart
                    {
                        Product = getDetailProduct(id),
                        Quantity = 1
                    });
                }
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(dataCart));
                // var cart2 = HttpContext.Session.GetString("cart");//get key cart
                //  return Json(cart2);
            }

            return RedirectToAction("Index", "Customer");

        }

        public IActionResult ListCart()
        {
            var cart = HttpContext.Session.GetString("cart");//get key cart
            if (cart != null)
            {
                List<Cart> dataCart = JsonConvert.DeserializeObject<List<Cart>>(cart);
                if (dataCart.Count > 0)
                {
                    ViewBag.carts = dataCart;
                    return View();
                }
            }
        
            return RedirectToAction("Index", "Home");
    }

        //[HttpPost]
        //public IActionResult updateCart(int id, int quantity)
        //{
        //    var cart = HttpContext.Session.GetString("cart");
        //    if (cart != null)
        //    {
        //        List<Cart> dataCart = JsonConvert.DeserializeObject<List<Cart>>(cart);
        //        if (quantity > 0)
        //        {
        //            for (int i = 0; i < dataCart.Count; i++)
        //            {
        //                if (dataCart[i].Product.Id == id)
        //                {
        //                    dataCart[i].Quantity = quantity;
        //                }
        //            }


        //            HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(dataCart));
        //        }
        //        var cart2 = HttpContext.Session.GetString("cart");
        //        return Ok(quantity);
        //    }
        //    return BadRequest();

        //}
        [HttpPost]
        public IActionResult updateCart(int id, int quantity)
        {
            var session = HttpContext.Session.GetString("cart");
            List<Cart> dataCart = new List<Cart>();
            if (session == null)
                dataCart = JsonConvert.DeserializeObject<List<Cart>>(session);
            foreach (var item in dataCart)
            {
                if (item.Product.Id == id)
                {
                    if (quantity == 0)
                    {
                        dataCart.Remove(item);
                    }
                    else
                    {
                        item.Quantity = quantity;
                    }

                }
            }
            var cart2 = HttpContext.Session.GetString("cart");
            return Ok(dataCart);

        }


        public IActionResult deleteCart(int id)
        {
            var cart = HttpContext.Session.GetString("cart");
            if (cart != null)
            {
                List<Cart> dataCart = JsonConvert.DeserializeObject<List<Cart>>(cart);

                for (int i = 0; i < dataCart.Count; i++)
                {
                    if (dataCart[i].Product.Id == id)
                    {
                        dataCart.RemoveAt(i);
                    }
                }
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(dataCart));
                return RedirectToAction(nameof(ListCart));
            }
            return RedirectToAction(nameof(Index));
        }


        public IActionResult buyCart(int id)
        {
            var cart = HttpContext.Session.GetString("cart");
            if (cart != null)
            {
                List<Cart> dataCart = JsonConvert.DeserializeObject<List<Cart>>(cart);


                //for (int i = 0; i < dataCart.Count; i++)
                //{
                //    dataCart.RemoveAt(i);
                //}
                dataCart.Clear();
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(dataCart));
                return RedirectToAction(nameof(ListCart));
            }

            return RedirectToAction(nameof(Index));
        }




    }
}

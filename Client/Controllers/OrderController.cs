using Client.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Globalization;

namespace Client.Controllers
{
    public class OrderController : Controller
    {
        private ePRJContext _context;

        public OrderController(ePRJContext context)
        {
            _context = context;
        }
        List<Cart> GetCartItems()
        {

            var session = HttpContext.Session;
            string jsoncart = session.GetString("Cart");
            if (jsoncart != null)
            {
                return JsonConvert.DeserializeObject<List<Cart>>(jsoncart);
            }
            return new List<Cart>();
        }
        void ClearCart()
        {
            var session = HttpContext.Session;
            session.Remove("Cart");
        }

        // Lưu Cart (Danh sách CartItem) vào session
        void SaveCartSession(List<Cart> ls)
        {
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(ls);
            session.SetString("Cart", jsoncart);
        }
        public IActionResult Index()
        {
            ViewBag.Cart = HttpContext.Session.GetString("Cart");
            return View();
        }
        [Route("addcart/{productid:int}")]
        public IActionResult AddToCart([FromRoute] int productid)
        {

            var product = _context.Products
                            .Where(p => p.Id == productid)
                            .FirstOrDefault();
            if (product == null)
            {
                return NotFound("Not found product");
            }

            // Xử lý đưa vào Cart ...
            var carts = GetCartItems();

            var cartitem = carts.Find(p => p.product.Id == productid);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cartitem.quantity++;
            }
            else
            {
                //  Thêm mới
                carts.Add(new Cart() { quantity = 1, product = product });
            }

            string jsoncart = JsonConvert.SerializeObject(carts);
            HttpContext.Session.SetString("Cart", jsoncart);

            SaveCartSession(carts);

            return RedirectToAction("Index");
        }

        /// xóa item trong cart
        [Route("/removecart/{productid:int}", Name = "removecart")]
        public IActionResult RemoveCart([FromRoute] int productid)
        {
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.product.Id == productid);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cart.Remove(cartitem);
            }

            SaveCartSession(cart);
            return RedirectToAction("Index");
        }

        /// Cập nhật
        [Route("/updatecart", Name = "updatecart")]
        [HttpPost]
        public IActionResult UpdateCart([FromForm] int productid, [FromForm] int quantity)
        {
            // Cập nhật Cart thay đổi số lượng quantity ...
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.product.Id == productid);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cartitem.quantity = quantity;
            }
            SaveCartSession(cart);
            // Trả về mã thành công (không có nội dung gì - chỉ để Ajax gọi)
            return Ok();
        }

        [Route("/checkout")]
        public IActionResult CheckOut() { return View(); }

        [HttpPost]
        public async Task<IActionResult> Buy(Order data)
        {
            var cusId = HttpContext.Session.GetInt32("CustomerId");

            var cart = GetCartItems();
            Customer cus = _context.Customers.SingleOrDefault(e => e.Id == cusId);
            if (!ModelState.IsValid)
            {
                foreach (var item in cart)
                {
                    data.CustomerId = cus.Id;
                    data.Address = cus.UserAddress;
                    data.OrderDate = DateTime.Now;
                    data.PaymentTerm = 1;
                    data.Phone = cus.PhoneNumber;
                    data.TotalPrice = (int)HttpContext.Session.GetInt32("TotalPrice");
                    data.Quantity = (int)HttpContext.Session.GetInt32("TotalQuantity");
                    data.ProductId = item.product.Id;
                    _context.Orders!.Add(data);
                    

                }
                await _context.SaveChangesAsync();
                ClearCart();
                TempData["message2"] = "Registration successful!";
                return RedirectToAction("Index","Home");
            }
            return RedirectToAction("CheckOut");
        }
    }
}

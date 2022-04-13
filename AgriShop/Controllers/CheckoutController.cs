using AnBinhMarket.Data;
using AnBinhMarket.Data.Entities;
using AnBinhMarket.Extensions;
using AnBinhMarket.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AnBinhMarket.Controllers
{
    public class CheckoutController : Controller
    {
        private const string Cartsession = "Cartsession";
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public CheckoutController(SignInManager<ApplicationUser> signInManager, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.signInManager = signInManager;
            _context = context;
            _userManager = userManager;
        }


        public IActionResult Index()
        {

            var cart = HttpContext.Session.Get<List<CartItem>>("Cartsession");
            if (signInManager.IsSignedIn(User))
            {
                return View(cart);

            }
            else
            {
                return RedirectToAction("Login", "Profile");
            }
        }


        public IActionResult Additem(Guid productId, int quantity)
        {
            var product = _context.SanPhams.Find(productId);
            var cart = HttpContext.Session.Get<List<CartItem>>("Cartsession");

            if (product?.SoLuong == 0 || product?.SoLuong < 0)
            {
                return RedirectToAction("ErrorOrder", "Checkout");
            }
            else
            {
                if (cart != null)
                {
                    var list = (List<CartItem>)cart;
                    if (list.Any(x => x.SanPham.Id == productId))
                    {
                        foreach (var item in list)
                        {
                            if (item.SanPham.Id == productId)
                            {
                                item.Soluong += quantity;
                            }
                        }
                    }
                    else
                    {
                        //tạo mới đối tượng cart item;
                        var item = new CartItem();
                        item.SanPham = product;
                        item.Soluong = quantity;
                        list.Add(item);


                    }
                    HttpContext.Session.Set<List<CartItem>>("Cartsession", list);
                }
                else
                {
                    var item = new CartItem();
                    item.SanPham = product;
                    item.Soluong = quantity;
                    var list = new List<CartItem>();
                    list.Add(item);
                    //gắn với session
                    HttpContext.Session.Set<List<CartItem>>("Cartsession", list);

                }
            }

            return RedirectToAction("Index", "Checkout");
        }


        public JsonResult Update(string cartModel)
        {

            var jsonCart = JsonConvert.DeserializeObject<List<CartItem>>(cartModel);
            var sessionCart = HttpContext.Session.Get<List<CartItem>>("Cartsession");
            foreach (var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.SanPham.Id == item.SanPham.Id);
                if (jsonItem != null)
                {
                    item.Soluong = jsonItem.Soluong;
                }
            }
            HttpContext.Session.Set<List<CartItem>>("Cartsession", sessionCart);
            return Json(new
            {
                status = true
            });
        }

        [HttpGet]
        public ActionResult Delete(Guid id)
        {
            var sessionCart = HttpContext.Session.Get<List<CartItem>>("Cartsession");
            sessionCart.RemoveAll(x => x.SanPham.Id == id);
            HttpContext.Session.Set<List<CartItem>>("Cartsession", sessionCart);

            return RedirectToAction("Index", "Checkout");
        }
        public ActionResult DeleteAll()
        {
            HttpContext.Session.Remove("Cartsession");
            return RedirectToAction("Index", "Checkout");
        }
        [HttpGet]
        public ActionResult Order(string sumTotal)
        {
            ViewBag.sumTotal = sumTotal;
            var cart = HttpContext.Session.Get<List<CartItem>>("Cartsession");
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }

        [HttpPost]
        public ActionResult Order(string DcNhanHang, string GhiChu)
        {
            List<CartItem> ListSP = new List<CartItem>();
            ListSP = HttpContext.Session.Get<List<CartItem>>("Cartsession");

            if (signInManager.IsSignedIn(User))
            {
                GioHang giohang = new GioHang()
                {
                    NgayCapNhat = DateTime.Now,
                     NgayTao = DateTime.Now
                };
                giohang.TenTaiKhoan =Guid.Parse(_userManager.GetUserId(User));
                _context.GioHangs.Add(giohang);
                _context.SaveChanges();
                var generatedId = giohang.Id;
                foreach (var item in ListSP)
                {
                    ChiTietGioHang ct = new ChiTietGioHang();
                    ct.MaGioHang = generatedId;
                    ct.MaSP = item.SanPham.Id;
                    ct.SoLuong = item.Soluong;
                    ct.Gia = item.SanPham.Gia;
                    _context.ChiTietGioHangs.Add(ct);
                    _context.SaveChanges();
                    var product = _context.SanPhams.SingleOrDefault(x => x.Id == item.SanPham.Id);
                    if(product != null)
                    {
                        product.SoLuong -= item.Soluong;

                    }
                }
                HoaDon hd = new HoaDon();
                hd.NgayTao = DateTime.Now;
                hd.TrangThai = "Chờ xác nhận";
                hd.PhiShip = 15000;
                hd.ChuY = GhiChu;
                hd.MaGioHang = generatedId;
                if (DcNhanHang != "")
                {
                    hd.DiaChi = DcNhanHang;
                }
                else
                {
                    var currentUser = _context.Users.FirstOrDefault(x => x.Id == Guid.Parse(_userManager.GetUserId(User)));
                    if (currentUser != null)
                    {
                        hd.DiaChi = currentUser.DiaChi;
                    }
                }

                _context.HoaDons.Add(hd);
                _context.SaveChanges();

            }
            else
            {
                return RedirectToAction("Login", "Profile");
            }
            HttpContext.Session.Remove("Cartsession");
            return Redirect("/");
        }
    }
}

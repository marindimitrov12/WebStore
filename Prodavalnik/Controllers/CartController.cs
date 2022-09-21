using Microsoft.AspNetCore.Mvc;
using Prodavalnik.Core.IConfiguration;
using Prodavalnik.Infrastructure;
using Prodavalnik.Models;
using Prodavalnik.ViewModels;

namespace Prodavalnik.Controllers
{
    public class CartController:Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public CartController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            List<CardItem> cart = HttpContext.Session.GetJson<List<CardItem>>("Cart")??new List<CardItem>();
           CartViewModel CardVM = new()
            {
                CardItems = cart,
                GrandTotal = cart.Sum(x => x.Quantity * x.Price)
            };
            return View(CardVM);
        }

        public async Task<IActionResult> Add(int id)
        {
            var product = await _unitOfWork.Product.GetById(id);
            List<CardItem> cart = HttpContext.Session.GetJson<List<CardItem>>("Cart") ?? new List<CardItem>();
            CardItem cardItem = cart.Where(p=>p.ProductId==id).FirstOrDefault();
            if (cardItem == null)
            {
                cart.Add(new CardItem(product));
            }
            else
            {
                cardItem.Quantity += 1;
            }
            HttpContext.Session.SetJson("Cart",cart);
            TempData["Success"] = "The product has been added!";
            
            return Redirect(HttpContext.Request.Headers["Referer"].ToString());
        }
        
               public async Task<IActionResult> Decrease(int id)
        {
            List<CardItem> cart = HttpContext.Session.GetJson<List<CardItem>>("Cart") ?? new List<CardItem>();
            CardItem cardItem = cart.Where(p => p.ProductId == id).FirstOrDefault();
            if (cardItem.Quantity > 1)
            {
                --cardItem.Quantity;
            }
            else
            {
                cart.RemoveAll(x=>x.ProductId==id);
            }
            if (cart.Count==0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart",cart);
            }
               
            
            HttpContext.Session.SetJson("Cart", cart);
            TempData["Success"] = "The product has been added!";

            return Redirect(HttpContext.Request.Headers["Referer"].ToString());
        }
        public async Task<IActionResult> Remove(int id)
        {
            var product = await _unitOfWork.Product.GetById(id);
            List<CardItem> cart = HttpContext.Session.GetJson<List<CardItem>>("Cart") ?? new List<CardItem>();
            CardItem cardItem = cart.Where(p => p.ProductId == id).FirstOrDefault();
            if (cardItem != null)
            {
                cart.Remove(cardItem);
            } 
           
            HttpContext.Session.Remove("Cart");
            TempData["Success"] = "The product has been deleted frome cart!";

            return Redirect(HttpContext.Request.Headers["Referer"].ToString());
        }
        public async Task<IActionResult> Clear()
        {
            HttpContext.Session.Remove("Cart");
           return Redirect(HttpContext.Request.Headers["Referer"].ToString());
        }
      
        public async Task<IActionResult> CheckOut()
        {
            var model = HttpContext.Session.GetJson<List<CardItem>>("Cart");
            foreach (var item in HttpContext.Session.GetJson<List<CardItem>>("Cart"))
            {
                var Order = new Order
                {
                    Quantity = model.Count(),
                    Product = new Product
                    {
                        Name=item.ProductName,
                        Price = item.Price,
                        Description="",
                        Img=item.Image,
                    },
                    
                };
            await _unitOfWork.Order.Add(Order);
            }
            await _unitOfWork.CompliteAsync();
          
            HttpContext.Session.Remove("Cart");
            return Redirect(HttpContext.Request.Headers["Referer"].ToString());
        }


    }
}

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


    }
}

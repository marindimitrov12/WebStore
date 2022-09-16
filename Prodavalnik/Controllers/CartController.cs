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

    }
}

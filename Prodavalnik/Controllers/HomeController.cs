using Microsoft.AspNetCore.Mvc;
using Prodavalnik.Core.IConfiguration;
using Prodavalnik.Models;
using System.Diagnostics;

namespace Prodavalnik.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
      
        public HomeController(ILogger<HomeController> logger,IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index(int p = 1)
        {
           
            int pageSize = 3;
            ViewBag.PageNumber = p;
            ViewBag.PageRange= pageSize;
            ViewBag.TotalPages=(int)Math.Ceiling((double)await _unitOfWork.Product.Count()/3);
            var result = await _unitOfWork.Product.Skip((p - 1) * pageSize);
            
            return View(result.Take(pageSize).ToList());
            
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
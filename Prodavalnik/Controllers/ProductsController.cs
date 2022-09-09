using Microsoft.AspNetCore.Mvc;
using Prodavalnik.Core.IConfiguration;
using Prodavalnik.ViewModels;

namespace Prodavalnik.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        public ProductsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;

        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>  Create(ProductViewModel model)
        {
            var fileBytes=new byte[model.Img.Length];
            using (var ms = new MemoryStream())
            {
                model.Img.CopyTo(ms);
                 fileBytes = ms.ToArray();
                
                
            }


           await unitOfWork.Product.Add(new Models.Product { Name=model.Name,
            Price=(decimal)model.Price,Img= fileBytes,Category=model.Category,
            AddedOn=DateTime.Now,Description=model.Description
            });
            await unitOfWork.CompliteAsync();



            return Redirect("/");
        }
    }
}

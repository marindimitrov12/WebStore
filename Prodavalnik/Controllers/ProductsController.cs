using Microsoft.AspNetCore.Mvc;
using Prodavalnik.Core.IConfiguration;
using Prodavalnik.ViewModels;
using System.Text;

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
        [HttpGet]
        public async Task<IActionResult> Edit(ProductViewModel model)
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Edit( int id,ProductViewModel model)
        {
            var item = await unitOfWork.Product.GetById(id);
          
           // item.Img = fileBytes;
                item.Price = model.Price;
                item.Category = model.Category;
                item.AddedOn = DateTime.Now;
                item.Description = model.Description;
                item.Name = model.Name;
            
            unitOfWork.CompliteAsync();
            return Redirect("/");
        }

    }
}

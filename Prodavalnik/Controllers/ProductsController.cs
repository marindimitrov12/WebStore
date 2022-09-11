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
        public async Task<IActionResult> Edit(int id)
        {
            var item = await unitOfWork.Product.GetById(id);
            return View(new ProductViewModel { Id=item.Id,Name=item.Name,Price=(int)item.Price,
                Description=item.Description,Img=null,Category=item.Category});
        }
        [HttpPost]
        public async Task<IActionResult> Edit( ProductViewModel model)  
        {
            
            var img=model.Img;
          
           
            var fileBytes = new byte[model.Img.Length];
            using (var ms = new MemoryStream())
            {
                img.CopyTo(ms);
                fileBytes = ms.ToArray();
            }
            await unitOfWork.Product.Upsert(new Models.Product
            {
                Id =model.Id,
                Description = model.Description,
                Price = model.Price,
                Img= fileBytes,
                Category=model.Category,
                Name=model.Name,
                AddedOn=DateTime.Now
            });
           await unitOfWork.CompliteAsync();
            return Redirect("/");
        }

    }
}

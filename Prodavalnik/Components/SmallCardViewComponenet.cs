using Microsoft.AspNetCore.Mvc;
using Prodavalnik.Infrastructure;
using Prodavalnik.Models;
using Prodavalnik.ViewModels;

namespace Prodavalnik.Components
{
    public class SmallCardViewComponenet:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            List<CardItem> cart = HttpContext.Session.GetJson<List<CardItem>>("Cart");
            SmallCardViewModel smallCardVM;
            if (cart == null || cart.Count == 0)
            {
                smallCardVM = null;
            }
            else
            {
                smallCardVM = new()
                {
                    NumberOfItems = cart.Sum(x => x.Quantity),
                    TotalAmount=cart.Sum(x => x.Quantity*x.Price)
                };
            }
            return View(smallCardVM);
        }
    }
}

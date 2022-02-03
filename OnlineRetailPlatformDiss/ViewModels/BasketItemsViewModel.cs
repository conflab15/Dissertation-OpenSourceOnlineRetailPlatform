using OnlineRetailPlatformDiss.Models;
using System;
using System.Linq;

namespace OnlineRetailPlatformDiss.ViewModels
{
    public class BasketItemsViewModel
    {
        //ViewModel for the Items within the Basket, which will be returned to a Razor Component
        public List<BasketsModel>? BasketItems { get; set; } //List of Products in the Users Basket

        public decimal BasketPrice
        {
            get
            {
                return BasketItems.Sum(i => i.BasketQuantity * i.BasketProduct.PromotionalPrice); //Total Price of the Basket...
            }
        }
    }
}

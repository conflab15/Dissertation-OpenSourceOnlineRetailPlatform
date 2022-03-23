using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnlineRetailPlatformDiss.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineRetailPlatformDiss.Models
{
    public class ShoppingBasketModel
    {
        private readonly ApplicationDbContext _context;

        public ShoppingBasketModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public string BasketId { get; set; }

        public List<ShoppingBasketProductModel> ShoppingBasketItems { get; set; }

        public static ShoppingBasketModel GetBasket(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            var context = services.GetService<ApplicationDbContext>();
            string basketId = session.GetString("BasketId") ?? Guid.NewGuid().ToString();

            session.SetString("BasketId", basketId);

            return new ShoppingBasketModel(context) { BasketId = basketId };
        }

        public async void AddToBasket(ProductModel Product, int qty)
        {
            var shoppingBasketItem = _context.ShoppingBasketProducts.SingleOrDefault(
                s => s.Product.ProductID == Product.ProductID && s.ShoppingBasketId == BasketId);

            if (shoppingBasketItem == null)
            {
                shoppingBasketItem = new ShoppingBasketProductModel
                {
                    ShoppingBasketId = BasketId,
                    Product = Product,
                    Quantity = qty
                };

                _context.ShoppingBasketProducts.Add(shoppingBasketItem);
            }
            else
            {
                shoppingBasketItem.Quantity++;
            }
            await _context.SaveChangesAsync();
        }

        public int RemoveFromBasket(ProductModel Product)
        {
            var shoppingBasketItem = _context.ShoppingBasketProducts.SingleOrDefault(
                s => s.Product.ProductID == Product.ProductID && s.ShoppingBasketId == BasketId);
            var localQuantity = 0;

            if (shoppingBasketItem != null)
            {
                if(shoppingBasketItem.Quantity > 1)
                {
                    shoppingBasketItem.Quantity--;
                    localQuantity = shoppingBasketItem.Quantity;
                }
                else
                {
                    _context.ShoppingBasketProducts.Remove(shoppingBasketItem);
                } 
                    
            }

            _context.SaveChanges();
            return localQuantity;
        }

        public List<ShoppingBasketProductModel> GetShoppingBasketProducts()
        {
            return ShoppingBasketItems ??
                (ShoppingBasketItems = _context.ShoppingBasketProducts.Where(c => c.ShoppingBasketId == BasketId)
                .Include(s => s.Product)
                .ToList());
        }

        public async void ClearBasket()
        {
            var basketProducts = _context
                .ShoppingBasketProducts
                .Where(basket => basket.ShoppingBasketId == BasketId);

            _context.ShoppingBasketProducts.RemoveRange(basketProducts);

            await _context.SaveChangesAsync();
        }

        public decimal GetShoppingBasketTotal()
        {
            var total = _context.ShoppingBasketProducts.Where(b => b.ShoppingBasketId == BasketId)
                .Select(p => p.Product.PromotionalPrice * p.Quantity).Sum();
            return total;
        }
    }
}

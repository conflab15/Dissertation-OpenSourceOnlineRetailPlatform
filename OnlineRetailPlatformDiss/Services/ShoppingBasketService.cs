using Microsoft.EntityFrameworkCore;
using OnlineRetailPlatformDiss.Data;
using OnlineRetailPlatformDiss.Models;

namespace OnlineRetailPlatformDiss.Services
{
    public class ShoppingBasketService
    {
        ApplicationDbContext context = new();
        public static string BasketId { get; set; } = Guid.NewGuid().ToString();

        public static async Task<ShoppingBasketService> GetBasket()
        {
            var basket = new ShoppingBasketService();
            return await Task.FromResult(basket);
        }

        //The Shopping Basket
        public async Task<ShoppingBasketVM> shoppingBasket()
        {
            var basket = await ShoppingBasketService.GetBasket();

            //Implementing ViewModel
            var vm = new ShoppingBasketVM
            {
                BasketItems = await basket.GetItems()
            };
            return vm; 
        }

        //Add Items to Cart...
        public async Task<ShoppingBasketRemoveVM> AddToBasket(Guid id)
        {
            //Find the product to add from the db...
            var productToAdd = await context.Products.SingleAsync(p => p.ProductID == id);

            //Add it to the Shopping Basket
            var basket = await ShoppingBasketService.GetBasket();
            int count = await basket.AddToBasket(productToAdd);

            //ViewModel to present a message to the user
            var result = new ShoppingBasketRemoveVM
            {
                Message = $"{productToAdd.ProductName} has been added to your basket!",
                BasketTotal = await basket.GetTotal(),
                BasketCount = await basket.GetCount(),
                ProductCount = count,
                DeleteId = id
            };
            return result;
        }

        //Remove an item from the basket...
        public async Task<ShoppingBasketRemoveVM> RemoveFromBasket(Guid id)
        {
            var basket = await ShoppingBasketService.GetBasket();

            //Get Item name to display within the message...
            string productName = context.Products.Single(p => p.ProductID == id).ProductName;

            //Remove the item
            int productCount = await basket.RemoveFromBasket(id);

            var result = new ShoppingBasketRemoveVM
            {
                Message = $"One of {productName} has been removed from your basket!",
                BasketTotal = await basket.GetTotal(),
                BasketCount = await basket.GetCount(),
                ProductCount = count,
                DeleteId = id
            };
            return results;
        }

        public async Task<int> BasketCount()
        {
            var basket = await ShoppingBasketService.GetBasket();
            return await basket.GetCount();
        }

        public async Task<int> AddToCart(ProductModel product)
        {
            var basketItem = await context.Baskets.SingleOrDefaultAsync(
                b => b.BasketId == BasketId
                && b.ProductId == product.ProductID);

            if (basketItem == null)
            {
                //Create a Basket Item if it doesn't exist... If it does exist, increase the quantity by 1
                basketItem = new Baskets
                {
                    ProductId = product.ProductID,
                    BasketId = BasketId,
                    Count = 1,
                    DateCreated = DateTime.Now
                };
                context.Baskets.Add(basketItem);
            }
            else
            {
                //Increasing the quantity by 1
                basketItem.Count = 1;
            }
            //Save Changes to the db
            await context.SaveChangesAsync();

            return basketItem.Count;
        }

        //Remove from Basket
        public async Task<int> RemoveFromBasketItem(Guid id)
        {
            //Get the Basket
            var basketItem = await context.Baskets.SingleAsync(
                b => b.BasketId == BasketId
                && basketItem.ProductId == id);

            int prodCount = 0;

            if (basketItem != null)
            {
                if (basketItem.Count > 1)
                {
                    basketItem.Count--;
                    prodCount = basketItem.Count;
                }
                else
                {
                    context.Baskets?.Remove(basketItem);
                }
                await context.SaveChangesAsync();
            }
            return prodCount;
        }

        //Empty everything from basket...
        public async Task EmptyCart()
        {
            var basketItems = context.Baskets?.Where(
                b => b.BasketId == BasketId);

            foreach (var item in basketItems)
            {
                context.Baskets?.Remove(item);
            }

            //Save The Changes
            await context.SaveChangesAsync();
        }

        //Tasks
        //Get Cart Items

        //GetCount

        //Get Total

        //Create an Order





    }
}

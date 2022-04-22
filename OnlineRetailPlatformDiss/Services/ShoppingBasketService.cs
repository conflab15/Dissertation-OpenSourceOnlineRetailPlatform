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
                BasketItems = await basket.getProducts()
            };
            return vm; 
        }

        //Add Items to Cart...
        public async Task<ShoppingBasketRemoveVM> AddToBasket(Guid id)
        {
            //Find the product to add from the db...
            ProductModel? productToAdd = await context.Products.SingleAsync(p => p.ProductID == id);

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
            string? productName = context.Products?.Single(p => p.ProductID == id).ProductName;

            //Remove the item
            int productCount = await basket.RemoveFromBasketItem(id);

            var result = new ShoppingBasketRemoveVM
            {
                Message = $"One of {productName} has been removed from your basket!",
                BasketTotal = await basket.GetTotal(),
                BasketCount = await basket.GetCount(),
                ProductCount = productCount,
                DeleteId = id
            };
            return result;
        }

        private bool IsNotNull(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> BasketCount()
        {
            var basket = await ShoppingBasketService.GetBasket();
            return await basket.GetCount();
        }

        public async Task<int> AddToBasket(ProductModel product)
        {
            Baskets? basketItem = await context.Baskets.SingleOrDefaultAsync(
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
            Baskets basketItem = await context.Baskets.SingleAsync(
                b => b.BasketId == BasketId
                     && b.ProductId == id);

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
            IQueryable<Baskets>? basketItems = context.Baskets?.Where(
                b => b.BasketId == BasketId);

            if (basketItems != null)
            {
                foreach (var item in basketItems)
                {
                    context.Baskets?.Remove(item);
                }
            }
            //Save The Changes
            await context.SaveChangesAsync();
        }

        //Tasks
        //Get Cart Items
        public async Task<List<Baskets>> GetBasketItems()
        {
            return await context.Baskets.Include("Product").Where(
                b => b.BasketId == BasketId).ToListAsync();
        }

        public async Task<List<ProductModel>> getProducts()
        {
            return await context.Products.ToListAsync();
        }

        //GetCount
        public async Task<int> GetCount()
        {
            //Get the total of each product in the basket and add them together
            int? productCount = await (from basket in context.Baskets
                                       where basket.BasketId == BasketId
                                       select (int?)basket.Count).SumAsync();
            //If all the entries are null, then return 0...
            return productCount ?? 0;
        }

        //Get Total
        public async Task<decimal> GetTotal()
        {
            //Multiply the product price by the total within the Basket
            //Add them all together...
            decimal? total = await (from basket in context.Baskets
                                    where basket.BasketId == BasketId
                                    select (int?)basket.Count *
                                    basket.Product.ProductPrice).SumAsync();
            return total ?? decimal.Zero;
        }

        //Create an Order
        //To be added once shopping basket works as intended...




    }
}

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

        //The Shopping Basket to return details to the view via the viewmodel
        public async Task<ShoppingBasketVM> shoppingBasket()
        {
            var basket = await GetBasket();

            //Implementing ViewModel
            var vm = new ShoppingBasketVM
            {
                BasketItems = await basket.GetBasketItems(),
                BasketTotal = await basket.GetTotal()
            };
            return vm;
        }

        //Add Items to the Basket...
        public async Task<ShoppingBasketRemoveVM> AddToBasket(Guid id, int quantity)
        {
            //Find the product to add from the db...
            ProductModel? productToAdd = await context.Products.SingleAsync(p => p.ProductID == id);
            int finalQuantity = 0;
            if (quantity > productToAdd.StockLevel)
            {
                finalQuantity = productToAdd.StockLevel; //If the Quantity To Add exceeds the stock level, change it...
            }
            else
            {
                finalQuantity = quantity; //Assign the Quantity To Add...
            }


            //Add it to the Shopping Basket
            var basket = await GetBasket();
            int count = await basket.AddToBasket(productToAdd, quantity);

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
        public async Task<ShoppingBasketRemoveVM> RemoveFromBasket(Guid id, int quantity)
        {
            var basket = await GetBasket();

            //Get Item name to display within the message...
            ProductModel? product = context.Products?.Single(p => p.ProductID == id);

            //Remove the item
            int productCount = await basket.RemoveFromBasketItem(id, quantity);

            var result = new ShoppingBasketRemoveVM
            {
                Message = $"{quantity} of {product?.ProductName} has been removed from your basket!",
                BasketTotal = await basket.GetTotal(),
                BasketCount = await basket.GetCount(),
                ProductCount = productCount,
                DeleteId = id
            };
            return result;
        }

        //private bool IsNotNull(Guid id)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<int> BasketCount()
        {
            var basket = await GetBasket();
            return await basket.GetCount();
        }

        public async Task<int> AddToBasket(ProductModel product, int quantity)
        {
            BasketModel? basketItem = null; //Allows the function to run if it cannot find a pre-existing basket...

            //Used for testing issues with the db...
            //context.Baskets.Add(new BasketModel
            //{
            //    ProductId = product.ProductID,
            //    BasketId = BasketId,
            //    Count = 1,
            //    DateCreated = DateTime.Now
            //});

            basketItem = context.Baskets.Where(b => b.BasketId == BasketId && b.ProductId == product.ProductID).FirstOrDefault();

            //Checking if the quantity to add exceeds the stock level...
            int finalQuantity = 0;
            if (quantity > product.StockLevel)
            {
                finalQuantity = product.StockLevel; //If the Quantity To Add exceeds the stock level, change it...
            }
            else
            {
                finalQuantity = quantity; //Assign the Quantity To Add...
            }

            if (basketItem == null)
            {
                //Create a Basket Item if it doesn't exist... If it does exist, increase the quantity by 1
                basketItem = new BasketModel
                {
                    ProductId = product.ProductID,
                    BasketId = BasketId,
                    Count = finalQuantity,
                    DateCreated = DateTime.Now
                };
                context.Baskets.Add(basketItem);
            }
            else
            {
                //Increasing the quantity by 1
                //Error here, was originally assigning the count to 1 instead of increasing by 1...
                basketItem.Count += finalQuantity;
            }

            //Modify the Stock Level of the product...
            product.StockLevel -= quantity;
            context.Products?.Update(product);
            await context.SaveChangesAsync();

            //Save Changes to the db
            await context.SaveChangesAsync();

            return basketItem.Count;
        }

        //Remove from Basket
        public async Task<int> RemoveFromBasketItem(Guid id, int quantity)
        {
            //Get the Basket
            BasketModel basketItem = await context.Baskets.SingleAsync(
                b => b.BasketId == BasketId
                     && b.ProductId == id);

            ProductModel? product = await context.Products.SingleAsync(
                p => p.ProductID == basketItem.ProductId);

            int prodCount = 0;

            if (basketItem != null)
            {
                if (basketItem.Count > 1)
                {
                    basketItem.Count -= quantity;
                    prodCount = basketItem.Count;

                    //Add the Stock back to the item...
                    product.StockLevel += quantity;
                    context.Products?.Update(product);
                }
                else
                {
                    product.StockLevel += 1;
                    context.Products?.Update(product);
                    context.Baskets?.Remove(basketItem);
                }
                await context.SaveChangesAsync();
            }
            return prodCount;
        }

        //Empty everything from basket...
        public async Task EmptyCart()
        {
            IQueryable<BasketModel>? basketItems = context.Baskets?.Where(
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
        public async Task<List<BasketModel>> GetBasketItems()
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
                                    select (int?)basket.Count
                                           * basket.Product.ProductPrice).SumAsync();

            return total ?? decimal.Zero;
        }

        //Create an Order
        public async Task<OrderModel> CreateOrder(OrderModel order)
        {
            decimal total = 0;
            order.OrderLines = new List<OrderLineModel>();

            var basketItems = await GetBasketItems();

            //For each item in the cart, add it to the order...
            foreach (var product in basketItems)
            {
                var orderline = new OrderLineModel
                {
                    ProductId = product.ProductId,
                    OrderId = order.OrderId,
                    ProductPrice = product.Product.ProductPrice,
                    ProductQuantity = product.Count,
                    Status = "Order Accepted" //Setting Default Order Status
                };
                //Set the Order Total!
                total += (product.Count * product.Product.ProductPrice);
                //Add the OrderLine to the Order...
                order.OrderLines.Add(orderline);
                //Add to the db...
                context.OrderLines?.Add(orderline);
            }
            order.OrderTotal = total;
            order.OrderStatus = "Order Placed";

            context.Orders?.Add(order); //Adding the order, which creates a Guid...

            //Save the changes to the db...
            await context.SaveChangesAsync();

            //Empty the basket
            await EmptyCart();

            //When the order is returned, we can use the ID to show the success of the order to the user...
            return order;
        }



    }
}

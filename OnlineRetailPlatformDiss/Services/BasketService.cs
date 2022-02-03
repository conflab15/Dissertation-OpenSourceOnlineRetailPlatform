using Microsoft.EntityFrameworkCore;
using OnlineRetailPlatformDiss.Data;
using System;
using System.Linq;
using OnlineRetailPlatformDiss.ViewModels;
using OnlineRetailPlatformDiss.Models;

namespace OnlineRetailPlatformDiss.Services
{
    public class BasketService
    {
        //Apply db context, implement a Basket ID...
        ApplicationDbContext? _context;
        public static Guid BasketServiceID { get; set; } = Guid.NewGuid();

        //Get the Basket Service????
        public static async Task<BasketService> GetBasket()
        {
            var basket = new BasketService();
            return await Task.FromResult(basket);
        }

        //Get the Items within the Basket...
        public async Task<BasketItemsViewModel> Basket()
        {
            var basket = await BasketService.GetBasket();

            //Implement the ViewModel
            var basketItemsVM = new BasketItemsViewModel()
            {
                BasketItems = await basket.GetBasketItems()
            };

            //Return the ViewModel to the Razor Component/Page
            return basketItemsVM;
        }

        //Add Item to Basket
        public async Task<BasketItemsMessageViewModel> AddToBasket(Guid id)
        {
            //Get the product from the db...
            var product = await _context.Products.SingleAsync(prod => prod.ProductID == id);

            //Fetch the Basket Service
            var basket = await BasketService.GetBasket();

            int quantity = await basket.AddToBasket(product);

            //Construct the Message VM...
            var message = new BasketItemsMessageViewModel()
            {
                Message = $"'{product.ProductName}' has been added to your Basket!",
                BasketTotal = await basket.GetTotalPrice(),
                BasketProductTotal = await basket.GetQuantity(),
                BasketQuantity = quantity
            };
            return message;
        }

        //Remove Item from Basket

        //Empty Basket

        //Functions -----

        //FUNCTION: Get Products from Basket
        public async Task<List<BasketsModel>> GetBasketItems()
        {
            //Returns the Product from every BasketModel where the BasketModel ID matches the Basket Service of the Users session?
            return await _context.Baskets.Include("BasketProduct").Where(basket => basket.BasketID == BasketServiceID).ToListAsync(); 
        }

        //FUNCTION: Add Product to the Basket
        public async Task<int> AddToBasket(ProductModel product)
        {
            //Get the matching BasketModel and the Product...
            var productToAdd = await _context.Baskets.SingleOrDefaultAsync(c => c.BasketID == BasketServiceID && c.BasketProduct == product);

            //If the Basket Item doesn't exist
            if (productToAdd == null)
            {
                productToAdd = new BasketsModel
                {
                    BasketID = Guid.NewGuid(),
                    BasketServiceID = BasketServiceID,
                    BasketProduct = product,
                    BasketQuantity = 1,
                    BasketPrice = product.PromotionalPrice
                };
                _context.Baskets.Add(productToAdd);
            }
            else
            {
                //If the BasketModel already exists...
                productToAdd.BasketQuantity++; //Increase the BasketQuantity...
            }
            //Save Changes to the db...
            await _context.SaveChangesAsync();

            return productToAdd.BasketQuantity;
        }

        //FUNCTION: Remove a Product from the Basket


        //FUNCTION: Empty the Basket


        //FUNCTION: Get the Quantity of each item in the basket and create a total...
        public async Task<int> GetQuantity()
        {
            int? quantity = await (from basketItems in _context.Baskets
                                   where basketItems.BasketID == BasketServiceID
                                   select (int?)basketItems.BasketQuantity).SumAsync();
            //Return quantity - if quantity is empty, then return 0
            return quantity ?? 0;
        }

        //FUNCTION: Get the Total Price of the Users basket...
        public async Task<decimal> GetTotalPrice()
        {
            //Multiply the Product Price by the quantity of that Product to get the current price for each of the Products within the cart...
            //Sum all Product Price Totals to get the Basket Total...
            decimal? totalPrice = await (from basketItems in _context.Baskets
                                         where basketItems.BasketID == BasketServiceID
                                         select (int?)basketItems.BasketQuantity * basketItems.BasketProduct.PromotionalPrice).SumAsync();
            //Return totalPrice - if empty Return a 0.0
            return totalPrice ?? decimal.Zero;
        }
    }
}

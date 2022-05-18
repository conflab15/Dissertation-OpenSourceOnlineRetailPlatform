using Xunit;
using Bunit;
using OnlineRetailPlatformDiss.Pages.Business;
using OnlineRetailPlatformDiss.Pages.Customer;
using OnlineRetailPlatformDiss.Data;
using Microsoft.Extensions.DependencyInjection;
using OnlineRetailPlatformDiss.Services;
using Microsoft.AspNetCore.Components;
using System.Linq;
using System;

namespace OnlineRetailPlatformDissUnitTests
{
    public class ViewProductDetailsPageUnitTests
    {
        [Fact]
        public void ViewDetailsPageProductsRenders()
        {
            //Test - Check if the ProductView component loads correctly...
            //Arrange
            using var ctx = new TestContext();

            //Registering Services.
            ctx.Services.AddSingleton<ApplicationDbContext>(new ApplicationDbContext());
            ctx.Services.AddScoped<AppState>();
            ctx.Services.AddScoped<ShoppingBasketService>();
            ctx.JSInterop.Mode = JSRuntimeMode.Loose;


            Guid ID = new Guid("c9b512f6-b0cd-4355-b06d-1a144c6710d7");

            //Act
            var cut = ctx.RenderComponent<ProductDetails>(parameters => parameters
            .Add(p => p.ProductId, ID));

            //Assert
            cut.Markup.Contains("Funko Pop! Iron Man");
        }

        [Fact]
        public void ViewDetailsPageAddItemToBasketFails()
        {
            //Test - Add an Item to the basket returns error message - no colour selected
            //Arrange
            using var ctx = new TestContext();

            //Registering Services.
            ctx.Services.AddSingleton<ApplicationDbContext>(new ApplicationDbContext());
            ctx.Services.AddScoped<AppState>();
            ctx.Services.AddScoped<ShoppingBasketService>();
            ctx.JSInterop.Mode = JSRuntimeMode.Loose;

            Guid ID = new Guid("c9b512f6-b0cd-4355-b06d-1a144c6710d7");

            //Act
            var cut = ctx.RenderComponent<ProductDetails>(parameters => parameters
            .Add(p => p.ProductId, ID));

            //Assert
            cut.Markup.Contains("Funko Pop! Iron Man");

            cut.Find("button:contains('Add to Basket')").Click();

            cut.Markup.Contains("0 Items");
        }

        [Fact]
        public void ViewDetailsPageAddItemToBasketSuccess()
        {
            //Test - Add an Item to the basket returns success message
            //Arrange
            using var ctx = new TestContext();

            //Registering Services.
            ctx.Services.AddSingleton<ApplicationDbContext>(new ApplicationDbContext());
            ctx.Services.AddScoped<AppState>();
            ctx.Services.AddScoped<ShoppingBasketService>();
            ctx.JSInterop.Mode = JSRuntimeMode.Loose;

            Guid ID = new Guid("7bb4a2d9-31aa-433e-355c-08da38b10955");

            //Act
            var cut = ctx.RenderComponent<ProductDetails>(parameters => parameters
            .Add(p => p.ProductId, ID));

            //Assert
            cut.Markup.Contains("Lego City Police Car");

            cut.Find("button:contains('Add to Basket')").Click();

            cut.Markup.Contains("0 Items");
        }
    }
}

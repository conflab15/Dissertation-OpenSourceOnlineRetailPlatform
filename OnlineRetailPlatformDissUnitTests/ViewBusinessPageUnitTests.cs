using Xunit;
using Bunit;
using OnlineRetailPlatformDiss.Pages.Business;
using OnlineRetailPlatformDiss.Pages.Customer;
using OnlineRetailPlatformDiss.Data;
using Microsoft.Extensions.DependencyInjection;
using OnlineRetailPlatformDiss.Services;
using Microsoft.AspNetCore.Components;
using System.Linq;
using Bunit.TestDoubles;

namespace OnlineRetailPlatformDissUnitTests
{
    public class ViewBusinessPageUnitTests
    {
        [Fact]
        public void ViewBusinessPageRendersCorrectly()
        {
            //Test - Check that the component renders correctly...
            //Arrange
            using var ctx = new TestContext();

            //Registering Services.
            ctx.Services.AddSingleton<ApplicationDbContext>(new ApplicationDbContext());

            //Act
            var cut = ctx.RenderComponent<ViewBusiness>();

            //Assert
            cut.Markup.Contains("Businesses");
        }

        [Fact]
        public void ViewBusinessPageSearchShowsResults()
        {
            //Test - Check that the component renders correctly...
            //Arrange
            using var ctx = new TestContext();

            //Registering Services.
            ctx.Services.AddSingleton<ApplicationDbContext>(new ApplicationDbContext());

            //Act
            var cut = ctx.RenderComponent<ViewBusiness>();
            var inputs = cut.FindAll("input");

            cut.Find("input[name=SearchString]").Change("BusiTown");
            cut.Find("input[value=Submit]").Click();

            //Assert
            cut.Markup.Contains("Businesses within: 'BusiTown'");
        }

        //Business Details Tests

        [Fact]
        public void BusinessDetailsRendersCorrectly()
        {
            //Test - Check that the BusinessDetails component renders correctly
            //Arrange
            using var ctx = new TestContext();

            //Registering Services.
            ctx.Services.AddSingleton<ApplicationDbContext>(new ApplicationDbContext());
            ctx.Services.AddScoped<AppState>();
            ctx.Services.AddScoped<ShoppingBasketService>();

            //Act
            var cut = ctx.RenderComponent<BusinessDetails>(parameters => parameters
            .Add(p => p.businessName, "Toy Store"));

            //Assert
            cut.Markup.Contains("Toy Store");
        }

        [Fact]
        public void BusinessDetailsSearchProductsShowsProducts()
        {
            //Test - When rendered, the SearchProducts Component shows products that the business sells.
            //Arrange
            using var ctx = new TestContext();

            //Registering Services.
            ctx.Services.AddSingleton<ApplicationDbContext>(new ApplicationDbContext());
            ctx.Services.AddScoped<AppState>();
            ctx.Services.AddScoped<ShoppingBasketService>();

            //Act
            var cut = ctx.RenderComponent<BusinessDetails>(parameters => parameters
            .Add(p => p.businessName, "Toy Store"));

            //Assert
            cut.Markup.Contains("Sold by Toy Store");
        }
    }
}

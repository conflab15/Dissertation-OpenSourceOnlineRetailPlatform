using Xunit;
using Bunit;
using OnlineRetailPlatformDiss.Pages.Business;
using OnlineRetailPlatformDiss.Pages.Customer;
using OnlineRetailPlatformDiss.Data;
using Microsoft.Extensions.DependencyInjection;
using OnlineRetailPlatformDiss.Services;
using Microsoft.AspNetCore.Components;
using System.Linq;

namespace OnlineRetailPlatformDissUnitTests
{
    public class CheckoutPageUnitTests
    {
        [Fact]
        public void CheckoutPageRenders()
        {
            //Test - Check that the checkout page correctly loads
            //Arrange
            using var ctx = new TestContext();

            //Registering Services.
            ctx.Services.AddSingleton<ApplicationDbContext>(new ApplicationDbContext());
            ctx.Services.AddScoped<AppState>();
            ctx.Services.AddScoped<ShoppingBasketService>();
            ctx.Services.GetRequiredService<NavigationManager>();
            ctx.JSInterop.Mode = JSRuntimeMode.Loose;

            //Act
            var cut = ctx.RenderComponent<Checkout>();

            //Assert
            cut.Markup.Contains("Checkout");
        }
    }
}

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
    public class OwnerViewProductsPageUnitTests
    {
        [Fact]
        public void OwnerViewProductsRenders()
        {
            //Test - Check if the OwnerViewProducts component loads correctly...
            //Arrange
            using var ctx = new TestContext();

            //Registering Services.
            ctx.Services.AddSingleton<ApplicationDbContext>(new ApplicationDbContext());
            ctx.JSInterop.Mode = JSRuntimeMode.Loose;

            //Act
            var cut = ctx.RenderComponent<OwnerViewProducts>(parameters => parameters
            .Add(p => p.businessName, "Toy Store"));

            //Assert
            cut.Markup.Contains("Products for: Toy Store");
        }

        [Fact]
        public void OwnerViewProductsMessageIfNoProductsAreFound()
        {
            //Test - Check if products are available to edit...
            //Arrange
            using var ctx = new TestContext();

            //Registering Services.
            ctx.Services.AddSingleton<ApplicationDbContext>(new ApplicationDbContext());
            ctx.JSInterop.Mode = JSRuntimeMode.Loose;

            //Act
            var cut = ctx.RenderComponent<OwnerViewProducts>(parameters => parameters
            .Add(p => p.businessName, "Toy Store"));

            //Assert
            cut.Markup.Contains("We couldn't find any products for your business!");

        }
    }
}

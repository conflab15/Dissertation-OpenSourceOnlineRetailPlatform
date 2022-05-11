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
    public class CreateProductPageUnitTests
    {
        [Fact]
        public void CreateProductPageRendersCorrectly()
        {
            //Test - Check that the component renders correctly...
            //Arrange
            using var ctx = new TestContext();
            var authContext = ctx.AddTestAuthorization();
            authContext.SetAuthorized("test@test.com", AuthorizationState.Authorized);

            //Registering Services.
            ctx.Services.AddSingleton<ApplicationDbContext>(new ApplicationDbContext());
            ctx.JSInterop.Mode = JSRuntimeMode.Loose;

            //Act
            var cut = ctx.RenderComponent<CreateProduct>();

            //Assert
            cut.Markup.Contains("Create a new product:");
        }

        [Fact]
        public void CreateProductErrorShowsWhenUserHasNoBusinesses()
        {
            //Test - Check that the Create Product page shows an error when the user has no businesses...
            //Arrange
            using var ctx = new TestContext();
            var authContext = ctx.AddTestAuthorization();
            authContext.SetAuthorized("test@test.com", AuthorizationState.Authorized);

            //Registering Services.
            ctx.Services.AddSingleton<ApplicationDbContext>(new ApplicationDbContext());
            ctx.JSInterop.Mode = JSRuntimeMode.Loose;

            //Act
            var cut = ctx.RenderComponent<CreateProduct>();

            //Assert
            cut.Markup.Contains("You must create a Business to be able to create products for!");
        }
    }
}

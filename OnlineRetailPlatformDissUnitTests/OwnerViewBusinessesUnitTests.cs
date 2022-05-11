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
    public class OwnerViewBusinessesUnitTests
    {
        [Fact]
        public void OwnerViewBusinessPageRendersCorrectly()
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
            var cut = ctx.RenderComponent<OwnerViewBusiness>();

            //Assert
            cut.Markup.Contains("Businesses managed by: test@test.com");
        }

        [Fact]
        public void OwnerViewBusinessPageDisplaysNoBusinessesWarning()
        {
            //Test - Check that the No Businesses Found warning displays...
            //Arrange
            using var ctx = new TestContext();
            var authContext = ctx.AddTestAuthorization();
            authContext.SetAuthorized("test@test.com", AuthorizationState.Authorized);

            //Registering Services.
            ctx.Services.AddSingleton<ApplicationDbContext>(new ApplicationDbContext());
            ctx.JSInterop.Mode = JSRuntimeMode.Loose;

            //Act
            var cut = ctx.RenderComponent<OwnerViewBusiness>();

            //Assert
            cut.Markup.Contains("Hmmm... it looks like test@test.com does not manage any businesses...");
        }


    }
}

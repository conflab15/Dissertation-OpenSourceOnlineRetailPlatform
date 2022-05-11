using Xunit;
using Bunit;
using OnlineRetailPlatformDiss.Pages.Business;
using OnlineRetailPlatformDiss.Pages.Customer;
using OnlineRetailPlatformDiss.Data;
using Microsoft.Extensions.DependencyInjection;
using OnlineRetailPlatformDiss.Services;
using Bunit.TestDoubles;
using OnlineRetailPlatformDiss.Pages.Customer.Orders;

namespace OnlineRetailPlatformDissUnitTests
{
    public class OrderListPageUnitTests
    {
        [Fact]
        public void OrderListPageRendersCorrectly()
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
            var cut = ctx.RenderComponent<OrderList>();

            //Assert
            cut.Markup.Contains("Uh oh!");
        }
    }
}

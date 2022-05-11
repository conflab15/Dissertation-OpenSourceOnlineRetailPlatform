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

            cut.Find("input[name=SearchString]").Change("Taunton");
            cut.Find("input[value=Submit]").Click();

            //Assert
            cut.Markup.Contains("Showing Businesses within: 'Taunton'");
        }
    }
}

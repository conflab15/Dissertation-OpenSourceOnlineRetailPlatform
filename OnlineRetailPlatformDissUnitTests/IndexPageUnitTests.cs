using Xunit;
using Bunit;
using OnlineRetailPlatformDiss.Pages.Business;
using OnlineRetailPlatformDiss.Pages.Customer;
using OnlineRetailPlatformDiss.Data;
using Microsoft.Extensions.DependencyInjection;
using OnlineRetailPlatformDiss.Services;

namespace OnlineRetailPlatformDissUnitTests
{
    public class IndexPageUnitTests
    {
        [Fact]
        public void IndexPageBusinessOptionsRendersWithUsername()
        {
            //Test - BusinessManagerOptions renders correctly when a username is passed via the parameters
            //Arrange
            using var ctx = new TestContext();

            //Act
            var cut = ctx.RenderComponent<BusinessManagerOptions>(parameters => parameters
            .Add(p => p.UserForename, "testuser@test.com"));

            //Assert
            cut.Markup.Contains("<h1 pb-2>Welcome back, testuser@test.com, Business Management Options are shown below!</h1>");
        }

        [Fact]
        public void IndexPageSearchProductsRendersCorrectly()
        {
            //Test - Checks if the SearchProducts component loads correctly...
            //Arrange
            using var ctx = new TestContext();

            //Registering Services.
            ctx.Services.AddSingleton<ApplicationDbContext>(new ApplicationDbContext());
            ctx.Services.AddScoped<AppState>();
            ctx.Services.AddScoped<ShoppingBasketService>();

            //Act
            var cut = ctx.RenderComponent<SearchProducts>();

            //Assert
            cut.Markup.Contains("<h2>Sort by:</h2>");
        }

        [Fact]
        public void IndexPageSearchProductsRendersWithSearchCriteria()
        {
            //Test - Checks if the Search Criteria is applied when the button is clicked...
            //Arrange
            using var ctx = new TestContext();

            //Registering Services.
            ctx.Services.AddSingleton<ApplicationDbContext>(new ApplicationDbContext());
            ctx.Services.AddScoped<AppState>();
            ctx.Services.AddScoped<ShoppingBasketService>();

            //Act
            var cut = ctx.RenderComponent<SearchProducts>();
            cut.Find("input[name=SearchString]").Change("Knitted");
            cut.Find("input[value=Search]").Click();

            //Assert
            cut.Markup.Contains("<h2>Search Criteria: 'Knitted' </h2>");
        }

        [Fact]
        public void IndexPageSearchProductsFiltersByPrice()
        {
            //Test - Checks if the Filter by price is applied when the button is clicked 
            //Arrange
            using var ctx = new TestContext();

            //Registering Services.
            ctx.Services.AddSingleton<ApplicationDbContext>(new ApplicationDbContext());
            ctx.Services.AddScoped<AppState>();
            ctx.Services.AddScoped<ShoppingBasketService>();

            //Act
            var cut = ctx.RenderComponent<SearchProducts>();
            cut.Find("a:contains('Price (Ascending)')").Click();

            //Assert
            cut.Markup.Contains("<h2>Sorted by Price (Ascending)</h2>");
        }

        [Fact]
        public void IndexPageSearchProductsFiltersByAZ()
        {
            //Test - Checks if the Filter by A-Z is applied when the button is clicked 
            //Arrange
            using var ctx = new TestContext();

            //Registering Services.
            ctx.Services.AddSingleton<ApplicationDbContext>(new ApplicationDbContext());
            ctx.Services.AddScoped<AppState>();
            ctx.Services.AddScoped<ShoppingBasketService>();

            //Act
            var cut = ctx.RenderComponent<SearchProducts>();
            cut.Find("a:contains('Title (A-Z)')").Click();

            //Assert
            cut.Markup.Contains("<h2>Sorted by Title (A-Z)</h2>");
        }
    }
}

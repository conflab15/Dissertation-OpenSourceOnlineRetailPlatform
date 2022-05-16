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
    public class CreateBusinessPageUnitTests
    {
        [Fact]
        public void CreateBusinessPageRendersCorrectly()
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
            var cut = ctx.RenderComponent<CreateBusiness>();

            //Assert
            cut.Markup.Contains("Create your business");
        }

        [Fact]
        public void CreateBusinessPageUnauthorizedReturnsError()
        {
            //Test - When a user tries to access this page without being logged in, a message appears...
            //Arrange
            using var ctx = new TestContext();
            var authContext = ctx.AddTestAuthorization();
            authContext.SetNotAuthorized();

            //Registering Services.
            ctx.Services.AddSingleton<ApplicationDbContext>(new ApplicationDbContext());
            ctx.JSInterop.Mode = JSRuntimeMode.Loose;

            //Act
            var cut = ctx.RenderComponent<CreateBusiness>();

            //Assert
            cut.Markup.Contains("Oops! Something went wrong.");
        }

        [Fact]
        public void CreateEmptyBusinessReturnsError()
        {
            //Test - Check that the input forms change red when no data is entered...
            //Arrange
            using var ctx = new TestContext();
            var authContext = ctx.AddTestAuthorization();
            authContext.SetAuthorized("test@test.com", AuthorizationState.Authorized);

            //Registering Services.
            ctx.Services.AddSingleton<ApplicationDbContext>(new ApplicationDbContext());
            ctx.JSInterop.Mode = JSRuntimeMode.Loose;

            //Act
            var cut = ctx.RenderComponent<CreateBusiness>();

            var buttons = cut.FindAll("button"); //Find all Buttons
            var button = buttons.FirstOrDefault(b => b.ParentElement.TextContent.Contains("Submit"));
            button?.Click(); //Click the Submit Button

            var inputs = cut.FindAll("input");
            var correct = inputs.FirstOrDefault(i => i.ClassName.Contains("form-control invalid")).ToString();
            
            //Find an input field with a form-control invalid class
            cut.Markup.Contains(correct);
        }
    }
}

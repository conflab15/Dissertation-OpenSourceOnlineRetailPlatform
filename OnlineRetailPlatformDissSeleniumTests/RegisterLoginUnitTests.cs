using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace OnlineRetailPlatformDissSeleniumTests
{
    public class RegisterLoginUnitTests
    {
        IWebDriver driver;

        [OneTimeSetUp]
        public void Setup()
        {
            //OneTimeSetup allows for the tests to use the same setup, saving time when testing..
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://dissertationonlineretailplatform.azurewebsites.net/Identity/Account/Register");
        }
        [Test]
        public void RegisterUserDoesNotAcceptBadModel()
        {
            //Nothing is entered into the Input Fields
            var submitBtn = driver.FindElement(By.Id("registerSubmit"));
            submitBtn.Click();

            var title = driver.Title;
            Assert.AreEqual(title, "Register - OnlineRetailPlatformDiss");
        }


        [Test]
        public void RegisterNewUser()
        {
            var inputEmail = driver.FindElement(By.Id("Input_Email"));
            inputEmail.SendKeys("selenium1@test.com");
            var inputForename = driver.FindElement(By.Id("Input_Forename"));
            inputForename.SendKeys("Selenium");
            var inputSurname = driver.FindElement(By.Id("Input_Surname"));
            inputSurname.SendKeys("User");
            var inputAddr1 = driver.FindElement(By.Id("Input_AddressLine1"));
            inputAddr1.SendKeys("Selenium Street");
            var inputTown = driver.FindElement(By.Id("Input_Town"));
            inputTown.SendKeys("Selenium Town");
            var inputCounty = driver.FindElement(By.Id("Input_County"));
            inputCounty.SendKeys("Selenium County");
            var inputPostCode = driver.FindElement(By.Id("Input_PostCode"));
            inputPostCode.SendKeys("SE1 1UM");
            var inputPass = driver.FindElement(By.Id("Input_Password"));
            inputPass.SendKeys("Selenium1!");
            var inputConfPass = driver.FindElement(By.Id("Input_ConfirmPass"));
            inputConfPass.SendKeys("Selenium1!");

            var submitBtn = driver.FindElement(By.Id("registerSubmit"));
            submitBtn.Click();

            var title = driver.Title;
            Assert.AreEqual(title, "Welcome");
        }

        [Test]
        public void LoginWithRegisteredAccount()
        {
            driver.Navigate().GoToUrl("https://dissertationonlineretailplatform.azurewebsites.net/Identity/Account/Login");
            var inputEmail = driver.FindElement(By.Id("Input_Email"));
            inputEmail.SendKeys("selenium2@test.com");
            var inputPass = driver.FindElement(By.Id("Input_Pass"));
            inputPass.SendKeys("Password!");

            var submitBtn = driver.FindElement(By.Id("login-submit"));
            submitBtn.Click();

            var title = driver.Title;
            Assert.AreEqual(title, "Welcome");
        }

        [OneTimeTearDown]
        public void End()
        {
            driver.Close();
        }
    }
}
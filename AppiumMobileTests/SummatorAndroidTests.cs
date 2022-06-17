using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using System;
using System.Linq;


namespace AppiumMobileTests
{
   

    
    
    public class SummatorAndroidTests


    {
        private AndroidDriver<AndroidElement> driver;
        private AppiumOptions options;
        private const string AppiumUri = "http://127.0.0.1:4723/wd/hub";
        private const string app = @"C:\com.example.androidappsummator.apk";


        [SetUp]
        public void StartApp()
        {
            this.options = new AppiumOptions() { PlatformName = "Android" };
            options.AddAdditionalCapability("app", app);
            this.driver = new AndroidDriver<AndroidElement>(new Uri(AppiumUri), options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
            
        }

        [TearDown]
        public void CloseApp()
        {
            driver.Quit();
        }

        [Test]
        public void Test_SummatorApp_TwoPositiveValues()
        {
            // Arrange
            var field1 = driver.FindElement(By.Id("com.example.androidappsummator:id/editText1"));
            field1.SendKeys("5");

            var field2 = driver.FindElement(By.Id("com.example.androidappsummator:id/editText2"));
            field2.SendKeys("5");





            //Act
            var buttonCalc = driver.FindElement(By.Id("com.example.androidappsummator:id/buttonCalcSum"));
            buttonCalc.Click();

            var fieldResult = driver.FindElement(By.Id("com.example.androidappsummator:id/editTextSum")).Text;

            //Assert
            Assert.That(fieldResult, Is.EqualTo("10"));
        }
    }
}
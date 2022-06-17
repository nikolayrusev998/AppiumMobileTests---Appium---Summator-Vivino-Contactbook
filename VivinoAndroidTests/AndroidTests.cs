using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using System;

namespace VivinoAndroidTests
{
    public class AndroidTests
    {

        private const string AppiumServerUri = "http://127.0.0.1:4723/wd/hub";
        private const string VivinoAppPackage = "vivino.web.app";
        private const string VivinoAppStartupActivity = "com.sphinx_solution.activities.SplashActivity";
        private const string VivinoTestAccountEmail = "test_vivino@gmail.com";
        private const string VivinoTestAccountPassword = "p@ss987654321";
        private AndroidDriver<AndroidElement> driver;

        [SetUp]
        public void Setup()
        {
            var appiumOptions = new AppiumOptions() { PlatformName = "Android" };
            appiumOptions.AddAdditionalCapability("appPackage", VivinoAppPackage);
            appiumOptions.AddAdditionalCapability("appActivity", VivinoAppStartupActivity);
            driver = new AndroidDriver<AndroidElement>(new Uri(AppiumServerUri), appiumOptions);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        [TearDown]
        public void Teardown() 
        {
            driver.Quit();
        }

        [Test]
        public void Test_LoginFunctionality()
        {
            var linkLogin = driver.FindElementById("vivino.web.app:id/txthaveaccount");
            linkLogin.Click();

            var emailTextField = driver.FindElementById("vivino.web.app:id/edtEmail");
            emailTextField.SendKeys(VivinoTestAccountEmail);

            var passwordTextField = driver.FindElementById("vivino.web.app:id/edtPassword");
            passwordTextField.SendKeys(VivinoTestAccountPassword);

            var loginButton = driver.FindElementById("vivino.web.app:id/action_signin");
            loginButton.Click();

            driver.FindElementById("vivino.web.app:id/wine_explorer_tab").Click();

            var searchField = driver.FindElementById("vivino.web.app:id/search_vivino");
            searchField.Click();

            driver.FindElementById("vivino.web.app:id/editText_input").SendKeys("Katarzyna Reserve Red 2006");

            var winePresented = driver.FindElementById("vivino.web.app:id/winename_textView").Text;
            Assert.That(winePresented, Is.EqualTo("Reserve Red 2006"));

            driver.FindElementById("vivino.web.app:id/wineimage").Click();

            var wineName = driver.FindElementById("vivino.web.app:id/wine_name").Text;
            Assert.That(wineName, Is.EqualTo("Reserve Red 2006"));

            var wineRating = driver.FindElementById("vivino.web.app:id/rating").Text;
            Assert.That(wineRating, Is.InRange("1.00", "5.00"));

            var tabsSummary = driver.FindElementById("vivino.web.app:id/tabs");
            var tabHighlights = tabsSummary.FindElementByXPath("//android.widget.TextView[1]");
            
            tabHighlights.Click();
            var highlightsDescription = driver.FindElementByAndroidUIAutomator(
                "new UiScrollable(new UiSelector().scrollable(true))" +
                ".scrollIntoView(new UiSelector().resourceIdMatches(" +
                "\"vivino.web.app:id/highlight_description\"))");
            Assert.That(highlightsDescription.Text, Is.EqualTo("Among top 1% of all wines in the world"));

            var tabFacts = tabsSummary.FindElementByXPath("//android.widget.TextView[2]");
            tabFacts.Click();
            var factTitle = driver.FindElementById("vivino.web.app:id/wine_fact_title");
            Assert.That(factTitle.Text, Is.EqualTo("Grapes"));
            var factText = driver.FindElementById("vivino.web.app:id/wine_fact_text");
            Assert.That(factText.Text, Is.EqualTo("Cabernet Sauvignon,Merlot"));
        }
        
    }
}
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;
using System;

namespace ContactBookAndroidTests
{
    public class AndroidTests
    {

        public class AndroidAppiumTestsContactBook
        {
            private const string AppiumServerUri = "http://127.0.0.1:4723/wd/hub";
            private const string ContactBookAppPath = @"C:\contactbook-androidclient.apk";
            private const string ApiServiceUrl = "http://contactbook.nakov.repl.co/api";
            private AndroidDriver<AndroidElement> driver;
            private WebDriverWait wait;

            [SetUp]
            public void Setup()
            {
                var options = new AppiumOptions() { PlatformName = "Android" };
                options.AddAdditionalCapability("app", ContactBookAppPath);
                driver = new AndroidDriver<AndroidElement>(new Uri(AppiumServerUri), options);
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            }

            [TearDown]
            public void TearDown()
            {
                driver.Quit();
            }

            [Test]
            public void Test_AndroidApp_SearchSteveContact()
            {
                var editTextApiUrl = driver.FindElementById("contactbook.androidclient:id/editTextApiUrl");
                editTextApiUrl.Clear();
                editTextApiUrl.SendKeys(ApiServiceUrl);

                var buttonConnect = driver.FindElementById("contactbook.androidclient:id/buttonConnect");
                buttonConnect.Click();

                var editTextKeyword = driver.FindElementById("contactbook.androidclient:id/editTextKeyword");
                editTextKeyword.Clear();
                editTextKeyword.SendKeys("Steve");

                var buttonSearch = driver.FindElementById("contactbook.androidclient:id/buttonSearch");
                buttonSearch.Click();

                // Assert that one contact is displayed
                var textViewSearchResult = driver.FindElementById("contactbook.androidclient:id/textViewSearchResult");
                wait.Until(t => textViewSearchResult.Text != "");
                var text = textViewSearchResult.Text;
                Assert.That(text.Contains("Contacts found: 1"));

                //Assert that the contact is "Steve Jobs"
                var textViewFirstName = driver.FindElementById("contactbook.androidclient:id/textViewFirstName");
                Assert.That(textViewFirstName.Text, Is.EqualTo("Steve"));

                var textViewLastName = driver.FindElementById("contactbook.androidclient:id/textViewLastName");
                Assert.That(textViewLastName.Text, Is.EqualTo("Jobs"));



            }

            [Test]
            public void Test_AndroidApp_SearchMultipleContacts()
            {
                var editTextApiUrl = driver.FindElementById("contactbook.androidclient:id/editTextApiUrl");
                editTextApiUrl.Clear();
                editTextApiUrl.SendKeys(ApiServiceUrl);

                var buttonConnect = driver.FindElementById("contactbook.androidclient:id/buttonConnect");
                buttonConnect.Click();

                var editTextKeyword = driver.FindElementById("contactbook.androidclient:id/editTextKeyword");
                editTextKeyword.Clear();
                editTextKeyword.SendKeys("e");

                var buttonSearch = driver.FindElementById("contactbook.androidclient:id/buttonSearch");
                buttonSearch.Click();

                // Assert that one contact is displayed
                var textViewSearchResult = driver.FindElementById("contactbook.androidclient:id/textViewSearchResult");
                wait.Until(t => textViewSearchResult.Text != "");
                var text = textViewSearchResult.Text;
                Assert.That(text.Contains("Contacts found: 14"));

            }
            [Test]
            public void Test_AndroidApp_SearchInvalidName()
            {
                var editTextApiUrl = driver.FindElementById("contactbook.androidclient:id/editTextApiUrl");
                editTextApiUrl.Clear();
                editTextApiUrl.SendKeys(ApiServiceUrl);

                var buttonConnect = driver.FindElementById("contactbook.androidclient:id/buttonConnect");
                buttonConnect.Click();

                var editTextKeyword = driver.FindElementById("contactbook.androidclient:id/editTextKeyword");
                editTextKeyword.Clear();
                editTextKeyword.SendKeys("Niko brrratski ");

                var buttonSearch = driver.FindElementById("contactbook.androidclient:id/buttonSearch");
                buttonSearch.Click();

                // Assert that one contact is displayed
                var textViewSearchResult = driver.FindElementById("contactbook.androidclient:id/textViewSearchResult");
                wait.Until(t => textViewSearchResult.Text != "");
                var text = textViewSearchResult.Text;
                Assert.That(text.Contains("Contacts found: 0"));




            }
        }
    }
}


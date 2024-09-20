using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SauceDemoTests
{
    [TestClass]
    public class LoginPageTests
    {
        private IWebDriver _driver;
        private LoginPage _loginPage;
        private InventoryPage _inventoryPage;
        private SeleniumHelpers _seleniumHelpers;

        [TestInitialize]
        public void Setup()
        {
            _driver = new ChromeDriver();
            _loginPage = new LoginPage(_driver);
            _inventoryPage = new InventoryPage(_driver);
            _seleniumHelpers = new SeleniumHelpers(_driver, _inventoryPage, _loginPage);
        }

        [TestMethod]
        public void LoginTest()
        {
            _seleniumHelpers.LoginAs("standard_user");

            Assert.AreEqual(_inventoryPage.Url, _driver.Url);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}
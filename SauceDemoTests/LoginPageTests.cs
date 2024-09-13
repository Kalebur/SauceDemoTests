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

        [TestInitialize]
        public void Setup()
        {
            _driver = new ChromeDriver();
            _loginPage = new LoginPage(_driver);
            _inventoryPage = new InventoryPage(_driver);
        }

        [TestMethod]
        [DataRow("standard_user")]
        [DataRow("locked_out_user")]
        [DataRow("problem_user")]
        [DataRow("performance_glitch_user")]
        [DataRow("error_user")]
        [DataRow("visual_user")]
        public void TestMethod1(string username)
        {
            var password = "secret_sauce";

            _driver.Navigate().GoToUrl(_loginPage.Url);
            _loginPage.UsernameTextbox.SendKeys(username);
            _loginPage.PasswordTextbox.SendKeys(password);
            _loginPage.LoginButton.Click();
            var shoppingCartLinkDisplayed = _inventoryPage.ShoppingCartLink.Displayed;

            Assert.IsTrue(shoppingCartLinkDisplayed);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}
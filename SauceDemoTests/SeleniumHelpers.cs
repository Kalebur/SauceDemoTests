using OpenQA.Selenium;

namespace SauceDemoTests
{
    public class SeleniumHelpers
    {
        private readonly IWebDriver _driver;
        private readonly InventoryPage _inventoryPage;
        private readonly LoginPage _loginPage;
        private const string _password = "secret_sauce";

        public SeleniumHelpers(IWebDriver driver, InventoryPage inventoryPage, LoginPage loginPage)
        {
            _driver = driver;
            _inventoryPage = inventoryPage;
            _loginPage = loginPage;
        }

        public void NavigateTo(string url)
        {
            _driver.Navigate().GoToUrl(url);
        }

        public void LoginAs(string username)
        {
            NavigateTo(_inventoryPage.Url);
            _loginPage.UsernameTextbox.SendKeys(username);
            _loginPage.PasswordTextbox.SendKeys(_password);
            _loginPage.LoginButton.Click();
        }
    }
}

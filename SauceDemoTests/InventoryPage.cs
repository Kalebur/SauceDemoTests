using OpenQA.Selenium;

namespace SauceDemoTests
{
    public class InventoryPage
    {
        private readonly IWebDriver _driver;

        public InventoryPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public string Url = "https://www.saucedemo.com/inventory.html";

        public IWebElement ShoppingCartLink => _driver.FindElement(By.Id("shopping_cart_container"));
    }
}

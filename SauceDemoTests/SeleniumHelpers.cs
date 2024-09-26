using OpenQA.Selenium;
using System.Text;
using System.Text.Json;

namespace SauceDemoTests
{
    public class SeleniumHelpers
    {
        private readonly IWebDriver _driver;
        private readonly InventoryPage _inventoryPage;
        private readonly LoginPage _loginPage;
        private const string _password = "secret_sauce";
        private const string _cookieFilePath = "sauce-demo-cookies.txt";

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

        public Cookie GetSessionCookie()
        {
            LoginAs("standard_user");
            var sessionCookie = _driver.Manage().Cookies.GetCookieNamed("session-username");

            return sessionCookie;
        }

        public void GotoInventoryPage()
        {
            if (!File.Exists(_cookieFilePath))
            {
                var cookie = GetSessionCookie();
                File.WriteAllText(_cookieFilePath, cookie.ToString());
            }
            else
            {
                Cookie cookie;
                var cookieValues = new Dictionary<string, string>();
                var cookieData = File.ReadAllText(_cookieFilePath);
                var dataItems = cookieData.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                foreach (var item in dataItems)
                {
                    var data = item.Split('=');
                    cookieValues.Add(data[0], data[1]);
                }

                var expirationTime = DateTime.Parse(cookieValues["expires"][..23]);

                if (expirationTime <= DateTime.Now - TimeSpan.FromMinutes(60))
                {
                    expirationTime.AddDays(2);
                    File.Delete(_cookieFilePath);
                    cookie = new Cookie("session-username", cookieValues["session-username"], cookieValues["domain"], cookieValues["path"], expirationTime.AddDays(2));
                    File.WriteAllText(_cookieFilePath, cookie.ToString());
                }

                cookie = new Cookie("session-username", cookieValues["session-username"], cookieValues["domain"], cookieValues["path"], expirationTime.AddDays(2));
                NavigateTo(_loginPage.Url);
                _driver.Manage().Cookies.AddCookie(cookie);
                NavigateTo(_inventoryPage.Url);
            }
        }
    }
}

using InstagramAutomatization.Data;
using InstagramAutomatization.Services.Abstractions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumUndetectedChromeDriver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstagramAutomatization.Services
{
    internal class InstagramSelenium : IInstagram
    {
        private string _driverPath;
        private WebDriver _driver;
        public bool _statusLogin = false;
        private int _period;

        public InstagramSelenium(string driverPath, int period)
        {
            _period = period;
            _driverPath = driverPath;
            ChromeOptions chromeCapabilities = new ChromeOptions();
            chromeCapabilities.EnableMobileEmulation("iPhone SE");
            _driver = new ChromeDriver(driverPath, chromeCapabilities);
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
        }

        public bool GetStatusLogin()
        {
            return _statusLogin;
        }
        public IInstagramUser Login(string username, string password)
        {
            if (!_statusLogin)
            {
                var user = new InstagramSeleniumUser(username, password, _driver, _period);
                _statusLogin = true;
                return user;
            }
            else
            {
                throw new Exception("You are already logined");
            }
        }

        public UserInfo GetUserInfo(string username)
        {
            Thread.Sleep(_period);
            _driver.Navigate().GoToUrl(@"https://www.instagram.com/" + username);
            Thread.Sleep(_period);
            var name = "";
            try
            {
                name = _driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[2]/div/div/div/div[1]/div[1]/div[2]/div[2]/section/main/div/div[1]/div[1]/span")).Text;
            }

            catch { }

            var description = "";
            try
            {
                description = _driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[2]/div/div/div/div[1]/div[1]/div[2]/div[2]/section/main/div/div[1]/h1")).Text;
            }
            catch { }

            var link = "";
            try
            {
                link = _driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[2]/div/div/div/div[1]/div[1]/div[2]/div[2]/section/main/div/div[1]/div[4]/a/span/span")).Text;
            }
            catch { }

            return new UserInfo(username, name, description, link);
        }

        public List<UserInfo> GetUsersInfo(List<string> usernames)
        {
            List<UserInfo> users = new List<UserInfo>();

            foreach (var user in usernames)
                users.Add(GetUserInfo(user));

            return users;
        }
    }
}

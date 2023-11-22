using InstagramAutomatization.Data;
using InstagramAutomatization.Services.Abstractions;
using OpenQA.Selenium;
using SeleniumUndetectedChromeDriver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstagramAutomatization.Services
{
    internal class InstagramSeleniumUser : IInstagramUser
    {
        private WebDriver _driver;

        private string _username;
        private int _period;
        public InstagramSeleniumUser(string username, string password, WebDriver driver, int period)
        {
            _driver = driver;
            _username = username;
            _period = period;

            driver.Navigate().GoToUrl(@"https://www.instagram.com/accounts/login/?next=%2Fusers%2Fself&source=mobile_nav");

            Thread.Sleep(_period);

            driver.FindElement(By.Name("username")).SendKeys(username);
            driver.FindElement(By.Name("password")).SendKeys(password);

            driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[2]/div/div/div/div[1]/section/main/div[1]/div/div/div[2]/form/div[1]/div[6]/button")).Click();

            try
            {
                Thread.Sleep(10000);
                driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[2]/div/div/div/div[1]/div[1]/div[1]/div/div/div/div/div/div[3]/span/div/a"));
            }
            catch
            {
                throw new Exception("Not correct login or password");
            }

            Console.WriteLine("Logined");
        }

        public long GetFollowersCount()
        {
            Thread.Sleep(_period);
            _driver.Navigate().GoToUrl($"https://www.instagram.com/{_username}");
            Thread.Sleep(_period);

            var countStr = _driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[2]/div/div/div/div[1]/div[1]/div[2]/div[2]/section/main/div/ul/li[2]/a/span/span/span")).Text.Replace(" ", "");

            if (countStr != null && int.TryParse(countStr, out int count))
            {
                return count;
            }
            else
            {
                throw new Exception("Cannot find count of followers");
            }
        }

        public List<string> GetFollowersUsernames()
        {   
            var listUsers = new List<string>();

            _driver.Navigate().GoToUrl($"https://www.instagram.com/{_username}/followers/");
            Thread.Sleep(_period);

            Console.WriteLine();
            var countBefore = 0;
            var countAfter = 0;
            var repeat = 0;
            while (repeat < 3)
            {
                countBefore = countAfter;
                var js = (IJavaScriptExecutor)_driver;
                js.ExecuteScript("window.scrollTo(0,document.body.scrollHeight);");
                Thread.Sleep(2000);
                countAfter = _driver.FindElements(By.XPath("/html/body/div[2]/div/div/div[2]/div/div/div/div[1]/div[1]/div[2]/section/main/div[2]/div[1]/div/div")).Count;
                if (countAfter == countBefore)
                    repeat++;
                else
                    repeat = 0;
            }
            for (int i = 0; i < countAfter; i++)
                listUsers.Add(_driver.FindElements(By.XPath("/html/body/div[2]/div/div/div[2]/div/div/div/div[1]/div[1]/div[2]/section/main/div[2]/div[1]/div/div/div/div/div/div[2]/div/div/div/a/div/div/span"))[i].Text);
            return listUsers;
        }


    }
}

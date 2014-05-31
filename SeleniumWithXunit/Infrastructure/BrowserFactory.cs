using OpenQA.Selenium.Firefox;

namespace SeleniumWithXunit.Infrastructure
{
    public class BrowserFactory
    {
        private readonly object padlock;

        public BrowserFactory()
        {
            padlock = new object();
        }

        public FirefoxDriver Create()
        {
            //Firefox instance creation is not thread safe
            FirefoxDriver browser;
            lock (padlock)
            {
                browser = new FirefoxDriver();
            }

            browser.Manage().Window.Maximize();
            return browser;
        }
    }
}
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using OpenQA.Selenium.Firefox;

namespace SeleniumWithXunit.Infrastructure
{
    public class BrowserPool : IDisposable
    {
        private readonly BrowserFactory browserFactory;
        private readonly ConcurrentQueue<FirefoxDriver> pool;

        public BrowserPool(BrowserFactory browserFactory)
        {
            this.browserFactory = browserFactory;
            pool = new ConcurrentQueue<FirefoxDriver>();
        }

        public FirefoxDriver TakeOne()
        {
            FirefoxDriver browser;
            if (!pool.TryDequeue(out browser))
            {
                browser = browserFactory.Create();
            }

            return browser;
        }

        public void Return(FirefoxDriver browser)
        {
            try
            {
                ResetState(browser);
                pool.Enqueue(browser);
            }
            catch (Exception)
            {  
                CloseSafely(browser);
            }                        
        }

        public void Dispose()
        {
            foreach (var browser in pool)
            {                
                CloseSafely(browser);
            }

            foreach (var firefoxProcess in Process.GetProcessesByName("firefox"))
            {
                firefoxProcess.Kill();
            }
        }

        private static void ResetState(FirefoxDriver browser)
        {
            browser.Manage().Cookies.DeleteAllCookies();
            browser.Manage().Window.Maximize();
            browser.Navigate().GoToUrl("about:blank");
        }

        private void CloseSafely(FirefoxDriver browser)
        {
            try
            {
                browser.Close();
            }
            catch (Exception)
            {
            }
        }
    }
}
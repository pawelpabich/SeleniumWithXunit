using System;
using System.Drawing.Imaging;
using System.IO;
using Autofac;
using OpenQA.Selenium.Firefox;
using Xunit.Sdk;

namespace SeleniumWithXunit.Infrastructure
{
    public abstract class BaseTest : IDisposable, INeedToKnowTestFailure, INeedToKnowTestSuccess
    {
        private static readonly object padlock = new object();
        private static bool isTestRunSetup;

        public FirefoxDriver Browser { get; set; }

        protected BaseTest()
        {
            EnsureTestRunIsSetup();
            CurrentLifetimeScope = TestRun.Container.BeginLifetimeScope();
            CurrentLifetimeScope.InjectUnsetProperties(this);
        }

        private ILifetimeScope CurrentLifetimeScope { get; set; }

        public void Dispose()
        {
            CurrentLifetimeScope.Dispose();
        }

        public void Handle(TestFailed result)
        {
            TakeScreenshot(result.TestDisplayName);
        }

        private void TakeScreenshot(string testName)
        {
            var browserScreenShot = Browser.GetScreenshot();
            string pathToScreenShot = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DateTime.Now.ToString("HH_mm_ss_fff_") + testName + ".png");
            browserScreenShot.SaveAsFile(pathToScreenShot, ImageFormat.Png);
        }

        public void Handle(TestPassed result)
        {
            //All good
        }

        private static void EnsureTestRunIsSetup()
        {
            if (isTestRunSetup) return;

            lock (padlock)
            {
                if (isTestRunSetup) return;
                TestRun.Setup();
                isTestRunSetup = true;
            }
        }
    }
}
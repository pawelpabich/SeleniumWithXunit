using OpenQA.Selenium.Firefox;
using SeleniumWithXunit.Infrastructure;

namespace SeleniumWithXunit.Pages
{
    public class GitHubProjectMainPage : IPageObject
    {
        public FirefoxDriver Browser { get; set; }

        public void NavigateToSelf()
        {
            Browser.Navigate().GoToUrl("https://github.com/pawelpabich/SeleniumWithXunit");
        }

        public int GetNumberOfCommits()
        {
            var text = Browser.FindElementByCssSelector(".commits .num").Text;
            return int.Parse(text.Trim());
        }

        public string GetDescription()
        {
            return Browser.FindElementByCssSelector(".repository-description").Text;
        }
    }
}
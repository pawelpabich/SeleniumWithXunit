using SeleniumWithXunit.Infrastructure;
using SeleniumWithXunit.Pages;
using Shouldly;
using Xunit;

namespace SeleniumWithXunit
{   
    public class GitHubTests : BaseTest
    {
        public GitHubProjectMainPage GitHubProjectMainPage { get; set; }

        [Fact]
        public void Project_should_have_some_commits()
        {
            GitHubProjectMainPage.NavigateToSelf();
            GitHubProjectMainPage.GetNumberOfCommits().ShouldBeGreaterThan(0);
        }

        [Fact]
        public void Project_should_have_description()
        {
            GitHubProjectMainPage.NavigateToSelf();
            GitHubProjectMainPage.GetDescription().ShouldNotBeNullOrEmpty();
        }
    }
}

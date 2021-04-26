using OnlineSearchAssistant.Domain;
using Xunit;

namespace OnlineSearchAssistant.Tests.UnitTests.Domain
{
    public class GoogleSearchStrategyTests
    {
        [Fact]
        public void GetHtmlDomSearchXPath_ShouldReturnFollowingXPath()
        {
            var expectedXPath = "//div[@id='search']//div[@class='g']//div[@class='r']/a";

            var sut = new GoogleSearchStrategy();

            Assert.Equal(expectedXPath, sut.GetHtmlDomSearchXPath());
        }

        [Fact]
        public void GetSearchPageUrl_ShouldContainGoogleInUrl()
        {
            var expectedTerm = "Google";

            var sut = new GoogleSearchStrategy();

            Assert.Contains(expectedTerm, sut.GetSearchPageUrl(1));
        }

        [Fact]
        public void GetSearchPageUrl_ShouldReturnNull_IfPageNoIsHigherThan10()
        {
            var sut = new GoogleSearchStrategy();

            Assert.Null(sut.GetSearchPageUrl(11));
        }

        [Fact]
        public void GetSearchPageUrl_ShouldReturnNull_IfPageNoIsLessThan1()
        {
            var sut = new GoogleSearchStrategy();

            Assert.Null(sut.GetSearchPageUrl(0));
        }

    }
}

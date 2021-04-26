using OnlineSearchAssistant.Domain;
using Xunit;

namespace OnlineSearchAssistant.Tests.UnitTests.Domain
{
    public class BingSearchStrategyTests
    {
        [Fact]
        public void GetHtmlDomSearchXPath_ShouldReturnFollowingXPath()
        {
            var expectedXPath = "//div[@id='b_content']//ol[@id='b_results']//li[@class='b_algo']/h2/a";

            var sut = new BingSearchStrategy();

            Assert.Equal(expectedXPath, sut.GetHtmlDomSearchXPath());
        }

        [Fact]
        public void GetSearchPageUrl_ShouldContainGoogleInUrl()
        {
            var expectedTerm = "Bing";

            var sut = new BingSearchStrategy();

            Assert.Contains(expectedTerm, sut.GetSearchPageUrl(1));
        }

        [Fact]
        public void GetSearchPageUrl_ShouldReturnNull_IfPageNoIsHigherThan10()
        {
            var sut = new BingSearchStrategy();

            Assert.Null(sut.GetSearchPageUrl(11));
        }

        [Fact]
        public void GetSearchPageUrl_ShouldReturnNull_IfPageNoIsLessThan1()
        {
            var sut = new BingSearchStrategy();

            Assert.Null(sut.GetSearchPageUrl(0));
        }

    }
}

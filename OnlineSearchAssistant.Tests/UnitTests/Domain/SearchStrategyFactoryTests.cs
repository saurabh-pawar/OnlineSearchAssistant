using OnlineSearchAssistant.Domain;
using Xunit;

namespace OnlineSearchAssistant.Tests.UnitTests.Domain
{
    public class SearchStrategyFactoryTests
    {
        [Fact]
        public void SearchStrategyFactoryGetStrategy_GivenInputGoogle_ShouldReturnGoogleStrategy()
        {
            var sut = new SearchStrategyFactory();

            var strategy = sut.GetStrategy(SearchEngines.GOOGLE);

            Assert.IsType<GoogleSearchStrategy>(strategy);
        }

        [Fact]
        public void SearchStrategyFactoryGetStrategy_GivenInputGoogle_ShouldReturnBingStrategy()
        {
            var sut = new SearchStrategyFactory();

            var strategy = sut.GetStrategy(SearchEngines.BING);

            Assert.IsType<BingSearchStrategy>(strategy);
        }
    }
}

using Moq;
using OnlineSearchAssistant.Domain;
using OnlineSearchAssistant.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace OnlineSearchAssistant.Tests.UnitTests.Domain
{
    public class HtmlSearchResultParserTests
    {
        [AutoMoqData]
        [Theory]
        public void GetPageRanks_CalledWithoutSettingStrategy_ShouldThrowAppException(string searchText, string expectedText, int maxResultsToSearch)
        {
            var htmlReaderMock = new Mock<IHtmlPageParser>();

            var sut = new HtmlSearchResultParser(htmlReaderMock.Object);

            Assert.ThrowsAsync<ApplicationException>(() => sut.GetPageRanks(searchText, expectedText, maxResultsToSearch));
        }

        [AutoMoqData]
        [Theory]
        public async Task GetPageRanks_IfFirstSearchPageUrlIsNull_ShouldReturnEmptyList(string searchText, string expectedText, int maxResultsToSearch)
        {
            var htmlReaderMock = new Mock<IHtmlPageParser>();
            var searchStrategyMock = new Mock<ISearchStrategy>();
            string nullString = null;

            searchStrategyMock.Setup(m => m.GetSearchPageUrl(It.IsAny<int>())).Returns(nullString);

            var sut = new HtmlSearchResultParser(htmlReaderMock.Object);
            sut.SetStrategy(searchStrategyMock.Object);

            var result = await sut.GetPageRanks(searchText, expectedText, maxResultsToSearch);

            Assert.Empty(result);
        }

        [AutoMoqData]
        [Theory]
        public async Task GetPageRanks_IfMaxResultsToSearchIsZero_ShouldReturnEmptyList(string searchText, string expectedText)
        {
            var htmlReaderMock = new Mock<IHtmlPageParser>();
            var searchStrategyMock = new Mock<ISearchStrategy>();
            string nullString = null;

            searchStrategyMock.Setup(m => m.GetSearchPageUrl(It.IsAny<int>())).Returns(nullString);

            var sut = new HtmlSearchResultParser(htmlReaderMock.Object);
            sut.SetStrategy(searchStrategyMock.Object);

            var result = await sut.GetPageRanks(searchText, expectedText, 0);

            Assert.Empty(result);
        }

        [AutoMoqData]
        [Theory]
        public async Task GetPageRanks_IfResultTextIsPresentOnPage_ShouldReturnCorrectPageRank(
            string searchText, string expectedText, string searchPageUrl, int maxResultsToSearch)
        {
            var htmlReaderMock = new Mock<IHtmlPageParser>();
            var searchStrategyMock = new Mock<ISearchStrategy>();
            string nullString = null;

            var urlList = new List<string> { "randomurl", expectedText, expectedText };

            searchStrategyMock.Setup(m => m.GetSearchPageUrl(1)).Returns(searchPageUrl);
            searchStrategyMock.Setup(m => m.GetSearchPageUrl(2)).Returns(nullString);

            htmlReaderMock.Setup(m => m.GetAttributeValues(searchPageUrl, It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(urlList);

            var sut = new HtmlSearchResultParser(htmlReaderMock.Object);
            sut.SetStrategy(searchStrategyMock.Object);

            var result = await sut.GetPageRanks(searchText, expectedText, maxResultsToSearch);

            Assert.Equal(2, result.First());
            Assert.Equal(3, result.Last());
        }

        [AutoMoqData]
        [Theory]
        public async Task GetPageRanks_IfResultTextIsPresentBeyondMaxResultsToSearch_ShouldNotReturnThatPageRank(
            string searchText, string expectedText, string searchPageUrl)
        {
            var htmlReaderMock = new Mock<IHtmlPageParser>();
            var searchStrategyMock = new Mock<ISearchStrategy>();
            string nullString = null;
            int maxResultsToSearch = 2;

            var urlList = new List<string> { "randomurl", expectedText, "randomurl", expectedText };

            searchStrategyMock.Setup(m => m.GetSearchPageUrl(1)).Returns(searchPageUrl);
            searchStrategyMock.Setup(m => m.GetSearchPageUrl(2)).Returns(nullString);

            htmlReaderMock.Setup(m => m.GetAttributeValues(searchPageUrl, It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(urlList);

            var sut = new HtmlSearchResultParser(htmlReaderMock.Object);
            sut.SetStrategy(searchStrategyMock.Object);

            var result = await sut.GetPageRanks(searchText, expectedText, maxResultsToSearch);

            Assert.Equal(2, result.First());
            Assert.Single(result);
        }
    }
}

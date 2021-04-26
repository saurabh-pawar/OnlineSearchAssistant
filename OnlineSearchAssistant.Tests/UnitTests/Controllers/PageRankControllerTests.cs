using Microsoft.AspNetCore.Server.IIS;
using Moq;
using OnlineSearchAssistant.Application;
using OnlineSearchAssistant.Controllers;
using OnlineSearchAssistant.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace OnlineSearchAssistant.Tests.UnitTests.Controllers
{
    public class PageRankControllerTests
    {

        [Fact]
        public async Task GooglePageRankAction_ShouldCallSearchAssistantGetPageRanks_With50MaxResultsToSearch_AND_GoogleSearchEngine()
        {
            var inputSanitizerMock = new Mock<IInputSanitizer>();
            var searchAssistantMock = new Mock<ISearchAssistant>();

            inputSanitizerMock.Setup(m => m.IsAbsoluteUrl(It.IsAny<string>())).Returns(true);
            searchAssistantMock.Setup(m => m.GetPageRanks(It.IsAny<string>(), It.IsAny<string>(), 50, SearchEngines.GOOGLE)).ReturnsAsync(new List<int>());

            var sut = new PageRanksController(searchAssistantMock.Object, inputSanitizerMock.Object);

            await sut.Google(It.IsAny<string>(), It.IsAny<string>());

            searchAssistantMock.Verify(m => m.GetPageRanks(It.IsAny<string>(), It.IsAny<string>(), 50, SearchEngines.GOOGLE), Times.Once);
        }

        [Fact]
        public void GooglePageRankAction_IfUrlIsNotCorrectFormat_ShouldThrowBadRequest()
        {
            var inputSanitizerMock = new Mock<IInputSanitizer>();
            var searchAssistantMock = new Mock<ISearchAssistant>();

            inputSanitizerMock.Setup(m => m.IsAbsoluteUrl(It.IsAny<string>())).Returns(false);

            var sut = new PageRanksController(searchAssistantMock.Object, inputSanitizerMock.Object);

            Assert.ThrowsAsync<BadHttpRequestException>(() => sut.Google(It.IsAny<string>(), It.IsAny<string>()));
        }

        [Fact]
        public async Task BingPageRankAction_ShouldCallSearchAssistantGetPageRanks_With50MaxResultsToSearch_AND_BingSearchEngine()
        {
            var inputSanitizerMock = new Mock<IInputSanitizer>();
            var searchAssistantMock = new Mock<ISearchAssistant>();

            inputSanitizerMock.Setup(m => m.IsAbsoluteUrl(It.IsAny<string>())).Returns(true);
            searchAssistantMock.Setup(m => m.GetPageRanks(It.IsAny<string>(), It.IsAny<string>(), 50, SearchEngines.BING)).ReturnsAsync(new List<int>());

            var sut = new PageRanksController(searchAssistantMock.Object, inputSanitizerMock.Object);

            await sut.Bing(It.IsAny<string>(), It.IsAny<string>());

            searchAssistantMock.Verify(m => m.GetPageRanks(It.IsAny<string>(), It.IsAny<string>(), 50, SearchEngines.BING), Times.Once);
        }

        [Fact]
        public void BingPageRankAction_IfUrlIsNotCorrectFormat_ShouldThrowBadRequest()
        {
            var inputSanitizerMock = new Mock<IInputSanitizer>();
            var searchAssistantMock = new Mock<ISearchAssistant>();

            inputSanitizerMock.Setup(m => m.IsAbsoluteUrl(It.IsAny<string>())).Returns(false);

            var sut = new PageRanksController(searchAssistantMock.Object, inputSanitizerMock.Object);

            Assert.ThrowsAsync<BadHttpRequestException>(() => sut.Bing(It.IsAny<string>(), It.IsAny<string>()));
        }
    }
}

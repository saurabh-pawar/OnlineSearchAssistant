using OnlineSearchAssistant.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineSearchAssistant.Application
{
    public interface ISearchAssistant
    {
        public Task<List<int>> GetPageRanks(string searchText, string expectedText, int maxResultsToSearch, SearchEngines searchEngine);
    }

    public class OnlineSearchAssistant : ISearchAssistant
    {
        private readonly ISearchStrategyFactory searchStrategyFactory;
        private readonly ISearchResultParser searchResultParser;
        public OnlineSearchAssistant(ISearchStrategyFactory searchStrategyFactory, ISearchResultParser searchResultParser)
        {
            this.searchStrategyFactory = searchStrategyFactory;
            this.searchResultParser = searchResultParser;
        }

        public async Task<List<int>> GetPageRanks(string searchText, string expectedText, int maxResultsToSearch, SearchEngines searchEngine)
        {
            var strategy = searchStrategyFactory.GetStrategy(searchEngine);

            searchResultParser.SetStrategy(strategy);

            return await searchResultParser.GetPageRanks(searchText, expectedText, maxResultsToSearch);
        }
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineSearchAssistant.Domain
{
    public interface ISearchResultParser
    {
        public void SetStrategy(ISearchStrategy searchStrategy);
        public Task<List<int>> GetPageRanks(string searchText, string resultText, int maxResultsToSearch);
    }
}

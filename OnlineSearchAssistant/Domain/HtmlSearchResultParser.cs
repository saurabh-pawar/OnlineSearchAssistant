using OnlineSearchAssistant.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineSearchAssistant.Domain
{
    public class HtmlSearchResultParser : ISearchResultParser
    {
        private ISearchStrategy searchStrategy;
        private readonly IHtmlPageParser htmlReader;

        public HtmlSearchResultParser(IHtmlPageParser htmlReader)
        {
            this.htmlReader = htmlReader;
        }
        public void SetStrategy(ISearchStrategy searchStrategy)
        {
            this.searchStrategy = searchStrategy;
        }
        public async Task<List<int>> GetPageRanks(string searchString, string expectedText, int maxResultsToSearch)
        {
            if (searchStrategy == null)
            {
                throw new ApplicationException("Please set a strategy before getting page ranks.");
            }

            List<int> pageRanks = new List<int>();
            int currentRank = 1;
            int currentSearchPage = 1;

            while (!string.IsNullOrEmpty(searchStrategy.GetSearchPageUrl(currentSearchPage)) && currentRank <= maxResultsToSearch)
            {
                var searchResultLinks = await htmlReader.GetAttributeValues(
                    searchStrategy.GetSearchPageUrl(currentSearchPage),
                    searchStrategy.GetHtmlDomSearchXPath(),
                    "href");

                foreach (var link in searchResultLinks)
                {
                    if (link.Contains(expectedText))
                    {
                        pageRanks.Add(currentRank);
                    }

                    currentRank++;

                    if (currentRank > maxResultsToSearch)
                        break;
                }

                currentSearchPage++;
            }

            return pageRanks;
        }


    }
}

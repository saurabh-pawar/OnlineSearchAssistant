namespace OnlineSearchAssistant.Domain
{
    public interface ISearchStrategyFactory
    {
        public ISearchStrategy GetStrategy(SearchEngines searchEngine);
    }

    public class SearchStrategyFactory : ISearchStrategyFactory
    {
        public ISearchStrategy GetStrategy(SearchEngines searchEngine)
        {
            switch (searchEngine)
            {
                default:
                case SearchEngines.GOOGLE:
                    return new GoogleSearchStrategy();
                case SearchEngines.BING:
                    return new BingSearchStrategy();
            }
        }
    }
}

namespace OnlineSearchAssistant.Domain
{
    public interface ISearchStrategy
    {
        public string GetHtmlDomSearchXPath();

        public string GetSearchPageUrl(int pageNo);

    }
}

namespace OnlineSearchAssistant.Domain
{
    public class GoogleSearchStrategy : ISearchStrategy
    {
        private const int MaxPages = 10;

        public string GetHtmlDomSearchXPath()
        {
            return "//div[@id='search']//div[@class='g']//div[@class='r']/a";
        }

        public string GetSearchPageUrl(int pageNo)
        {
            if (pageNo < 1 || pageNo > MaxPages)
                return null;

            return $"https://infotrack-tests.infotrack.com.au/Google/Page{pageNo.ToString("d2")}.html";
        }
    }
}

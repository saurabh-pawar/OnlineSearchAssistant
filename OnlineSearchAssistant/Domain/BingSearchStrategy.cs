namespace OnlineSearchAssistant.Domain
{
    public class BingSearchStrategy : ISearchStrategy
    {
        private const int MaxPages = 10;
        public string GetHtmlDomSearchXPath()
        {
            return "//div[@id='b_content']//ol[@id='b_results']//li[@class='b_algo']/h2/a";
        }

        public string GetSearchPageUrl(int pageNo)
        {
            if (pageNo < 1 || pageNo > MaxPages)
                return null;

            return $"https://infotrack-tests.infotrack.com.au/Bing/Page{pageNo.ToString("d2")}.html";
        }
    }
}

using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineSearchAssistant.Infrastructure
{
    public interface IHtmlPageParser
    {
        public Task<List<string>> GetAttributeValues(string pageUrl, string elementXPath, string attributeName);
    }

    public class HtmlUtilityPackHtmlPageParser : IHtmlPageParser
    {
        public async Task<List<string>> GetAttributeValues(string pageUrl, string elementXPath, string attributeName)
        {
            HtmlWeb web = new HtmlWeb();

            var htmlDoc = await web.LoadFromWebAsync(pageUrl);

            var searchResultNodes = htmlDoc.DocumentNode.SelectNodes(elementXPath);

            var list = (from node in searchResultNodes
                        let requiredAttribute = node.Attributes.FirstOrDefault(a => a.Name == attributeName)
                        where requiredAttribute != null
                        select requiredAttribute.Value).ToList();

            return list;
        }

    }
}

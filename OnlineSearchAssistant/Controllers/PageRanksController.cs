using Microsoft.AspNetCore.Mvc;
using OnlineSearchAssistant.Application;
using OnlineSearchAssistant.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineSearchAssistant.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PageRanksController : Controller
    {
        private readonly IInputSanitizer inputSanitizer;
        private readonly ISearchAssistant searchAssistant;

        private const int MaxResultsToSearch = 50;

        public PageRanksController(ISearchAssistant searchAssistant, IInputSanitizer inputSanitizer)
        {
            this.searchAssistant = searchAssistant;
            this.inputSanitizer = inputSanitizer;
        }

        [HttpGet]
        [Route("Google")]
        public async Task<ActionResult<List<int>>> Google(string searchString, string urlToCheck)
        {
            if (!inputSanitizer.IsAbsoluteUrl(urlToCheck))
            {
                return BadRequest("Please provide a valid url to check");
            }

            var result = await searchAssistant.GetPageRanks(searchString, urlToCheck, MaxResultsToSearch, SearchEngines.GOOGLE);

            result = result.DefaultIfEmpty<int>(0).ToList();

            return Ok(result);
        }

        [HttpGet]
        [Route("Bing")]
        public async Task<ActionResult<List<int>>> Bing(string searchString, string urlToCheck)
        {
            if (!inputSanitizer.IsAbsoluteUrl(urlToCheck))
            {
                return BadRequest("Please provide a valid url to check");
            }

            var result = await searchAssistant.GetPageRanks(searchString, urlToCheck, MaxResultsToSearch, SearchEngines.BING);

            result = result.DefaultIfEmpty<int>(0).ToList();

            return Ok(result);
        }
    }
}
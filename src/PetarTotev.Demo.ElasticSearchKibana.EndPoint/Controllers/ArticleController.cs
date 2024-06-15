using Microsoft.AspNetCore.Mvc;
using PetarTotev.Demo.ElasticSearchKibana.EndPoint.Models;
using Serilog;

namespace PetarTotev.Demo.ElasticSearchKibana.EndPoint.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArticleController : ControllerBase
    {
        private readonly ILogger<ArticleController> _logger;
        private readonly Random _random = new();
        private readonly string[] _firstNames = ["Petar", "John", "Ivan", "Simeon", "Todor", "Vasil", "Dimitar"];
        private readonly string[] _lastNames = ["Totev", "Georgiev", "Ivanov", "Baykov", "Karakolev", "Kolev", "Todorov"];
        private readonly string[] _titles = ["Shogun", "Paragraph 22", "Lolita", "Tarzan", "Chess for Dummies", "The City", "Greek Mythology"];

        public ArticleController(ILogger<ArticleController> logger)
        {
            _logger = logger;
        }

        [HttpGet()]
        public IEnumerable<Article> Get()
        {
            var articles = new List<Article>();

            for (int i = 1; i <= _random.Next(2,6); i++)
            {
                if (i >= 3 && i <= 4)
                {
                    Log.Warning("Warning, too many writers ({i})!", i);
                }
                else if (i == 5)
                {
                    Log.Error("Error, WritersOverflow ({i})!", i);
                }

                var articleRandomized = new Article
                {
                    Author = _firstNames[_random.Next(0, _firstNames.Length)] + " " + _lastNames[_random.Next(0, _lastNames.Length)],
                    Title = _titles[_random.Next(0, _titles.Length)],
                    Year = _random.Next(1900, 2025)
                };

                articles.Add(articleRandomized);

                if (articleRandomized.Year > 2000)
                {
                    Log.Information("Created article {@Article} from this century!", articleRandomized);
                }
            }

            return articles;
        }
    }
}

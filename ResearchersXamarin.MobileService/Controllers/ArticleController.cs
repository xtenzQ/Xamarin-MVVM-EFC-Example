using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ResearchersXamarin.MobileService.DataContracts;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ResearchersXamarin.MobileService.Controllers
{
    [Route("api/[controller]")]
    public class ArticleController : Controller
    {
        [HttpGet("[action]/{researcherId}")]
        public IEnumerable<Article> GetArticleByResearcher(int researcherId)
        {
            var result = new List<Article>();
            foreach (var article in Business.Logic.ResearcherManager.Instance().GetResearcher(researcherId).Articles)
            {
                result.Add(new Article(article));
            }
            return result;
        }

        [HttpPost("[action]/{researcherId}")]
        public int AddArticle(int researcherId, [FromBody]Article article)
        {
            return Business.Logic.ResearcherManager.Instance().GetResearcher(researcherId)
                .AddArticle(article.Name, article.MagazineName, article.ReleaseDate);
        }

        [HttpPut("[action]")]
        public void UpdateArticle([FromBody]Article article)
        {
            new Business.Logic.Article(article.Id).Researcher.UpdateArticle(new Business.Logic.Article(article.Id, article.Name, article.MagazineName, article.ReleaseDate));
        }

        [HttpDelete("[action]/{articleId}")]
        public void DeleteArticle(int articleId)
        {
            new Business.Logic.Article(articleId).Researcher.DeleteArticle(articleId);
        }

        [HttpGet("[action]/{articleId}")]
        public Article GetArticle(int articleId)
        {
            var article = new Business.Logic.Article(articleId);
            article.Refresh();
            return new Article(article);
        }
    }
}

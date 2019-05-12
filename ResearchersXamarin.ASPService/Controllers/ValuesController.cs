using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ResearchersXamarin.ASPService.DataContracts;
using ResearchersXamarin.Service.DataContracts;

namespace ResearchersXamarin.ASPService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<List<Article>> GetArticleByResearcher(int researcherId)
        {
            var result = new List<Article>();
            foreach (var article in Business.Logic.ResearcherManager.Instance().GetResearcher(researcherId).Articles)
            {
                result.Add(new Article(article));
            }

            return result;
        }

        [HttpPut("{researcherId, article}")]
        public int AddArticle(int researcherId, Article article)
        {
            return Business.Logic.ResearcherManager.Instance().GetResearcher(researcherId)
                .AddArticle(article.Name, article.MagazineName, article.ReleaseDate);
        }

        [HttpPut("{article}")]
        public void UpdateArticle(Article article)
        {
            new Business.Logic.Article(article.Id).Researcher.UpdateArticle(new Business.Logic.Article(article.Id, article.Name, article.MagazineName, article.ReleaseDate));
        }

        [HttpDelete("{articleId}")]
        public void DeleteArticle(int articleId)
        {
            new Business.Logic.Article(articleId).Researcher.DeleteArticle(articleId);
        }

        [HttpGet]
        public ActionResult<Article> GetArticle(int articleId)
        {
            var aricle = new Business.Logic.Article(articleId);
            aricle.Refresh();
            return new Article(aricle);
        }

        #region Monograph

        [HttpGet]
        public ActionResult<List<Monograph>> GetMonographByResearcher(int researcherId)
        {
            var result = new List<Monograph>();
            foreach (var monograph in Business.Logic.ResearcherManager.Instance().GetResearcher(researcherId).Monographs)
            {
                result.Add(new Monograph(monograph));
            }

            return result;
        }

        [HttpPut("{researcherId, monograph}")]
        public int AddMonograph(int researcherId, Monograph monograph)
        {
            return Business.Logic.ResearcherManager.Instance().GetResearcher(researcherId).AddMonograph(monograph.Name,
                monograph.CoauthorLastName, monograph.CoauthorFirstName, monograph.CoauthorMiddleName,
                monograph.ReleaseDate, monograph.PageCount);
        }

        [HttpPut("{monograph}")]
        public void UpdateMonograph(Monograph monograph)
        {
            new Business.Logic.Monograph(monograph.Id).Researcher.UpdateMonograph(
                new Business.Logic.Monograph(monograph.Id, monograph.Name, monograph.CoauthorLastName,
                    monograph.CoauthorFirstName, monograph.CoauthorMiddleName, monograph.ReleaseDate,
                    monograph.PageCount));
        }

        [HttpDelete("{monographId}")]
        public void DeleteMonograph(int monographId)
        {
            new Business.Logic.Monograph(monographId).Researcher.DeleteMonograph(monographId);
        }

        [HttpGet]
        public ActionResult<Monograph> GetMonograph(int monographId)
        {
            var monograph = new Business.Logic.Monograph(monographId);
            monograph.Refresh();
            return new Monograph(monograph);
        }

        #endregion

        #region Presentation

        [HttpGet]
        public ActionResult<List<Presentation>> GetPresentationByResearcher(int researcherId)
        {
            var result = new List<Presentation>();
            foreach (var presentation in Business.Logic.ResearcherManager.Instance().GetResearcher(researcherId).Presentations)
            {
                result.Add(new Presentation(presentation));
            }

            return result;
        }

        [HttpPut("{researcherId, presentation}")]
        public int AddPresentation(int researcherId, Presentation presentation)
        {
            return Business.Logic.ResearcherManager.Instance().GetResearcher(researcherId)
                .AddPresentation(presentation.Name, presentation.ConferenceName, presentation.PresentationDate);
        }

        [HttpPut("{presentation}")]
        public void UpdatePresentation(Presentation presentation)
        {
            new Business.Logic.Presentation(presentation.Id).Researcher.UpdatePresentation(
                new Business.Logic.Presentation(presentation.Id, presentation.Name, presentation.ConferenceName, presentation.PresentationDate));
        }

        [HttpDelete("{presentationId}")]
        public void DeletePresentation(int presentationId)
        {
            new Business.Logic.Presentation(presentationId).Researcher.DeletePresentation(presentationId);
        }

        [HttpGet]
        public ActionResult<Presentation> GetPresentation(int presentationId)
        {
            var presentation = new Business.Logic.Presentation(presentationId);
            presentation.Refresh();
            return new Presentation(presentation);
        }

        #endregion

        #region Report

        [HttpGet]
        public ActionResult<List<Report>> GetReportByResearcher(int researcherId)
        {
            var result = new List<Report>();
            foreach (var report in Business.Logic.ResearcherManager.Instance().GetResearcher(researcherId).Reports)
            {
                result.Add(new Report(report));
            }

            return result;
        }

        [HttpPut("{researcherId, report}")]
        public int AddReport(int researcherId, Report report)
        {
            return Business.Logic.ResearcherManager.Instance().GetResearcher(researcherId).AddReport(report.Name,
                report.RegisterNumber, report.ReleaseYear, report.PageCount);
        }

        [HttpPut("{report}")]
        public void UpdateReport(Report report)
        {
            new Business.Logic.Report(report.Id).Researcher.UpdateReport(new Business.Logic.Report(report.Id,
                report.Name, report.RegisterNumber, report.ReleaseYear, report.PageCount));
        }

        [HttpDelete("{reportId}")]
        public void DeleteReport(int reportId)
        {
            new Business.Logic.Report(reportId).Researcher.DeleteReport(reportId);
        }

        [HttpGet]
        public ActionResult<Report> GetReport(int reportId)
        {
            var report = new Business.Logic.Report(reportId);
            report.Refresh();
            return new Report(report);
        }

        #endregion

        #region Request

        [HttpGet]
        public ActionResult<int> GetPresentationRequest(DateTime dateTime)
        {
            return new Business.Logic.Request().GetPresentationRequest(dateTime);
        }

        [HttpGet]
        public ActionResult<int> GetReportRequest(int departmentNumber)
        {
            return new Business.Logic.Request().GetReportRequest(departmentNumber);
        }

        #endregion

        #region Researcher

        [HttpGet]
        public ActionResult<List<Researcher>> GetResearchers()
        {
            var result = new List<DataContracts.Researcher>();
            foreach (Business.Logic.Researcher researcher in Business.Logic.ResearcherManager.Instance().ResearcherList)
            {
                result.Add(new DataContracts.Researcher(researcher));
            }
            return result;
        }

        [HttpPut("{lastName, firstName, middleName, departmentNumber, age, academicDegree, position}")]
        public int AddResearcher(string lastName, string firstName, string middleName, int departmentNumber, int age, string academicDegree, string position)
        {
            return Business.Logic.ResearcherManager.Instance().AddResearcher(lastName, firstName, middleName, departmentNumber, age, academicDegree, position);
        }

        [HttpPut("{c}")]
        public void UpdateResearcher(DataContracts.Researcher c)
        {
            Business.Logic.ResearcherManager.Instance().UpdateResearcher(new Business.Logic.Researcher(c.ResearcherId, c.LastName, c.LastName, c.LastName,
                c.DepartmentNumber, c.Age, c.AcademicDegree, c.Position));
        }

        [HttpDelete("{researcherId}")]
        public void DeleteResearcher(int researcherId)
        {
            Business.Logic.ResearcherManager.Instance().DeleteResearcher(researcherId);
        }

        #endregion
    }
}

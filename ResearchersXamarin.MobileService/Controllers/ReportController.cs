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
    public class ReportController : Controller
    {
        [HttpGet("[action]/{researcherId}")]
        public List<Report> GetReportByResearcher(int researcherId)
        {
            var result = new List<Report>();
            foreach (var report in Business.Logic.ResearcherManager.Instance().GetResearcher(researcherId).Reports)
            {
                result.Add(new Report(report));
            }

            return result;
        }

        [HttpPost("[action]/{researcherId}")]
        public int AddReport(int researcherId, [FromBody]Report report)
        {
            return Business.Logic.ResearcherManager.Instance().GetResearcher(researcherId).AddReport(report.Name,
                report.RegisterNumber, report.ReleaseYear, report.PageCount);
        }

        [HttpPut("[action]")]
        public void UpdateReport([FromBody]Report report)
        {
            new Business.Logic.Report(report.Id).Researcher.UpdateReport(new Business.Logic.Report(report.Id,
                report.Name, report.RegisterNumber, report.ReleaseYear, report.PageCount));
        }

        [HttpDelete("[action]/{reportId}")]
        public void DeleteReport(int reportId)
        {
            new Business.Logic.Report(reportId).Researcher.DeleteReport(reportId);
        }

        [HttpGet("[action]/{reportId}")]
        public Report GetReport(int reportId)
        {
            var report = new Business.Logic.Report(reportId);
            report.Refresh();
            return new Report(report);
        }
    }
}

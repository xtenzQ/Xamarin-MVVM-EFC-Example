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
    public class MonographController : Controller
    {
        [HttpGet("[action]/{researcherId}")]
        public List<Monograph> GetMonographByResearcher(int researcherId)
        {
            var result = new List<Monograph>();
            foreach (var monograph in Business.Logic.ResearcherManager.Instance().GetResearcher(researcherId).Monographs)
            {
                result.Add(new Monograph(monograph));
            }

            return result;
        }

        [HttpPost("[action]/{researcherId}")]
        public int AddMonograph(int researcherId, [FromBody]Monograph monograph)
        {
            return Business.Logic.ResearcherManager.Instance().GetResearcher(researcherId).AddMonograph(monograph.Name,
                monograph.CoauthorLastName, monograph.CoauthorFirstName, monograph.CoauthorMiddleName,
                monograph.ReleaseDate, monograph.PageCount);
        }

        [HttpPut("[action]")]
        public void UpdateMonograph([FromBody]Monograph monograph)
        {
            new Business.Logic.Monograph(monograph.Id).Researcher.UpdateMonograph(
                new Business.Logic.Monograph(monograph.Id, monograph.Name, monograph.CoauthorLastName,
                    monograph.CoauthorFirstName, monograph.CoauthorMiddleName, monograph.ReleaseDate,
                    monograph.PageCount));
        }

        [HttpDelete("[action]/{monographId}")]
        public void DeleteMonograph(int monographId)
        {
            new Business.Logic.Monograph(monographId).Researcher.DeleteMonograph(monographId);
        }

        [HttpGet("[action]/{monographId}")]
        public Monograph GetMonograph(int monographId)
        {
            var monograph = new Business.Logic.Monograph(monographId);
            monograph.Refresh();
            return new Monograph(monograph);
        }
    }
}

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ResearchersXamarin.Business.Logic;
using Researcher = ResearchersXamarin.MobileService.DataContracts.Researcher;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ResearchersXamarin.MobileService.Controllers
{
    [Route("api/[controller]")]
    public class ResearchersController : ControllerBase
    {
        // GET: api/<controller>
        [HttpGet("[action]")]
        public IEnumerable<Researcher> GetResearchers()
        {
            var result = new List<Researcher>();
            foreach (Business.Logic.Researcher researcher in ResearcherManager.Instance().ResearcherList)
            {
                result.Add(new Researcher(researcher));
            }
            return result;
        }

        [HttpPost("[action]")]
        public int AddResearcher([FromBody]Researcher researcher)
        {
            return ResearcherManager.Instance().AddResearcher(researcher.LastName, researcher.FirstName, researcher.MiddleName, researcher.DepartmentNumber, researcher.Age, researcher.AcademicDegree, researcher.Position);
        }

        [HttpPut("[action]")]
        public void UpdateResearcher([FromBody]Researcher c)
        {
            ResearcherManager.Instance().UpdateResearcher(new Business.Logic.Researcher(c.ResearcherId, c.LastName, c.LastName, c.LastName,
                c.DepartmentNumber, c.Age, c.AcademicDegree, c.Position));
        }

        [HttpDelete("[action]/{researcherId}")]
        public void DeleteResearcher(int researcherId)
        {
            ResearcherManager.Instance().DeleteResearcher(researcherId);
        }
    }
}

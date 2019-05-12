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
    public class PresentationController : Controller
    {
        [HttpGet("[action]/{researcherId}")]
        public List<Presentation> GetPresentationByResearcher(int researcherId)
        {
            var result = new List<DataContracts.Presentation>();
            foreach (var presentation in Business.Logic.ResearcherManager.Instance().GetResearcher(researcherId).Presentations)
            {
                result.Add(new Presentation(presentation));
            }

            return result;
        }

        [HttpPost("[action]/{researcherId}")]
        public int AddPresentation(int researcherId, [FromBody]Presentation presentation)
        {
            return Business.Logic.ResearcherManager.Instance().GetResearcher(researcherId)
                .AddPresentation(presentation.Name, presentation.ConferenceName, presentation.PresentationDate);
        }

        [HttpPut("[action]")]
        public void UpdatePresentation([FromBody]Presentation presentation)
        {
            new Business.Logic.Presentation(presentation.Id).Researcher.UpdatePresentation(
                new Business.Logic.Presentation(presentation.Id, presentation.Name, presentation.ConferenceName, presentation.PresentationDate));
        }

        [HttpDelete("[action]/{presentationId}")]
        public void DeletePresentation(int presentationId)
        {
            new Business.Logic.Presentation(presentationId).Researcher.DeletePresentation(presentationId);
        }

        [HttpGet("[action]/{presentationId}")]
        public Presentation GetPresentation(int presentationId)
        {
            var presentation = new Business.Logic.Presentation(presentationId);
            presentation.Refresh();
            return new Presentation(presentation);
        }
    }
}

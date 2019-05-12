using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ResearchersXamarin.MobileService.Controllers
{
    [Route("api/[controller]")]
    public class RequestController : Controller
    {
        [HttpGet("[action]/{dateTime}")]
        public int GetPresentationRequest(DateTime dateTime)
        {
            return new Business.Logic.Request().GetPresentationRequest(dateTime);
        }

        [HttpGet("[action]/{departmentNumber}")]
        public int GetReportRequest(int departmentNumber)
        {
            return new Business.Logic.Request().GetReportRequest(departmentNumber);
        }
    }
}

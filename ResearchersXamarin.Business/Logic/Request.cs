using System;
using ResearchersXamarin.Data.Managers;

namespace ResearchersXamarin.Business.Logic
{
    public class Request
    {
        public int GetPresentationRequest(DateTime dateTime)
        {
            var manager = new FactoryManager();
            return manager.GetRequestManager().GetPresentation(dateTime);
        }

        public int GetReportRequest(int departmentNumber)
        {
            var manager = new FactoryManager();
            return manager.GetRequestManager().GetReport(departmentNumber);
        }
    }
}
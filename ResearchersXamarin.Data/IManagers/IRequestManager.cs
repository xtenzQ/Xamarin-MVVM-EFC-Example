using System;

namespace ResearchersXamarin.Data.IManagers
{
    public interface IRequestManager
    {
        int GetPresentation(DateTime dateTime);
        int GetReport(int departmentNumber);
    }
}
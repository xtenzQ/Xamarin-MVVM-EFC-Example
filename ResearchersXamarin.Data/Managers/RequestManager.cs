﻿using System;
using System.Linq;
using ResearchersXamarin.Data.IManagers;
using ResearchersXamarin.Data.Model;

namespace ResearchersXamarin.Data.Managers
{
    public class RequestManager : IRequestManager
    {
        public int GetPresentation(DateTime dateTime)
        {
            using (var context = new ResDbContext())
            {
                return context.Presentations.Count(i => i.PresentationDate < dateTime);
            }
        }

        public int GetReport(int departmentNumber)
        {
            using (var context = new ResDbContext())
            {
                return context.Reports.Where(i => i.Researcher.DepartmentNumber == departmentNumber)
                    .Sum(i => i.PageCount);
            }
        }
    }
}
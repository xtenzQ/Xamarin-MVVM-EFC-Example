using System;
using System.Collections.Generic;
using ResearchersXamarin.Data.Model;

namespace ResearchersXamarin.Data.IManagers
{
    public interface IPresentationManager
    {
        int Add(string name, string conferenceName, DateTime presentationDate, int researcherId);
        void Delete(int presentationId);
        void Update(int presentationId, string name, string conferenceName, DateTime presentationDate);
        List<Presentation> FindByResearcher(int researcherId);
        Presentation Find(int presentationId);
    }
}
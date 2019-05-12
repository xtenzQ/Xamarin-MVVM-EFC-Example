using System.Collections.ObjectModel;
using ResearchersXamarin.UI.Common;

namespace ResearchersXamarin.UI.ViewModel
{
    public class ResearcherDetailViewModel : ViewModelBase
    {
        public ResearcherViewModel SelectedResearcher;

        public string RName => $"{SelectedResearcher.LastName} {SelectedResearcher.FirstName} {SelectedResearcher.MiddleName}";

        public ResearcherDetailViewModel()
        {
            
        }

        private static ResearcherDetailViewModel _instance;

        public static ResearcherDetailViewModel Instance()
        {
            return _instance ?? (_instance = new ResearcherDetailViewModel());
        }
    }
}
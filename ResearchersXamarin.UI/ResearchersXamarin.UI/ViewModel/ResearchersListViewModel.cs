using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Windows.Input;
using ResearchersXamarin.UI.Common;
using ResearchersXamarin.UI.Interface;
using ResearchersXamarin.UI.Service;
using ResearchersXamarin.UI.View;
using Xamarin.Forms;

namespace ResearchersXamarin.UI.ViewModel
{
    public class ResearchersListViewModel : ViewModelBase
    {
        private static ResearchersListViewModel _instance;

        private ResearcherViewModel _selectedResearcher;

        private ObservableCollection<ResearcherViewModel> _researcherList;

        private ICommand _showAddCommand;

        private ICommand _itemTapped;

        private ICommand _showDetailsCommand;

        private INavigation _navigation;

        public INavigation Navigation
        {
            get => _navigation;
            set
            {
                if (Equals(value, _navigation)) return;
                _navigation = value;
            }
        }

        public ObservableCollection<ResearcherViewModel> ResearcherList
        {
            get => GetResearchers();
            set
            {
                if (Equals(value, _researcherList)) return;
                _researcherList = value;
                OnPropertyChanged();
            }
        }

        public ResearcherViewModel SelectedResearcher
        {
            get => _selectedResearcher;
            set
            {
                if (Equals(value, _selectedResearcher)) return;
                Constants.Researcher = value;
                _selectedResearcher = value;
                OnPropertyChanged();
            }
        }

        public ICommand ShowAddCommand
        {
            get {

                return _showAddCommand ?? 
                         (_showAddCommand = new CommandBase(i => ShowAddDialog(), i => Clickable(), this));
            }
        }

        public ICommand ShowDetailsCommand
        {
            get
            {
                return _showDetailsCommand ??
                       (_showDetailsCommand = new CommandBase(i => ShowDetails(), i => Clickable(), this));
            }
        }

        public ICommand ItemTapped
        {
            get
            {
                return _itemTapped ??
                       (_itemTapped = new CommandBase(i => Tapped(), i => Clickable(), this));
            }
        }

        private ResearchersListViewModel()
        {
            ResearcherList = GetResearchers();
        }

        public static ResearchersListViewModel Instance()
        {
            return _instance ?? (_instance = new ResearchersListViewModel());
        }

        internal ObservableCollection<ResearcherViewModel> GetResearchers()
        {
            if (_researcherList == null)
            {
                _researcherList = new ObservableCollection<ResearcherViewModel>();
            }
            _researcherList.Clear();

            var res = App.Client.GetAsync("Researchers/GetResearchers").Result;
            if (!res.IsSuccessStatusCode) return _researcherList;

            var researchers = res.Content.ReadAsAsync<IEnumerable<Model.Researcher>>();

            foreach (var researcher in researchers.Result)
            {
                var resVm = new ResearcherViewModel(researcher);
                _researcherList.Add(resVm);
            }

            return _researcherList;
        }

        private void Tapped()
        {
            Constants.Navigation.PushAsync(new ResearcherDetailView(SelectedResearcher));

        }

        private bool Clickable()
        {
            return true;
        }

        private void ShowAddDialog()
        {
            Navigation.PushAsync(new ResearcherView() {BindingContext = new ResearcherViewModel()});
        }

        private void ShowDetails()
        {

        }
    }
}
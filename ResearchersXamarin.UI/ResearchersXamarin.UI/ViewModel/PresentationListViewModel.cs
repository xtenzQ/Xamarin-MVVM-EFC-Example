using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using ResearchersXamarin.Business.Logic;
using ResearchersXamarin.UI.Common;
using ResearchersXamarin.UI.Interface;
using ResearchersXamarin.UI.View;
using Xamarin.Forms;

namespace ResearchersXamarin.UI.ViewModel
{
    public class PresentationListViewModel : ViewModelBase
    {
        private static PresentationListViewModel _instance;

        private ObservableCollection<PresentationViewModel> _presentationList;
        private ICommand _showAddCommand;

        private INavigation _navigation;

        private ICommand _refreshCommand;
        public ResearcherViewModel Researcher => ResearchersListViewModel.Instance().SelectedResearcher;

        public string RName => $"{Researcher.LastName} {Researcher.FirstName} {Researcher.MiddleName}";

        public INavigation Navigation
        {
            get => _navigation;
            set
            {
                if (Equals(value, _navigation)) return;
                _navigation = value;
            }
        }

        public ObservableCollection<PresentationViewModel> PresentationList
        {
            get => GetPresentations();
            set
            {
                if (Equals(value, _presentationList)) return;
                _presentationList = value;
                OnPropertyChanged();
            }
        }

        public ICommand ShowAddCommand
        {
            get
            {

                return _showAddCommand ??
                       (_showAddCommand = new CommandBase(i => ShowAddDialog(), i => Clickable(), this));
            }
        }

        public ICommand RefreshCommand
        {
            get
            {
                return _refreshCommand ??
                       (_refreshCommand = new CommandBase(i => Refresh(), i => Clickable(), this));
            }
        }

        private PresentationListViewModel()
        {
            PresentationList = GetPresentations();
        }

        public static PresentationListViewModel Instance()
        {
            return _instance ?? (_instance = new PresentationListViewModel());
        }

        internal ObservableCollection<PresentationViewModel> GetPresentations()
        {
            if (_presentationList == null)
            {
                _presentationList = new ObservableCollection<PresentationViewModel>();
            }
            _presentationList.Clear();

            var client = new HttpClient { BaseAddress = new Uri(Constants.Address) };
            var res = client.GetAsync($"Presentation/GetPresentationByResearcher/{Researcher.Id}").Result;

            if (!res.IsSuccessStatusCode) return _presentationList;
            var presentations = res.Content.ReadAsAsync<IEnumerable<Model.Presentation>>();

            foreach (var presentation in presentations.Result)
            {
                var presentationVm = new PresentationViewModel(presentation);
                _presentationList.Add(presentationVm);
            }

            return _presentationList;
        }

        private bool Clickable()
        {
            return true;
        }

        private void ShowAddDialog()
        {
            Navigation.PushAsync(new PresentationView() { BindingContext = new PresentationViewModel() { Mode = Mode.Add } });
        }

        private void Refresh()
        {
            PresentationList = GetPresentations();
        }

    }
}
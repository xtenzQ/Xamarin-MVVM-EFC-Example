using System;
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
    public class MonographListViewModel : ViewModelBase
    {
        private static MonographListViewModel _instance;

        private ObservableCollection<MonographViewModel> _monographList;
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

        private MonographListViewModel()
        {
            MonographList = GetMonographs();
        }

        public static MonographListViewModel Instance()
        {
            return _instance ?? (_instance = new MonographListViewModel());
        }

        public ObservableCollection<MonographViewModel> MonographList
        {
            get => GetMonographs();
            set
            {
                if (Equals(value, _monographList)) return;
                _monographList = value;
                OnPropertyChanged();
            }
        }

        internal ObservableCollection<MonographViewModel> GetMonographs()
        {
            if (_monographList == null)
            {
                _monographList = new ObservableCollection<MonographViewModel>();
            }
            _monographList.Clear();

            var res = App.Client.GetAsync($"Monograph/GetMonographByResearcher/{Researcher.Id}").Result;

            if (!res.IsSuccessStatusCode) return _monographList;
            var monographs = res.Content.ReadAsAsync<IEnumerable<Model.Monograph>>();
            foreach (var monograph in monographs.Result)
            {
                var monographVm = new MonographViewModel(monograph);
                _monographList.Add(monographVm);
            }

            return _monographList;
        }

        private bool Clickable()
        {
            return true;
        }

        private void ShowAddDialog()
        {
            Navigation.PushAsync(new MonographView() { BindingContext = new MonographViewModel() { Mode = Mode.Add } });
        }

        private void Refresh()
        {
            MonographList = GetMonographs();
        }
    }
}
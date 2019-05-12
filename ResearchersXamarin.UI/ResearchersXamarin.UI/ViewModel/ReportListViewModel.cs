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
    public class ReportListViewModel : ViewModelBase
    {
        private static ReportListViewModel _instance;

        private ObservableCollection<ReportViewModel> _reportList;
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

        public ObservableCollection<ReportViewModel> ReportList
        {
            get => GetReports();
            set
            {
                if (Equals(value, _reportList)) return;
                _reportList = value;
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

        private ReportListViewModel()
        {
            ReportList = GetReports();
        }

        public static ReportListViewModel Instance()
        {
            return _instance ?? (_instance = new ReportListViewModel());
        }

        internal ObservableCollection<ReportViewModel> GetReports()
        {
            if (_reportList == null)
            {
                _reportList = new ObservableCollection<ReportViewModel>();
            }
            _reportList.Clear();

            var res = App.Client.GetAsync($"Report/GetReportByResearcher/{Researcher.Id}").Result;

            if (!res.IsSuccessStatusCode) return _reportList;
            var reports = res.Content.ReadAsAsync<IEnumerable<Model.Report>>();

            foreach (var report in reports.Result)
            {
                var reportVm = new ReportViewModel(report);
                _reportList.Add(reportVm);
            }

            return _reportList;
        }

        private bool Clickable()
        {
            return true;
        }

        private void ShowAddDialog()
        {
            Navigation.PushAsync(new ReportView() { BindingContext = new ReportViewModel() { Mode = Mode.Add } });
        }

        private void Refresh()
        {
            ReportList = GetReports();
        }
    }
}
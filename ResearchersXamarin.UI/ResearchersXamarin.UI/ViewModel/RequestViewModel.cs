using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows.Input;
using ResearchersXamarin.UI.Common;

namespace ResearchersXamarin.UI.ViewModel
{
    public class RequestViewModel : ViewModelBase
    {
        public DateTime MyDate { get; set; }
        public int Department { get; set; }

        private int _presentations;
        private int _reports;

        public int Presentations
        {
            get => _presentations;
            set
            {
                if (value == _presentations) return;
                _presentations = value;
                OnPropertyChanged();
            }
        }

        public int Reports
        {
            get => _reports;
            set
            {
                if (value == _reports) return;
                _reports = value;
                OnPropertyChanged();
            }
        }

        private ICommand _getPresentationsCommand;
        private ICommand _getReportsCommand;

        public ICommand GetPresentationsCommand
        {
            get { return _getPresentationsCommand ?? (_getPresentationsCommand = new CommandBase(i => GetPresentations(), i => Clickable(), this)); }
        }

        public ICommand GetReportsCommand
        {
            get { return _getReportsCommand ?? (_getReportsCommand = new CommandBase(i => GetReports(), i => Clickable(), this)); }
        }

        private static RequestViewModel _instance;

        public static RequestViewModel Instance()
        {
            return _instance ?? (_instance = new RequestViewModel());
        }

        private bool Clickable() => true;

        private void GetPresentations()
        {
            var res = App.Client.GetAsync($"Request/GetPresentationRequest/{MyDate:dd-MM-yyyy}").Result;
            if (!res.IsSuccessStatusCode) return;
            Presentations = res.Content.ReadAsAsync<int>().Result;
        }

        private void GetReports()
        {
            var res = App.Client.GetAsync($"Request/GetReportRequest/{Department}").Result;
            if (!res.IsSuccessStatusCode) return;
            Reports = res.Content.ReadAsAsync<int>().Result;
        }
    }
}
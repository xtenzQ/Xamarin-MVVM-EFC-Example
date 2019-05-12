using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Windows.Input;
using ResearchersXamarin.UI.Common;
using ResearchersXamarin.UI.Model;
using ResearchersXamarin.UI.View;

namespace ResearchersXamarin.UI.ViewModel
{
    public class ReportViewModel : ViewModelBase, IDataErrorInfo
    {
        private string _name;
        private int _registerNumber;
        private int _releaseYear;
        private int _pageCount;

        private ICommand _showEditCommand;
        private ICommand _updateCommand;
        private ICommand _deleteCommand;
        private ICommand _cancelCommand;

        private ReportViewModel _originalValue;

        public ResearcherViewModel Researcher => ResearchersListViewModel.Instance().SelectedResearcher;

        public ReportListViewModel Container => ReportListViewModel.Instance();

        public ICommand ShowEditCommand
        {
            get { return _showEditCommand ?? (_showEditCommand = new CommandBase(i => ShowEditDialog(), i => Clickable(), this)); }
        }

        public ICommand UpdateCommand
        {
            get { return _updateCommand ?? (_updateCommand = new CommandBase(i => Update(), i => Clickable(), null)); }
        }

        public ICommand DeleteCommand
        {
            get { return _deleteCommand ?? (_deleteCommand = new CommandBase(i => Delete(), i => Clickable(), null)); }
        }

        public ICommand CancelCommand
        {
            get { return _cancelCommand ?? (_cancelCommand = new CommandBase(i => Undo(), i => Clickable(), null)); }
        }

        public Mode Mode { get; set; }   

        public int Id { get; set; }

        public string Name
        {
            get => _name;
            set
            {
                if (value == _name) return;
                _name = value;
                OnPropertyChanged();
            }
        }

        // Регистрационный номер
        public int RegisterNumber
        {
            get => _registerNumber;
            set
            {
                if (value == _registerNumber) return;
                _registerNumber = value;
                OnPropertyChanged();
            }
        }

        // Год выпуска
        public int ReleaseYear
        {
            get => _releaseYear;
            set
            {
                if (value == _releaseYear) return;
                _releaseYear = value;
                OnPropertyChanged();
            }
        }

        // Число страниц
        public int PageCount
        {
            get => _pageCount;
            set
            {
                if (value == _pageCount) return;
                _pageCount = value;
                OnPropertyChanged();
            }
        }

        internal ReportViewModel(Model.Report report)
        {
            Id = report.Id;
            Name = report.Name;
            RegisterNumber = report.RegisterNumber;
            ReleaseYear = report.ReleaseYear;
            PageCount = report.PageCount;
            _originalValue = (ReportViewModel) MemberwiseClone();
        }

        internal ReportViewModel() { }

        private void ShowEditDialog()
        {
            Mode = Mode.Edit;
            ReportListViewModel.Instance().Navigation.PushAsync(new ReportView() { BindingContext = this });
        }

        private void Update()
        {
            if (Mode == Mode.Add)
            {
                App.Client.PostAsJsonAsync($"Report/AddReport/{Researcher.Id}", 
                    new Report(0, Name, RegisterNumber, ReleaseYear, PageCount));
            }
            else if (Mode == Mode.Edit)
            {
                App.Client.PutAsJsonAsync("Report/UpdateReport", new Report
                {
                    Id = Id,
                    Name = Name,
                    PageCount = PageCount,
                    RegisterNumber = RegisterNumber,
                    ReleaseYear = ReleaseYear
                });
                _originalValue = (ReportViewModel) MemberwiseClone();
            }
            Container.ReportList = Container.GetReports();
        }

        private void Delete()
        {
            App.Client.DeleteAsync($"Report/DeleteReport/{Id}");
            Container.ReportList = Container.GetReports();
        }

        private void Undo()
        {
            if (Mode == Mode.Edit)
            {
                Name = _originalValue.Name;
                RegisterNumber = _originalValue.RegisterNumber;
                ReleaseYear = _originalValue.ReleaseYear;
                PageCount = _originalValue.PageCount;
            }
        }

        private bool Clickable() => true;

        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                switch (columnName)
                {
                    case nameof(Name):
                        if (string.IsNullOrEmpty(Name) || (Name.Length > 196))
                        {
                            error = "Длина названия отчета должна быть меньше 196 символов!";
                        }
                        break;
                    case nameof(RegisterNumber):
                        if (RegisterNumber < 1 && RegisterNumber > 1000)
                        {
                            error = "Регистрационный номер должен лежать в интервале от 0 до 1000!";
                        }
                        break;
                    case nameof(ReleaseYear):
                        if (ReleaseYear < 1900 && ReleaseYear > DateTime.Now.Year)
                        {
                            error = "Год должен быть не меньше 1900 и не больше текущей даты!";
                        }
                        break;
                    case nameof(PageCount):
                        if (PageCount < 1)
                        {
                            error = "Длина научного отчета должна быть не меньше 1!";
                        }
                        break;
                }
                return error;
            }
        }

        public string Error => string.Empty;
    }
}
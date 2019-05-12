using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Windows.Input;
using ResearchersXamarin.UI.Common;
using ResearchersXamarin.UI.Interface;
using ResearchersXamarin.UI.Model;
using ResearchersXamarin.UI.Service;
using ResearchersXamarin.UI.View;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Article = ResearchersXamarin.UI.Model.Article;

namespace ResearchersXamarin.UI.ViewModel
{
    public class ResearcherViewModel : ViewModelBase, IDataErrorInfo
    {
        private string _lastName;
        private string _firstName;
        private string _middleName;
        private int _departmentNumber;
        private int _age;
        private string _academicDegree;
        private string _position;

        private ObservableCollection<ArticleViewModel> _articles;
        private ObservableCollection<MonographViewModel> _monographs;
        private ObservableCollection<PresentationViewModel> _presentations;
        private ObservableCollection<ReportViewModel> _reports;

        private ICommand _showEditCommand;
        private ICommand _updateCommand;
        private ICommand _deleteCommand;
        private ICommand _cancelCommand;

        private ResearcherViewModel _originalValue;

        public Mode Mode { get; set; }

        public ICommand ShowEditCommand
        {
            get { return _showEditCommand ?? (_showEditCommand = new CommandBase(i => ShowEditDialog(), i => Clickable(), this)); }
        }

        public ICommand UpdateCommand
        {
            get { return _updateCommand ?? (_updateCommand = new CommandBase(i => Update(), i => Clickable(), this)); }
        }

        public ICommand DeleteCommand
        {
            get { return _deleteCommand ?? (_deleteCommand = new CommandBase(i => Delete(), i => Clickable(), this)); }
        }

        public ICommand CancelCommand
        {
            get { return _cancelCommand ?? (_cancelCommand = new CommandBase(i => Undo(), i => Clickable(), this)); }
        }

        public int Id { get; set; }

        // ФИО
        public string LastName
        {
            get => _lastName;
            set
            {
                if (value == _lastName) return;
                _lastName = value;
                OnPropertyChanged();
            }
        }

        public string FirstName
        {
            get => _firstName;
            set
            {
                if (value == _firstName) return;
                _firstName = value;
                OnPropertyChanged();
            }
        }

        public string MiddleName
        {
            get => _middleName;
            set
            {
                if (value == _middleName) return;
                _middleName = value;
                OnPropertyChanged();
            }
        }

        // Номер отдела
        public int DepartmentNumber
        {
            get => _departmentNumber;
            set
            {
                if (value == _departmentNumber) return;
                _departmentNumber = value;
                OnPropertyChanged();
            }
        }

        // Возраст
        public int Age
        {
            get => _age;
            set
            {
                if (value == _age) return;
                _age = value;
                OnPropertyChanged();
            }
        }

        // Ученая степень
        public string AcademicDegree
        {
            get => _academicDegree;
            set
            {
                if (value == _academicDegree) return;
                _academicDegree = value;
                OnPropertyChanged();
            }
        }

        // Должность
        public string Position
        {
            get => _position;
            set
            {
                if (value == _position) return;
                _position = value;
                OnPropertyChanged();
            }
        }

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

        public ResearchersListViewModel Container => ResearchersListViewModel.Instance();

        public ResearcherViewModel(Researcher researcher)
        {
            Id = researcher.ResearcherId;
            LastName = researcher.LastName;
            FirstName = researcher.FirstName;
            MiddleName = researcher.MiddleName;
            DepartmentNumber = researcher.DepartmentNumber;
            Age = researcher.Age;
            AcademicDegree = researcher.AcademicDegree;
            Position = researcher.Position;
            _originalValue = (ResearcherViewModel) MemberwiseClone();
        }

        internal  ResearcherViewModel() { }

        private bool Clickable()
        {
            return true;
        }

        private void ShowEditDialog()
        {
            Mode = Mode.Edit;
            ResearchersListViewModel.Instance().Navigation.PushAsync(new ResearcherView() { BindingContext = this });
        }

        private void Update()
        {            
            if (Mode == Mode.Add)
            {
                App.Client.PostAsJsonAsync("Researchers/AddResearcher/", 
                    new Researcher(0, LastName, FirstName, MiddleName,
                    DepartmentNumber, Age,
                    AcademicDegree, Position));
            }
            else if (Mode == Mode.Edit)
            {
                App.Client.PutAsJsonAsync("Researchers/UpdateResearcher/", new Researcher
                {
                    ResearcherId = Id,
                    LastName = LastName,
                    FirstName = FirstName,
                    MiddleName = MiddleName,
                    Age = Age,
                    DepartmentNumber = DepartmentNumber,
                    AcademicDegree = AcademicDegree,
                    Position = Position
                });                
                _originalValue = (ResearcherViewModel) MemberwiseClone();
            }
            Container.ResearcherList = Container.GetResearchers();
        }

        private void Delete()
        {
            App.Client.DeleteAsync($"Researchers/DeleteResearcher/{Id}");
            Container.ResearcherList = Container.GetResearchers();
        }

        private void Undo()
        {
            if (Mode != Mode.Edit) return;
            LastName = _originalValue.LastName;
            FirstName = _originalValue.FirstName;
            MiddleName = _originalValue.MiddleName;
            Age = _originalValue.Age;
            DepartmentNumber = _originalValue.DepartmentNumber;
            AcademicDegree = _originalValue.AcademicDegree;
            Position = _originalValue.Position;
        }

        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                switch (columnName)
                {
                    case nameof(LastName):
                        if (string.IsNullOrEmpty(LastName) || (LastName.Length > 196))
                        {
                            error = "Длина Вашей фамлилии должна быть меньше 196 символов!";
                        }
                        break;
                    case nameof(FirstName):
                        if (string.IsNullOrEmpty(FirstName) || FirstName.Length > 196)
                        {
                            error = "Длина Вашего имени должна быть меньше 196 символов!";
                        }
                        break;
                    case nameof(MiddleName):
                        if (string.IsNullOrEmpty(MiddleName) || MiddleName.Length > 196)
                        {
                            error = "Длина Вашего отчества должна быть меньше 196 символов!";
                        }
                        break;
                    case nameof(DepartmentNumber):
                        if (DepartmentNumber < 1 || DepartmentNumber > 1000)
                        {
                            error = "Номер отдела должен быть больше 1 и меньше 1000!";
                        }

                        break;
                    case nameof(Age):
                        if (Age < 1 || Age > 130)
                        {
                            error = "Возраст должен быть больше 0 и меньше 130";
                        }

                        break;
                    case nameof(AcademicDegree):
                        if (string.IsNullOrEmpty(AcademicDegree))
                        {
                            error = "Выберите степень!";
                        }

                        break;
                    case nameof(Position):
                        if (string.IsNullOrEmpty(Position) || Position.Length > 100)
                        {
                            error = "Длина должности должна быть меньше 100 символов!";
                        }

                        break;
                }
                return error;
            }
        }

        public string Error => string.Empty;
    }
}
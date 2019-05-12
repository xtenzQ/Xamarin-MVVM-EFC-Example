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
    public class MonographViewModel : ViewModelBase, IDataErrorInfo
    {
        public int Id { get; set; }
        private string _name;
        private string _coauthorLastName;
        private string _coauthorFirstName;
        private string _coauthorMiddleName;
        private int _releaseDate;
        private int _pageCount;

        private ICommand _showEditCommand;
        private ICommand _updateCommand;
        private ICommand _deleteCommand;
        private ICommand _cancelCommand;

        private MonographViewModel _originalValue;

        public ResearcherViewModel Researcher => ResearchersListViewModel.Instance().SelectedResearcher;

        public MonographListViewModel Container => MonographListViewModel.Instance();

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

        // ФИО соавтора
        public string CoauthorLastName
        {
            get => _coauthorLastName;
            set
            {
                if (value == _coauthorLastName) return;
                _coauthorLastName = value;
                OnPropertyChanged();
            }
        }

        public string CoauthorFirstName
        {
            get => _coauthorFirstName;
            set
            {
                if (value == _coauthorFirstName) return;
                _coauthorFirstName = value;
                OnPropertyChanged();
            }
        }

        public string CoauthorMiddleName
        {
            get => _coauthorMiddleName;
            set
            {
                if (value == _coauthorMiddleName) return;
                _coauthorMiddleName = value;
                OnPropertyChanged();
            }
        }

        // Год издания
        public int ReleaseDate
        {
            get => _releaseDate;
            set
            {
                if (value.Equals(_releaseDate)) return;
                _releaseDate = value;
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

        internal MonographViewModel(Model.Monograph monograph)
        {
            Id = monograph.Id;
            Name = monograph.Name;
            CoauthorLastName = monograph.CoauthorLastName;
            CoauthorFirstName = monograph.CoauthorFirstName;
            CoauthorMiddleName = monograph.CoauthorMiddleName;
            ReleaseDate = monograph.ReleaseDate;
            PageCount = monograph.PageCount;
            _originalValue = (MonographViewModel) MemberwiseClone();
        }

        internal MonographViewModel() { }

        private void ShowEditDialog()
        {
            Mode = Mode.Edit;
            MonographListViewModel.Instance().Navigation.PushAsync(new MonographView() { BindingContext = this });
        }

        private void Update()
        {
            if (Mode == Mode.Add)
            {
                App.Client.PostAsJsonAsync($"Monograph/AddMonograph/{Researcher.Id}",
                    new Monograph(0, Name, CoauthorLastName, CoauthorFirstName, CoauthorMiddleName, ReleaseDate,
                        PageCount));
            }
            else if (Mode == Mode.Edit)
            {
                App.Client.PutAsJsonAsync("Monograph/UpdateMonograph", new Monograph
                {
                    Id = Id,
                    CoauthorFirstName = CoauthorFirstName,
                    CoauthorMiddleName = CoauthorMiddleName,
                    CoauthorLastName = CoauthorLastName,
                    Name = Name,
                    PageCount = PageCount,
                    ReleaseDate = ReleaseDate
                });
                _originalValue = (MonographViewModel) MemberwiseClone();
            }
            Container.MonographList = Container.GetMonographs();
        }

        private void Delete()
        {
            App.Client.DeleteAsync($"Monograph/DeleteMonograph/{Id}");
            Container.MonographList = Container.GetMonographs();
        }

        private void Undo()
        {
            if (Mode != Mode.Edit) return;
            Name = _originalValue.Name;
            CoauthorLastName = _originalValue.CoauthorLastName;
            CoauthorFirstName = _originalValue.CoauthorFirstName;
            CoauthorMiddleName = _originalValue.CoauthorMiddleName;
            ReleaseDate = _originalValue.ReleaseDate;
            PageCount = _originalValue.PageCount;
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
                            error = "Длина названия монографии должна быть меньше 196 символов!";
                        }
                        break;
                    case nameof(CoauthorLastName):
                        if (string.IsNullOrEmpty(CoauthorLastName) || (CoauthorLastName.Length > 196))
                        {
                            error = "Длина фамлилии должна быть меньше 196 символов!";
                        }
                        break;
                    case nameof(CoauthorFirstName):
                        if (string.IsNullOrEmpty(CoauthorFirstName) || CoauthorFirstName.Length > 196)
                        {
                            error = "Длина имени должна быть меньше 196 символов!";
                        }
                        break;
                    case nameof(CoauthorMiddleName):
                        if (string.IsNullOrEmpty(CoauthorMiddleName) || CoauthorMiddleName.Length > 196)
                        {
                            error = "Длина Вашего отчества должна быть меньше 196 символов!";
                        }
                        break;
                    case nameof(ReleaseDate):
                        if (ReleaseDate < 1900 && ReleaseDate > DateTime.Now.Year)
                        {
                            error = "Год должен быть не меньше 1900 и не больше текущей даты!";
                        }
                        break;
                    case nameof(PageCount):
                        if (PageCount < 1)
                        {
                            error = "Длина монографии должна быть не меньше 1!";
                        }
                        break;
                }
                return error;
            }
        }

        public string Error => string.Empty;

    }
}
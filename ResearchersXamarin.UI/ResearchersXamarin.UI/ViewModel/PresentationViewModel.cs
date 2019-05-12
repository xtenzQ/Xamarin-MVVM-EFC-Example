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
    public class PresentationViewModel : ViewModelBase, IDataErrorInfo
    {
        private string _name;
        private string _conferenceName;
        private DateTime _presentationDate;

        private ICommand _showEditCommand;
        private ICommand _updateCommand;
        private ICommand _deleteCommand;
        private ICommand _cancelCommand;

        private PresentationViewModel _originalValue;

        public ResearcherViewModel Researcher => ResearchersListViewModel.Instance().SelectedResearcher;

        public PresentationListViewModel Container => PresentationListViewModel.Instance();

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

        // Название доклада
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

        // Название конференции
        public string ConferenceName
        {
            get => _conferenceName;
            set
            {
                if (value == _conferenceName) return;
                _conferenceName = value;
                OnPropertyChanged();
            }
        }

        // Дата выступления
        public DateTime PresentationDate
        {
            get => _presentationDate;
            set
            {
                if (value.Equals(_presentationDate)) return;
                _presentationDate = value;
                OnPropertyChanged();
            }
        }

        internal PresentationViewModel(Model.Presentation presentation)
        {
            Id = presentation.Id;
            Name = presentation.Name;
            ConferenceName = presentation.ConferenceName;
            PresentationDate = presentation.PresentationDate;
            _originalValue = (PresentationViewModel) MemberwiseClone();
        }

        internal PresentationViewModel() { }

        private void ShowEditDialog()
        {
            Mode = Mode.Edit;
            PresentationListViewModel.Instance().Navigation.PushAsync(new PresentationView() { BindingContext = this });
        }

        private void Update()
        {
            var client = new HttpClient { BaseAddress = new Uri(Constants.Address) };

            if (Mode == Mode.Add)
            {
                App.Client.PostAsJsonAsync($"Presentation/AddPresentation/{Researcher.Id}",
                    new Presentation(0, Name, ConferenceName, PresentationDate));
            }
            else if (Mode == Mode.Edit)
            {
                App.Client.PutAsJsonAsync("Presentation/UpdatePresentation", new Presentation
                {
                    Id = Id,
                    ConferenceName = ConferenceName,
                    Name = Name,
                    PresentationDate = PresentationDate
                });
                _originalValue = (PresentationViewModel) MemberwiseClone();
            }
            Container.PresentationList = Container.GetPresentations();
        }

        private void Delete()
        {
            App.Client.DeleteAsync($"Presentation/DeletePresentation/{Id}");
            Container.PresentationList = Container.GetPresentations();
        }

        private void Undo()
        {
            if (Mode == Mode.Edit)
            {
                Name = _originalValue.Name;
                ConferenceName = _originalValue.ConferenceName;
                PresentationDate = _originalValue.PresentationDate;
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
                            error = "Длина названия доклада должна быть меньше 196 символов!";
                        }
                        break;
                    case nameof(ConferenceName):
                        if (string.IsNullOrEmpty(ConferenceName) || (ConferenceName.Length > 196))
                        {
                            error = "Длина названия конференции должна быть меньше 196 символов!";
                        }
                        break;
                    case nameof(PresentationDate):
                        if (PresentationDate.Year < 1900 && PresentationDate > DateTime.Now)
                        {
                            error = "Год должен быть не меньше 1900 и не больше текущей даты!";
                        }
                        break;
                }
                return error;
            }
        }

        public string Error => string.Empty;
    }
}
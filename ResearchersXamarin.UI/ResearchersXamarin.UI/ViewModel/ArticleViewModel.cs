using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ResearchersXamarin.UI.Common;
using ResearchersXamarin.UI.Interface;
using ResearchersXamarin.UI.Model;
using ResearchersXamarin.UI.Service;
using ResearchersXamarin.UI.View;

namespace ResearchersXamarin.UI.ViewModel
{
    public class ArticleViewModel : ViewModelBase, IDataErrorInfo
    {
        public int Id { get; set; }
        private string _name;
        private string _magazineName;
        private DateTime _releaseDate;

        private ICommand _showEditCommand;
        private ICommand _updateCommand;
        private ICommand _deleteCommand;
        private ICommand _cancelCommand;

        private ArticleViewModel _originalValue;

        public ResearcherViewModel Researcher => ResearchersListViewModel.Instance().SelectedResearcher;

        public ArticleListViewModel Container => ArticleListViewModel.Instance();

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

        // Название статьи
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

        // Название журнала
        public string MagazineName
        {
            get => _magazineName;
            set
            {
                if (value == _magazineName) return;
                _magazineName = value;
                OnPropertyChanged();
            }
        }

        // Год и месяц издания
        public DateTime ReleaseDate
        {
            get => _releaseDate;
            set
            {
                if (value == _releaseDate) return;
                _releaseDate = value;
                OnPropertyChanged();
            }
        }

        internal ArticleViewModel(Article article)
        {
            Id = article.Id;
            Name = article.Name;
            MagazineName = article.MagazineName;
            ReleaseDate = article.ReleaseDate;
            _originalValue = (ArticleViewModel) MemberwiseClone();
        }

        internal ArticleViewModel() { }

        private void ShowEditDialog()
        {
            Mode = Mode.Edit;
            ArticleListViewModel.Instance().Navigation.PushAsync(new ArticleView() { BindingContext = this });
        }

        private void Update()
        {
            if (Mode == Mode.Add)
            {
                App.Client.PostAsJsonAsync($"Article/AddArticle/{Researcher.Id}",
                    new Article(0, Name, MagazineName, ReleaseDate));
            }
            else if (Mode == Mode.Edit)
            {
                App.Client.PutAsJsonAsync("Article/UpdateArticle/", new Article
                {
                    Id = Id,
                    MagazineName = MagazineName,
                    Name = Name,
                    ReleaseDate = ReleaseDate
                });
                _originalValue = (ArticleViewModel) MemberwiseClone();
            }
            Container.ArticleList = Container.GetArticles();
        }

        private void Delete()
        {
            App.Client.DeleteAsync($"Article/DeleteArticle/{Id}/");
            Container.ArticleList = Container.GetArticles();
        }

        private void Undo()
        {
            if (Mode != Mode.Edit) return;
            Name = _originalValue.Name;
            MagazineName = _originalValue.MagazineName;
            ReleaseDate = _originalValue.ReleaseDate;
        }

        private bool Clickable()
        {
            return true;
        }

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
                            error = "Длина названия статьи должна быть меньше 196 символов!";
                        }
                        break;
                    case nameof(MagazineName):
                        if (string.IsNullOrEmpty(MagazineName) || MagazineName.Length > 196)
                        {
                            error = "Длина незвания журнала должна быть меньше 196 символов!";
                        }
                        break;
                    case nameof(ReleaseDate):
                        if (ReleaseDate.Year < 1900 && ReleaseDate > DateTime.Now)
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
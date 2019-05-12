using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Windows.Input;
using ResearchersXamarin.Business.Logic;
using ResearchersXamarin.UI.Common;
using ResearchersXamarin.UI.Interface;
using ResearchersXamarin.UI.Service;
using ResearchersXamarin.UI.View;
using Xamarin.Forms;

namespace ResearchersXamarin.UI.ViewModel
{
    public class ArticleListViewModel : ViewModelBase
    {
        private static ArticleListViewModel _instance;

        private ObservableCollection<ArticleViewModel> _articleList;

        private ICommand _showAddCommand;

        private ICommand _refreshCommand;

        private INavigation _navigation;

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

        public ObservableCollection<ArticleViewModel> ArticleList
        {
            get => GetArticles();
            set
            {
                if (Equals(value, _articleList)) return;
                _articleList = value;
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

        private ArticleListViewModel()
        {
            ArticleList = GetArticles();
        }

        public static ArticleListViewModel Instance()
        {
            return _instance ?? (_instance = new ArticleListViewModel());
        }

        internal ObservableCollection<ArticleViewModel> GetArticles()
        {
            if (_articleList == null)
            {
                _articleList = new ObservableCollection<ArticleViewModel>();
            }
            _articleList.Clear();

            var res = App.Client.GetAsync($"Article/GetArticleByResearcher/{Researcher.Id}").Result;

            if (!res.IsSuccessStatusCode) return _articleList;
            var articles = res.Content.ReadAsAsync<IEnumerable<Model.Article>>();
            foreach (var article in articles.Result)
            {
                var articleVm = new ArticleViewModel(article);
                _articleList.Add(articleVm);
            }

            return _articleList;
        }

        private void Refresh()
        {
            ArticleList = GetArticles();
        }

        private bool Clickable() => true;

        private void ShowAddDialog()
        {
            //Navigation.PushAsync(new ResearcherView() {BindingContext = new ResearcherViewModel() {Mode = Mode.Add}});
            Navigation.PushAsync(new ArticleView() { BindingContext = new ArticleViewModel() { Mode = Mode.Add } });
        }

    }
}
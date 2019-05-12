using ResearchersXamarin.UI.Interface;
using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Xamarin.Forms;

namespace ResearchersXamarin.UI.View
{
    public class ResearcherViewDialog : IModalDialog
    {
        private ResearcherView _view;
        private INavigation _navigation;

        public void BindViewModel<TViewModel>(TViewModel viewModel)
        {
            GetPage().BindingContext = viewModel;
        }

        public void SetNavigation(INavigation navigation)
        {
            _navigation = navigation;
        }

        public void PushAsync()
        {
            _navigation.PushAsync(GetPage());
        }

        public void Close()
        {
            GetPage().Navigation.PopAsync();
        }

        private ResearcherView GetPage()
        {
            if (_view != null) return _view;
            _view = new ResearcherView();
            _view.Disappearing += view_Closed;
            return _view;
        }

        void view_Closed(object sender, EventArgs e)
        {
            _view = null;
        }
    }
}
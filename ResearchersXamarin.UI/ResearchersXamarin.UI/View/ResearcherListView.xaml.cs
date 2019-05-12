using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResearchersXamarin.UI.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ResearchersXamarin.UI.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResearcherListView : ContentPage
    {
        public ResearcherListView()
        {
            InitializeComponent();
            ResearchersListViewModel.Instance().Navigation = Navigation;
            BindingContext = ResearchersListViewModel.Instance();
        }

        private void AddResearcher(object sender, EventArgs e)
        {

        }

        private void EditResearcher(object sender, EventArgs e)
        {
            var researcherPage = new ResearcherView { BindingContext = new ResearcherViewModel { Mode = Mode.Edit } };
            Navigation.PushAsync(researcherPage);
        }

        private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            Navigation.PushAsync(new ResearcherDetailView(ResearchersListViewModel.Instance().SelectedResearcher));
        }
    }
}
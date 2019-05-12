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
    public partial class ResearcherDetailView : TabbedPage
    {
        public ResearcherDetailView(ResearcherViewModel researcher)
        {
            InitializeComponent();
            BindingContext = new ResearcherDetailViewModel() { SelectedResearcher = researcher };
        }

        private void MenuItem_OnClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}
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
    public partial class ReportListView : ContentPage
    {
        public ReportListView()
        {
            InitializeComponent();
            ReportListViewModel.Instance().Navigation = Navigation;
            BindingContext = ReportListViewModel.Instance();
        }
    }
}
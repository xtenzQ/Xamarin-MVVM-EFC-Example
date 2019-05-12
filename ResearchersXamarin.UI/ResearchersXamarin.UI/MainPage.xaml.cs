using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResearchersXamarin.UI.ViewModel;
using Xamarin.Forms;

namespace ResearchersXamarin.UI
{
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = ResearchersListViewModel.Instance();
        }
    }
}

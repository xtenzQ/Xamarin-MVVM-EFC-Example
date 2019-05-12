using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ResearchersXamarin.UI.Model;
using ResearchersXamarin.UI.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ResearchersXamarin.UI.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RequestView : ContentPage
    {
		public RequestView ()
		{
			InitializeComponent ();
            BindingContext = RequestViewModel.Instance();
        }
    }
}
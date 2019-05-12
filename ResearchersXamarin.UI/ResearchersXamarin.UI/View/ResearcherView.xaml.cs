using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ResearchersXamarin.UI.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ResearcherView : ContentPage
	{
		public ResearcherView ()
		{
			InitializeComponent ();
		}

        private void AddButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void CancelButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}
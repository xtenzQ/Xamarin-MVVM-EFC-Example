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
	public partial class MonographView : ContentPage
    {
		public MonographView ()
		{
			InitializeComponent();
		}


        private void btnAddMonograph_Click(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void btnEditMonograph_Click(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}
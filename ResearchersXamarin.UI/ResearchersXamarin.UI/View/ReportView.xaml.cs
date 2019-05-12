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
	public partial class ReportView : ContentPage
    {
		public ReportView ()
		{
			InitializeComponent ();
		}

        private void btnEditReport_Click(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void btnAddReport_Click(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ResearchersXamarin.UI.View;
using ResearchersXamarin.UI.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ResearchersXamarin.UI
{
    public partial class App : Application
    {
        public static HttpClient Client = new HttpClient() { BaseAddress = new Uri(Constants.Address) };
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new ResearcherListView());       
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

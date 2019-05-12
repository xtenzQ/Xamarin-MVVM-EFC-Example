using ResearchersXamarin.UI.Interface;
using ResearchersXamarin.UI.Service;
using ResearchersXamarin.UI.View;

namespace ResearchersXamarin.UI
{
    public class BootStrapper
    {
        public static void Initialize()
        {
            //initialize all the services needed for dependency injection
            //we use the unity framework for dependency injection, but you can choose others
            ServiceProvider.RegisterServiceLocator(new UnityServiceLocator());

            //register the IModalDialog using an instance of the CustomerViewDialog
            //this sets up the view
            ServiceProvider.Instance.Register<IModalDialog, ResearcherViewDialog>();
        }
    }
}
using ResearchersXamarin.UI.Interface;

namespace ResearchersXamarin.UI.Service
{
    public class ServiceProvider
    {
        public static IServiceLocator Instance { get; private set; }

        public static void RegisterServiceLocator(IServiceLocator s)
        {
            Instance = s;
        }
    }
}
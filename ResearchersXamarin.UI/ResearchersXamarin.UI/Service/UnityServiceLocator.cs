using ResearchersXamarin.UI.Interface;
using Unity;

namespace ResearchersXamarin.UI.Service
{
    public class UnityServiceLocator : IServiceLocator
    {
        private readonly UnityContainer _container;

        public UnityServiceLocator()
        {
            _container = new UnityContainer();
        }

        void IServiceLocator.Register<TInterface, TImplementation>()
        {
            _container.RegisterType<TInterface, TImplementation>();
        }

        TInterface IServiceLocator.Get<TInterface>()
        {
            return _container.Resolve<TInterface>();
        }
    }
}
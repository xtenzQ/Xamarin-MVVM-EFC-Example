using Xamarin.Forms;

namespace ResearchersXamarin.UI.Interface
{
    public interface IModalDialog
    {
        void BindViewModel<TViewModel>(TViewModel viewModel); //bind to viewModel

        void PushAsync();  //show the modal window 

        void SetNavigation(INavigation navigation);

        void Close();  //close the dialog   
    }
}
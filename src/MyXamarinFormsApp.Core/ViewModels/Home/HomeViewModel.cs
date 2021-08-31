using MvvmCross.Commands;
using MvvmCross.Navigation;

namespace MyXamarinFormsApp.Core.ViewModels.Home
{
    public class HomeViewModel : BaseViewModel
    {
        private IMvxNavigationService _navigationService;

        public MvxCommand NavigationCommand
        { get; private set; }
        public HomeViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
            NavigationCommand = new MvxCommand(navFunction);
        }

        private void navFunction()
        {
            _ = _navigationService.Navigate<DragDropModel>();
        }
    }
}

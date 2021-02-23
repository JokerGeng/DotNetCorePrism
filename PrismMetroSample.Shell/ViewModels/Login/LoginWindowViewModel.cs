using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using PrismMetroSample.Infrastructure;
using PrismMetroSample.Shell.Views;
using System.Threading;

namespace PrismMetroSample.Shell.ViewModels
{
    public class LoginWindowViewModel: BindableBase
    {
        #region Fields

        private readonly IRegionManager _regionManager;
        private readonly IDialogService _dialogService;

        #endregion

        #region Commands

        private DelegateCommand _loginLoadingCommand;
        public DelegateCommand LoginLoadingCommand =>
            _loginLoadingCommand ?? (_loginLoadingCommand = new DelegateCommand(ExecuteLoginLoadingCommand));

        #endregion

        #region  Excutes

        void ExecuteLoginLoadingCommand()
        {
            //window加载事件中导航到登录主界面
            //在LoginContentRegion区域导航到LoginMainContent页面
            _regionManager.RequestNavigate(RegionNames.LoginContentRegion, nameof(LoginMainContent), NavigationCompelted);
            IRegion region = _regionManager.Regions[RegionNames.LoginContentRegion];
            region.RequestNavigate(nameof(LoginMainContent), NavigationCompelted);
        }

        #endregion

        public LoginWindowViewModel(IRegionManager regionManager, IDialogService dialogService)
        {
            _regionManager = regionManager;
            _dialogService = dialogService;
        }

        private void NavigationCompelted(NavigationResult result)
        {
            if (result.Result == true)
            {
                Thread.Sleep(1000);
                _dialogService.ShowDialog("SuccessDialog", new DialogParameters($"message={"导航到LoginMainContent页面成功"}"), null);
            }
            else
            {
                _dialogService.Show("WarningDialog", new DialogParameters($"message={"导航到LoginMainContent页面失败"}"), null);
            }
        }
    }
}

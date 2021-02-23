using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using PrismMetroSample.Infrastructure;
using PrismMetroSample.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PrismMetroSample.Shell.ViewModels
{
    public class LoginMainContentViewModel : BindableBase,INavigationAware
    {
        private readonly IRegionManager _regionManager;

        private User _currentUser = new User();
        public User CurrentUser
        {
            get { return _currentUser; }
            set { SetProperty(ref _currentUser, value); }
        }

        private DelegateCommand _createAccountCommand;
        public DelegateCommand CreateAccountCommand =>
                _createAccountCommand ?? (_createAccountCommand = new DelegateCommand(ExecuteCreateAccountCommand));

        //导航到CreateAccount
        void ExecuteCreateAccountCommand()
        {
            Navigate("CreateAccount");
        }

        private void Navigate(string navigatePath)
        {
            if (navigatePath != null)
                _regionManager.RequestNavigate(RegionNames.LoginContentRegion, navigatePath);

        }

        //从其它页面导航到该页面时触发
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            MessageBox.Show("从CreateAccount导航到LoginMainContent");
            var loginId = navigationContext.Parameters["loginId"] as string;
            if (loginId != null)
            {
                this.CurrentUser = new User() { LoginId = loginId };
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;//为false时再次导航至该页面，将会重新生成一个view，viewmodel也是新的，旧数据将会丢失
        }

        //从该页面导航到其它页面时触发
        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            MessageBox.Show("退出了LoginMainContent");
        }

        public LoginMainContentViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

    }
}

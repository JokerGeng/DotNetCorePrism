using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using PrismMetroSample.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PrismMetroSample.Shell.ViewModels
{
    /*
    Prism提供NavigationParameters类以帮助指定和检索导航参数，在导航期间，可以通过访问以下方法来传递导航参数：

    1:INavigationAware接口的IsNavigationTarget，OnNavigatedFrom和OnNavigatedTo方法中IsNavigationTarget，OnNavigatedFrom和OnNavigatedTo中形参NavigationContext对象的NavigationParameters属性
    2:IConfirmNavigationRequest接口的ConfirmNavigationRequest形参NavigationContext对象的NavigationParameters属性
    3:区域导航的INavigateAsync接口的RequestNavigate方法赋值给其形参navigationParameters
    4:导航日志IRegionNavigationJournal接口CurrentEntry属性的NavigationParameters类型的Parameters属性(下面会介绍导航日志)
    这里我们CreateAccount页面注册完用户后询问是否需要用当前注册用户来作为登录LoginId，来演示传递导航参数
    */
    public class CreateAccountViewModel:BindableBase, INavigationAware,IConfirmNavigationRequest
    {
        private readonly IRegionManager _regionManager;
        private readonly IDialogService _dialogService;
        private IRegionNavigationJournal _journal;
        public CreateAccountViewModel(IRegionManager regionManager, IDialogService dialogService)
        {
            _regionManager = regionManager;
            _dialogService = dialogService;
        }

        #region Properties

        private string _registeredLoginId;
        public string RegisteredLoginId
        {
            get { return _registeredLoginId; }
            set { SetProperty(ref _registeredLoginId, value); }
        }

        public bool IsUseRequest { get; set; }

        #endregion

        private DelegateCommand _loginMainContentCommand;
        public DelegateCommand LoginMainContentCommand =>
            _loginMainContentCommand ?? (_loginMainContentCommand = new DelegateCommand(ExecuteLoginMainContentCommand));

        private DelegateCommand<object> _verityCommand;
        public DelegateCommand<object> VerityCommand =>
    _verityCommand ?? (_verityCommand = new DelegateCommand<object>(ExecuteVerityCommand));

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            MessageBox.Show("从LoginMainContent导航到CreateAccount");
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            MessageBox.Show("退出了CreateAccount");
        }

        void ExecuteLoginMainContentCommand()
        {
            Navigate("LoginMainContent");
        }

        //注册账号
        //void ExecuteVerityCommand(object parameter)
        //{
        //    if (!VerityRegister(parameter))
        //    {
        //        return;
        //    }
        //    this.IsUseRequest = true;
        //    var Title = string.Empty;
        //    _dialogService.ShowDialog("SuccessDialog", new DialogParameters($"message={"注册成功"}"), null);
        //   // _journal.GoBack();
        //}

        //注册账号
        void ExecuteVerityCommand(object parameter)
        {
            if (!VerityRegister(parameter))
            {
                return;
            }
            MessageBox.Show("注册成功!");
            LoginMainContentCommand.Execute();
        }

        private void Navigate(string navigatePath)
        {
            if (navigatePath != null)
                _regionManager.RequestNavigate(RegionNames.LoginContentRegion, navigatePath);
        }

        //导航前询问
        public void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            var result = false;
            if (MessageBox.Show("是否需要导航到LoginMainContent页面?", "Naviagte?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                //方式二页面直接传递参数
                navigationContext.Parameters.Add("loginId", RegisteredLoginId);
                result = true;
            }
            continuationCallback(result);
        }

        private bool VerityRegister(object parameter)
        {
            if (string.IsNullOrEmpty(this.RegisteredLoginId))
            {
                MessageBox.Show("LoginId 不能为空！");
                return false;
            }
            var passwords = parameter as Dictionary<string, PasswordBox>;
            var password = (passwords["Password"] as PasswordBox).Password;
            var confimPassword = (passwords["ConfirmPassword"] as PasswordBox).Password;
            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Password 不能为空！");
                return false;
            }
            if (string.IsNullOrEmpty(confimPassword))
            {
                MessageBox.Show("ConfirmPassword 不能为空！");
                return false;
            }
            if (password.Trim() != confimPassword.Trim())
            {
                MessageBox.Show("两次密码不一致");
                return false;
            }
            //Global.AllUsers.Add(new User()
            //{
            //    Id = Global.AllUsers.Max(t => t.Id) + 1,
            //    LoginId = this.RegisteredLoginId,
            //    PassWord = password
            //});
            return true;
        }
    }
}

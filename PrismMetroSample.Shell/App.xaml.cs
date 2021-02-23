using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using PrismMetroSample.Infratructure;
using PrismMetroSample.Shell.ViewModels;
using PrismMetroSample.Shell.Views;
using PrismMetroSample.Shell.Views.Dialog;
using System.Windows;

namespace PrismMetroSample.Shell
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<LoginWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //注册全局命令
            containerRegistry.RegisterSingleton<IApplicationCommands, ApplicationCommands>();

            //注册导航
            containerRegistry.RegisterForNavigation<CreateAccount>();
            containerRegistry.RegisterForNavigation<LoginMainContent>();

            //注册对话框
            containerRegistry.RegisterDialog<SuccessDialog, SuccessDialogViewModel>();
            containerRegistry.RegisterDialog<WarningDialog,WarningDialogViewModel>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            //将PatientModule模块设置为启动时加载
            moduleCatalog.AddModule<PatientModule.PatientModule>();

            var MedicineModuleType = typeof(MedicineModule.MedicineModule);

            //将MedicineModule模块设置为按需加载
            moduleCatalog.AddModule(new ModuleInfo()
            {
                ModuleName = MedicineModuleType.Name,
                ModuleType = MedicineModuleType.AssemblyQualifiedName,
                InitializationMode = InitializationMode.OnDemand
            });

        }
    }
}

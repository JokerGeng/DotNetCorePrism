using Prism.Commands;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PrismMetroSample.Shell.ViewModels
{
    public class ShellWindowViewModel:BindableBase
    {
        IModuleManager _moduleManager;
        public ShellWindowViewModel(IModuleManager moduleManager)
        {
            _moduleManager = moduleManager;
            LoadMedicineModuleCommand = new DelegateCommand(LoadMedicineModuleCommandExcute);
            _moduleManager.LoadModuleCompleted += _moduleManager_LoadModuleCompleted;
        }

        public DelegateCommand LoadMedicineModuleCommand { get; set; }

        void LoadMedicineModuleCommandExcute()
        {
            //手动加载模块
            _moduleManager.LoadModule("MedicineModule");
        }

        private void _moduleManager_LoadModuleCompleted(object sender, LoadModuleCompletedEventArgs e)
        {
            MessageBox.Show($"{e.ModuleInfo.ModuleName} loaded");
        }
    }
}

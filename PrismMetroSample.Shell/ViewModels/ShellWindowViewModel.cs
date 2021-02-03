using Prism.Commands;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using PrismMetroSample.Infrastructure;
using PrismMetroSample.Infratructure;
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
        private IApplicationCommands _applicationCommands;
        public ShellWindowViewModel(IModuleManager moduleManager, IApplicationCommands commands)
        {
            _moduleManager = moduleManager;
            LoadMedicineModuleCommand = new DelegateCommand(LoadMedicineModuleCommandExcute);
            LoadPatientDetailRegionCommand = new DelegateCommand(LoadPatientDetailRegionCommandExcute);
            _moduleManager.LoadModuleCompleted += _moduleManager_LoadModuleCompleted;
            this._applicationCommands = commands;
            this._applicationCommands.ShowCommand.RegisterCommand(LoadPatientDetailRegionCommand);
        }

        public DelegateCommand LoadMedicineModuleCommand { get; set; }
        public DelegateCommand LoadPatientDetailRegionCommand { get; set; }
        public IApplicationCommands ApplicationCommands
        {
            get { return _applicationCommands; }
            set { SetProperty(ref _applicationCommands, value); }
        }

        void LoadMedicineModuleCommandExcute()
        {
            //手动加载模块
            _moduleManager.LoadModule("MedicineModule");
        }

        private void _moduleManager_LoadModuleCompleted(object sender, LoadModuleCompletedEventArgs e)
        {
            MessageBox.Show($"{e.ModuleInfo.ModuleName} loaded");
        }

        void LoadPatientDetailRegionCommandExcute()
        {
            //_moduleManager.LoadModule(RegionNames.FlyoutRegion);
        }
    }
}

using Prism;
using Prism.Commands;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using PrismMetroSample.Infrastructure;
using PrismMetroSample.Infratructure;
using PrismMetroSample.MedicineModule.Views;
using PrismMetroSample.PatientModule.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PrismMetroSample.Shell.ViewModels
{
    public class ShellWindowViewModel:BindableBase,IActiveAware
    {
        IModuleManager _moduleManager;
        private IRegion _patientListRegion;
        private IRegion _medicineListRegion;
        private IRegionManager _regionManager;
        private PatientList _patientListView;
        private MedicineMainContent _medicineMainContentView;
        public ShellWindowViewModel(IModuleManager moduleManager,IRegionManager regionManager)
        {
            _moduleManager = moduleManager;
            _regionManager = regionManager;
            _moduleManager.LoadModuleCompleted += _moduleManager_LoadModuleCompleted;
        }

        private bool _isCanExcute = false;
        public bool IsCanExcute
        {
            get { return _isCanExcute; }
            set { SetProperty(ref _isCanExcute, value); }
        }

        #region Command

        private DelegateCommand _loadingCommand;
        public DelegateCommand LoadingCommand =>_loadingCommand ?? (_loadingCommand = new DelegateCommand(ExecuteLoadingCommand));

        private DelegateCommand _loadMedicineModuleCommand;
        public DelegateCommand LoadMedicineModuleCommand => _loadMedicineModuleCommand ?? (_loadMedicineModuleCommand = new DelegateCommand(ExecuteLoadMedicineModuleCommandExcute));

        private DelegateCommand _activePaientListCommand;
        public DelegateCommand ActivePaientListCommand => _activePaientListCommand ?? (_activePaientListCommand = new DelegateCommand(ExcuteActivePatientRegionCommand));

        private DelegateCommand _deactivePaientListCommand;
        public DelegateCommand DeactivePaientListCommand => _deactivePaientListCommand ?? (_deactivePaientListCommand = new DelegateCommand(ExecuteDeactivePaientListCommand));

        private DelegateCommand _activeMedicineListCommand;
        public DelegateCommand ActiveMedicineListCommand =>_activeMedicineListCommand ?? (_activeMedicineListCommand = new DelegateCommand(ExecuteActiveMedicineListCommand).ObservesCanExecute(() => IsCanExcute));

        private DelegateCommand _deactiveMedicineListCommand;

        public DelegateCommand DeactiveMedicineListCommand => _deactiveMedicineListCommand ?? (_deactiveMedicineListCommand = new DelegateCommand(ExecuteDeactiveMedicineListCommand).ObservesCanExecute(() => IsCanExcute));

        public event EventHandler IsActiveChanged;
        private bool _isActive;
        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                SetProperty(ref _isActive, value);
                if (_isActive)
                {
                    MessageBox.Show("视图被激活了");
                }
                else
                {
                    MessageBox.Show("视图失效了");
                }
                IsActiveChanged?.Invoke(this, new EventArgs());
            }
        }

        #endregion Command

        #region Excute

        void ExecuteLoadingCommand()
        {
            _patientListRegion = _regionManager.Regions[RegionNames.PatientListRegion];
            _medicineListRegion = _regionManager.Regions[RegionNames.MedicineMainContentRegion];

            //该段代码会添加新的视图到_patientListRegion中
            //_patientListView = ContainerLocator.Current.Resolve<PatientList>();
            //_patientListRegion.Add(_patientListView);

            _patientListView = (PatientList)_patientListRegion.Views.Where(t => t.GetType() == typeof(PatientList)).FirstOrDefault();
        }

        void ExecuteLoadMedicineModuleCommandExcute()
        {
            //手动加载模块
            _moduleManager.LoadModule("MedicineModule");
            _medicineMainContentView = (MedicineMainContent)_medicineListRegion.Views.Where(t => t.GetType() == typeof(MedicineMainContent)).FirstOrDefault();
            this.IsCanExcute = true;
        }


        private void ExcuteActivePatientRegionCommand()
        {
            _patientListRegion.Activate(_patientListView);
            IsActive = true;
        }

        private void ExecuteDeactivePaientListCommand()
        {
            _patientListRegion.Deactivate(_patientListView);
            IsActive = false;
        }

        void ExecuteActiveMedicineListCommand()
        {
            _medicineListRegion.Activate(_medicineMainContentView);
            IsActive = true;
        }

        void ExecuteDeactiveMedicineListCommand()
        {
            _medicineListRegion.Deactivate(_medicineMainContentView);
            IsActive = false;
        }

        #endregion Excute

        private void _moduleManager_LoadModuleCompleted(object sender, LoadModuleCompletedEventArgs e)
        {
            if (
                MessageBox.Show($"{e.ModuleInfo.ModuleName} loaded", "ModuleLoad", MessageBoxButton.YesNo, MessageBoxImage.Question)
            == MessageBoxResult.Yes)
            {
                //MessageBox.Show("Yes");
            }
            else
            {
                //MessageBox.Show("No");
            }
        }

    }
}

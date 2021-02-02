using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using PrismMetroSample.Infrastructure;
using PrismMetroSample.MedicineModule.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismMetroSample.MedicineModule
{
    public class MedicineModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();

            ///为指定区域指定页面
            regionManager.RegisterViewWithRegion(RegionNames.MedicineMainContentRegion, typeof(MedicineMainContent));
            regionManager.RegisterViewWithRegion(RegionNames.FlyoutRegion, typeof(SearchMedicine));
            regionManager.RegisterViewWithRegion(RegionNames.SHowSearchPatientRegion, typeof(ShowSearchPatient));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }
    }
}

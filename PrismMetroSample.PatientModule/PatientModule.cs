using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using PrismMetroSample.Infrastructure;
using PrismMetroSample.PatientModule.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismMetroSample.PatientModule
{
    public class PatientModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();

            ///为指定区域指定页面
            regionManager.RegisterViewWithRegion(RegionNames.PatientListRegion, typeof(PatientList));
            regionManager.RegisterViewWithRegion(RegionNames.FlyoutRegion, typeof(PatientDetail));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }
    }
}

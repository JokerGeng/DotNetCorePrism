using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using PrismMetroSample.Infrastructure;
using PrismMetroSample.Infrastructure.Models;
using PrismMetroSample.Infratructure;
using PrismMetroSample.Infratructure.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PrismMetroSample.PatientModule.ViewModels
{
    public class PatientListViewModel:BindableBase
    {
        IEventAggregator _ea;

        public PatientListViewModel(IEventAggregator ea, IApplicationCommands commands)
        {
            XDocument doc = XDocument.Load("PatientList.xml");

            var patients = from p in doc.Descendants("Patient") select p;
            foreach (var medicineElement in patients)
            {
                Patient patient = new Patient();
                patient.Id = int.Parse(medicineElement.Attribute("ID").Value);
                patient.Name = medicineElement.Attribute("Name").Value;
                patient.Age = int.Parse(medicineElement.Attribute("Age").Value);
                patient.Sex = medicineElement.Attribute("Sex").Value;
                patient.RoomNo = medicineElement.Attribute("RoomNo").Value;
                Patients.Add(patient);
            }

            _ea = ea;
            this.ApplicationCommands = commands;
        }

        private ObservableCollection<Patient> _allPatients = new ObservableCollection<Patient>();
        public ObservableCollection<Patient> Patients
        {
            get
            {
                return _allPatients;
            }
            set
            {
                _allPatients = value;
            }
        }

        private IApplicationCommands _applicationCommands;
        public IApplicationCommands ApplicationCommands
        {
            get { return _applicationCommands; }
            set { SetProperty(ref _applicationCommands, value); }
        }

        private DelegateCommand<Patient> _mouseDoubleClickCommand;
        public DelegateCommand<Patient> MouseDoubleClickCommand => _mouseDoubleClickCommand ?? (_mouseDoubleClickCommand = new DelegateCommand<Patient>(ExcuteMouseDoubleCLickCommand));

        void ExcuteMouseDoubleCLickCommand(Patient patient)
        {
            //打开窗体
            //this.ApplicationCommands.ShowCommand.Execute(RegionNames.FlyoutRegion);
            //发布消息
            _ea.GetEvent<PatientSentEvent>().Publish(patient);
        }

    }
}

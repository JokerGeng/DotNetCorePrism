using Prism.Events;
using Prism.Mvvm;
using PrismMetroSample.Infrastructure.Models;
using PrismMetroSample.Infratructure.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismMetroSample.PatientModule.ViewModels
{
    public class PatientDetailViewModel:BindableBase
    {
        IEventAggregator _ea;
        public PatientDetailViewModel(IEventAggregator ea)
        {
            _ea = ea;
            //订阅事件
            _ea.GetEvent<PatientSentEvent>().Subscribe(PatientMessageReceived);
        }

        private Patient _currentPatient;

        /// <summary>
        /// 当前病人
        /// </summary>
        public Patient CurrentPatient
        {
            get => _currentPatient;
            set
            {
                SetProperty(ref _currentPatient, value);
            }
        }

        private ObservableCollection<Medicine> _lstMedicnes;

        /// <summary>
        /// 当前病人的药物
        /// </summary>
        public ObservableCollection<Medicine> LstMedicnes
        {
            get { return _lstMedicnes; }
            set
            {
                SetProperty(ref _lstMedicnes, value);
            }
        }

        private void PatientMessageReceived(Patient patient)
        {
            this.CurrentPatient = patient;
            this.LstMedicnes = new ObservableCollection<Medicine>();
        }
    }
}

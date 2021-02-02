using Prism.Mvvm;
using PrismMetroSample.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PrismMetroSample.MedicineModule.ViewModels
{
    public class MedicineMainContentViewModel:BindableBase
    {
        private ObservableCollection<Medicine> _allMedicines = new ObservableCollection<Medicine>();
        public ObservableCollection<Medicine> Medicines
        {
            get
            {
                return _allMedicines;
            }
            set
            {
                _allMedicines = value;
            }
        }

        public MedicineMainContentViewModel()
        {
            XDocument doc = XDocument.Load("MedicineStorage.xml");

            var medicines = from p in doc.Descendants("Medicine") select p;
            foreach (var medicineElement in medicines)
            {
                Medicine medicine = new Medicine();
                medicine.Id = int.Parse(medicineElement.Attribute("ID").Value);
                medicine.Name = medicineElement.Attribute("Name").Value;
                medicine.Type = medicineElement.Attribute("Type").Value;
                medicine.Unit = medicineElement.Attribute("Unit").Value;
                Medicines.Add(medicine);
            }

        }
    }
}

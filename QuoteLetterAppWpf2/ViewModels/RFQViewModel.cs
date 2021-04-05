using ClassLibrary.Models;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteLetterAppWpf2
{
    public class RFQViewModel : ViewModelBase
    {
        public RFQWindow RFQWindow { get; set; } = new RFQWindow();
        public ObservableCollection<RFQModel> RFQs
        {
            get => GetValue<ObservableCollection<RFQModel>>();
            private set => SetValue(value);
        }
        public RFQModel _selectedRFQ { get; set; }
        public RFQModel SelectedRFQ 
        { 
            get { return _selectedRFQ; } 
            set 
            { 
                _selectedRFQ = value;
                Customer = value.Customer;
            } 
        }
        protected override void OnParameterChanged(object parameter)
        {
            base.OnParameterChanged(parameter);
            if (IsInDesignMode)
            {
                RFQs = new ObservableCollection<RFQModel>();
            }
            else
            {
                RFQs = new ObservableCollection<RFQModel>();

                if (parameter == null)
                {
                    RFQs.Add(new RFQModel());
                    SelectedRFQ = RFQs[0];
                    //RFQWindow.Show();
                }
                else
                {
                    RFQs.Add((RFQModel)parameter);
                    SelectedRFQ = RFQs[0];
                    RFQWindow = new RFQWindow((RFQModel)parameter);
                    //RFQWindow.Show();
                }
            }            
        }

        private string _customer;

        public string Customer
        {
            get { return _customer; }
            set 
            { 
                _customer = value;
                SelectedRFQ.Customer = value;
            }
        }

    }
}

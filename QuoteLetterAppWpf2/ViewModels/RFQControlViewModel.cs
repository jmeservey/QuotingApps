using ClassLibrary.Models;
using DevExpress.Mvvm;

namespace QuoteLetterAppWpf2
{
    public class RFQControlViewModel : ViewModelBase
    {
        public RFQModel SelectedRFQ { get; set; }

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

        protected override void OnParameterChanged(object parameter)
        {
            base.OnParameterChanged(parameter);
            SelectedRFQ = (RFQModel)parameter;
        }
    }
}

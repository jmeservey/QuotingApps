using ClassLibrary.Models;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;
using System.Windows;

namespace QuoteLetterAppWpf2
{
    public class MainWindowViewModel : ViewModelBase
    {
        public RFQViewModel RFQViewModel { get; private set; }
        public MainWindowViewModel()
        {
            if (IsInDesignMode)
            {
                RFQs = new ObservableCollection<RFQModel>();
            }
            else
            {
                RFQs = new ObservableCollection<RFQModel>();
                // Replace with Database function.
                RFQs.Add(new RFQModel("Test"));
            }
        }

        public void CreateRFQ()
        {
            var result = MessageBox.Show("Would you like to import SORT Form data to create this RFQ?", "Import SORT Data.", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                // -- Create an RFQ Window.
                RFQViewModel = new RFQViewModel();

                ((ISupportParameter)RFQViewModel).Parameter = null;

                // Import SORT Form.
                // -- Create an Excel interactions class.
                // -- Create a method inside the Excel interactions class that reads a SORT form.  Use ClosedXML library.

                // Open an RFQ Window populated with SORT data.
                // -- Create an RFQ Window constructor that accepts an RFQ Object as an argument.
            }
            else if (result == MessageBoxResult.No)
            {
                // Open a empty RFQ Window.
                // -- Create an RFQ Window construect that accepts no arguments.
            }
        }

        public void CopyRFQ()
        {
            if (SelectedRFQ == null)
            {
                MessageBox.Show("Please select an RFQ to Copy.");
                return;
            }

            RFQs.Add(new RFQModel(SelectedRFQ));
        }

        public void EditRFQ()
        {
            if (SelectedRFQ == null)
            {
                MessageBox.Show("Please select an RFQ to Edit.");
                return;
            }

            RFQViewModel = new RFQViewModel();

            ((ISupportParameter)RFQViewModel).Parameter = SelectedRFQ;
        }

        public ObservableCollection<RFQModel> RFQs
        {
            get => GetValue<ObservableCollection<RFQModel>>();
            private set => SetValue(value);
        }

        public RFQModel SelectedRFQ { get; set; } = null;
    }
}

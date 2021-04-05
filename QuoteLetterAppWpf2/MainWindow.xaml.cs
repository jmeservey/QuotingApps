using ClassLibrary;
using ClassLibrary.Models;
using ClassLibrary.Reports;
using DevExpress.Xpf.Core;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuoteLetterAppWpf2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ThemedWindow
    {
        ObservableCollection<RFQModel> DatabaseRFQList = new ObservableCollection<RFQModel>();
        ObservableCollection<RFQModel> RFQList = new ObservableCollection<RFQModel>(); // Should this be a class variable or a property?
        RFQModel SelectedRFQ = null;

        public MainWindow()
        {
            InitializeComponent();

            // -- Establish the final data structure of app.
            // -- Create tables in database.
            // -- Create database class.
            // -- Create method inside database class to grab RFQs from database and return a list of type RFQModel.
            // -- Write line to set RFQModels

            //DatabaseRFQList.Add(new RFQModel("Test"));

            //RFQList = DatabaseRFQList;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //RFQDatagrid.ItemsSource = RFQList; // Figure out how to setup notifications for when RFQ list changes as demonstrated by Tim Corey rather than have to refresh the list.
        }

        private void GenerateReportButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedRFQ == null)
            {
                MessageBox.Show("Please select an RFQ.");
                return;
            }

            ReportPrintTool printTool = new ReportPrintTool(new QuoteLetter2XtraReport(SelectedRFQ));
            printTool.Report.CreateDocument(true);
            printTool.ShowPreviewDialog();
        }

        private void CreateRFQButton_Click(object sender, RoutedEventArgs e)
        {
            //var result = MessageBox.Show("Would you like to import SORT Form data to create this RFQ?","Import SORT Data.", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

            //if (result == MessageBoxResult.Yes)
            //{
            //    // -- Create an RFQ Window.
            //    RFQWindow rfqWindow = new RFQWindow();

            //    rfqWindow.Show();

            //    // Import SORT Form.
            //    // -- Create an Excel interactions class.
            //    // -- Create a method inside the Excel interactions class that reads a SORT form.  Use ClosedXML library.

            //    // Open an RFQ Window populated with SORT data.
            //    // -- Create an RFQ Window constructor that accepts an RFQ Object as an argument.
            //}
            //else if(result == MessageBoxResult.No)
            //{
            //    // Open a empty RFQ Window.
            //    // -- Create an RFQ Window construect that accepts no arguments.
            //}
        }

        private void CopyRFQButton_Click(object sender, RoutedEventArgs e)
        {
            // -- Create a method to copy an RFQ.
        }

        private void EditRFQButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedRFQ == null)
            {
                MessageBox.Show("Please select an RFQ to Edit.");
                return;
            }

            RFQWindow rfqWindow = new RFQWindow(SelectedRFQ);

            //rfqWindow.Show();
            // -- Open an RFQ Window populated with the selected RFQ's data.
        }

        private void RFQDatagrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void PopupImageEditSettings_ConvertEditValue(DependencyObject sender, DevExpress.Xpf.Editors.ConvertEditValueEventArgs args)
        {
            //using (MemoryStream stream = new MemoryStream())
            //{
            //    JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            //    encoder.Frames.Add(BitmapFrame.Create((BitmapSource)args.ImageSource));  // Null value is received from arg.ImageSource when I try to open popup editor.
            //    encoder.Save(stream);
            //    args.EditValue = stream.ToArray();
            //    args.Handled = true;
            //}
        }

        private void RFQDatagrid_SelectedItemChanged(object sender, DevExpress.Xpf.Grid.SelectedItemChangedEventArgs e)
        {
            //SelectedRFQ = (RFQModel)RFQDatagrid.SelectedItem;
        }

        // -- Create a button for Quotator profiles.
        // -- Create a Window that displays a list of Quotator profiles.
        // -- Create a window that displays an individual Quotator profile.
    }
}

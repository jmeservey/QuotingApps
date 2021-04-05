using ClassLibrary.Reports;
using ClassLibrary.Models;
using DevExpress.XtraReports.UI;
using System.Windows;
using ClassLibrary;

namespace QuoteLetterAppWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GenerateReportButton_Click(object sender, RoutedEventArgs e)
        {
            //DevExpress.Xpf.Printing.PrintHelper.ShowPrintPreview(this, new QuoteLetterXtraReport());

            ReportPrintTool printTool = new ReportPrintTool(new QuoteLetter2XtraReport(new RFQModel()));


            printTool.Report.CreateDocument(true);
            printTool.ShowPreviewDialog();
        }
    }
}

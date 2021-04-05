using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using ClassLibrary.Models;
using DevExpress.Xpf.Core;

namespace QuoteLetterAppWpf2
{
    /// <summary>
    /// Interaction logic for RFQWindow.xaml
    /// </summary>
    public partial class RFQWindow : UserControl
    {
        ObservableCollection<RFQModel> RFQModels = new ObservableCollection<RFQModel>();
        RFQModel RFQModel;
        public RFQWindow()
        {
            InitializeComponent();
            RFQModel = new RFQModel();
        }

        public RFQWindow(RFQModel rfqModel)
        {
            InitializeComponent();
            RFQModel = rfqModel;
            RFQModels.Add(RFQModel);
            //RFQTreeView.ItemsSource = RFQModels;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}

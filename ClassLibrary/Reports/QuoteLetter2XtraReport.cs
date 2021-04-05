using ClassLibrary.Models;
using DevExpress.DataAccess.ObjectBinding;
using DevExpress.XtraReports.UI;
using System.Drawing;
using System.Reflection;

namespace ClassLibrary.Reports
{
    public partial class QuoteLetter2XtraReport : DevExpress.XtraReports.UI.XtraReport
    {
        private Color SMCBlue;
        public object this[string propertyName]
        {
            get
            {
                PropertyInfo property = GetType().GetProperty(propertyName);
                return property.GetValue(this, null);
            }
            set
            {
                PropertyInfo property = GetType().GetProperty(propertyName);
                property.SetValue(this, value, null);
            }
        }

        public QuoteLetter2XtraReport(RFQModel rfqModel)
        {
            InitializeComponent();

            objectDataSource1.DataSource = rfqModel;

            SMCBlue = Color.FromArgb(0, 80, 148);

            xrShape1.FillColor = SMCBlue;

            xrTableCell28.BackColor = SMCBlue;
            xrTableCell29.BackColor = SMCBlue;
            xrTableCell30.BackColor = SMCBlue;
            xrTableCell31.BackColor = SMCBlue;
            xrTableCell35.BackColor = SMCBlue;
            xrTableCell36.BackColor = SMCBlue;
            xrTableCell37.BackColor = SMCBlue;
            xrTableCell39.BackColor = SMCBlue;
            xrTableCell54.BackColor = SMCBlue;
            xrTableCell100.BackColor = SMCBlue;
            xrTableCell101.BackColor = SMCBlue;
            xrTableCell102.BackColor = SMCBlue;
            xrTableCell103.BackColor = SMCBlue;
            xrTableCell117.BackColor = SMCBlue;
            xrTableCell118.BackColor = SMCBlue;
            xrTableCell119.BackColor = SMCBlue;
            xrTableCell120.BackColor = SMCBlue;
            xrTableCell121.BackColor = SMCBlue;
            xrTableCell122.BackColor = SMCBlue;
        }

        private void xrSubreport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XtraReport reportSource = ((XRSubreport)sender).ReportSource;

            PartModel part = DetailReport.GetCurrentRow() as PartModel;

            ((ObjectDataSource)reportSource.DataSource).DataMember = "";
            ((ObjectDataSource)reportSource.DataSource).DataSource = part.QuotedQuantities;
        }
    }
}

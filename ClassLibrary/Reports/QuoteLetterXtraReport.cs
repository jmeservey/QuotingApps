using ClassLibrary.Models;
using DevExpress.DataAccess.ObjectBinding;
using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace ClassLibrary
{
    public partial class QuoteLetterXtraReport : DevExpress.XtraReports.UI.XtraReport
    {
        public QuoteLetterXtraReport()
        {
            InitializeComponent();
            QuoteModel quoteModel = new QuoteModel();
            xrSubreport1.BeforePrint += XrSubreport1_BeforePrint;
            objectDataSource1.DataSource = quoteModel;
        }

        private void XrSubreport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XtraReport reportSource = ((XRSubreport)sender).ReportSource;

            PartModel part = DetailReport.GetCurrentRow() as PartModel;

            ((ObjectDataSource)reportSource.DataSource).DataMember = "";
            ((ObjectDataSource)reportSource.DataSource).DataSource = part.QuotedQuantities;
        }

        public QuoteLetterXtraReport(QuoteModel quoteModel)
        {
            InitializeComponent();
            objectDataSource1.DataSource = quoteModel;
        }


    }
}

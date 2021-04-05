using ClassLibrary.Models;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassLibrary.DataAccess
{
    public class ExcelInteractions
    {
        /// <summary>
        /// Reads RFQ data from a SORT Form.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static void ReadSORTForm(RFQModel rfq, string filePath = @"\\s-fs1-smdrv\mydocs$\Joshua.Meservey\Scott Wolf\Quote Project\Copy of SORT-RFQ Form Rev 14.xlsm")
        {
            using (var wb = new XLWorkbook(filePath))
            {
                var ws = wb.Worksheet("SORT FORM");

                rfq.BDM = ws.Cell("O7").Value.ToString();
                rfq.Customer = ws.Cell("G9").Value.ToString();
                rfq.Project = ws.Cell("N9").Value.ToString();
            }

            Trace.WriteLine($"BDM: {rfq.BDM}");
        }

        public static void CreateToolQuoteWorkbook()
        {
            // Creates a Tool Quote workbook populated with RFQ data.
        }

        public static void CreateQuoteReviewWorkbook()
        {
            // Creates a Quote Review workbook populated with RFQ data.
        }
    }
}

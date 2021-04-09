using ClassLibrary.Models;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
                rfq.Program = ws.Cell("N9").Value.ToString();
                rfq.Project = ws.Cell("N11").Value.ToString();
            }

            Trace.WriteLine($"BDM: {rfq.BDM}");
        }

        public static void CreateToolQuoteWorkbook(RFQModel rfq, PartModel part, string destinationFolderPath = @"\\s-fs1-smdrv\mydocs$\Joshua.Meservey\Scott Wolf\Quoting App\")
        {
            string sourceFilePath = @"\\s-fs1-smdrv\mydocs$\Joshua.Meservey\Scott Wolf\Quoting App\Simple Quote Worksheet - 2020-12-11.xlsx";

            using (var wb = new XLWorkbook(sourceFilePath))
            {
                var ws = wb.Worksheet("RFQ Details");

                ws.Cell("C10").Value = rfq.Customer;
                ws.Cell("C11").Value = rfq.Project;
                ws.Cell("C12").Value = part.Description;
                ws.Cell("C13").Value = part.ItemNumber;
                ws.Cell("C14").Value = part.RevisionNumber;
                ws.Cell("C15").Value = part.Material;

                wb.SaveAs(destinationFolderPath);
            }

            ProcessStartInfo info = new ProcessStartInfo();

            info.FileName = destinationFolderPath;
            info.UseShellExecute = true;

            Process.Start(info);
        }

        public static void CreateQuoteReviewWorkbook()
        {
            // Creates a Quote Review workbook populated with RFQ data.
        }
    }
}

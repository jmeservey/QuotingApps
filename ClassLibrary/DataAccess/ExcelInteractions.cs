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

                // Part information.

                ws.Cell("C10").Value = rfq.Customer;
                ws.Cell("C11").Value = rfq.Project;
                ws.Cell("C12").Value = part.Description;
                ws.Cell("C13").Value = part.ItemNumber;
                ws.Cell("C14").Value = part.RevisionNumber;
                ws.Cell("C15").Value = part.Material;
                ws.Cell("C16").Value = part.Finish_Texture;
                ws.Cell("C17").Value = part.EstimatedAnnualVolumes;
                ws.Cell("D17").Value = part.PressSizeStr;

                // Tool information.

                ws.Cell("B20").Value = part.RJGSensorQty;
                ws.Cell("C20").Value = part.Cavitation;
                ws.Cell("D20").Value = part.ToolClass;

                ws.Cell("B22").Value = part.SideActionQty;
                ws.Cell("C22").Value = part.Gating;
                ws.Cell("D22").Value = part.Warranty;

                ws.Cell("B24").Value = part.SideActionType;
                ws.Cell("C24").Value = part.FeedSystem;
                ws.Cell("D24").Value = part.Spares;

                ws.Cell("B26").Value = part.EjectionType;
                ws.Cell("C26").Value = part.ManifoldDropsQty;
                ws.Cell("D26").Value = part.BaseMaterial;

                ws.Cell("B28").Value = part.AdditionalNotes;

                wb.SaveAs(destinationFolderPath);
            }

            ProcessStartInfo info = new ProcessStartInfo();

            info.FileName = destinationFolderPath;
            info.UseShellExecute = true;

            Process.Start(info);
        }

        public static PartModel ReadToolQuoteWorkbook(string filePath)
        {
            PartModel part = new PartModel();

            using (var wb = new XLWorkbook(filePath))
            {
                var ws1 = wb.Worksheet("Quote Worksheet");
                var ws2 = wb.Worksheet("Quote Letter");

                part.DesignCost = ws1.Cell("H18").GetValue<int>() * ws1.Cell("C88").GetValue<int>();  // Multiplies Design Hours by the Shop rate specified in cell C88 to determine design cost.
                part.ToolBuildCost = ws2.Cell("H44").GetValue<int>();
                part.SpareCost = ws2.Cell("K44").GetValue<int>();
                part.ManifoldCost = ws1.Cell("H5").GetValue<int>();
            }

            return part;
        }

        public static void CreateQuoteReviewWorkbook()
        {
            // Creates a Quote Review workbook populated with RFQ data.
        }
    }
}

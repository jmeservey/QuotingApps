using DevExpress.Mvvm;
using System;
using System.Collections.ObjectModel;

namespace ClassLibrary.Models
{
    public class RFQModel : BindableBase
    {
        public int ID { get; set; }
        public string RFQNumberStr 
        { 
            get 
            {
                if (RevisionNumber > 0)
                {
                    return $"{RFQNumber}-{RevisionNumber}"; 
                }
                else
                {
                    return $"{RFQNumber}";
                }
            } 
        } // Auto-generated.
        public int RFQNumber { get; set; }  // Auto-generated.
        public int RevisionNumber { get; set; } = 0; // Auto-generated.
        public string Name { get { return Project; } } // This is strictly for the TreeView control on the RFQView control.
        public string Customer { get; set; }
        public string CustomerLocation { get; set; }
        public string ManufacturingLocation { get; set; }
        public DateTime DateReceived { get; set; }
        public DateTime OriginalDueDateToBDM { get; set; }
        public DateTime AdjustedDueDate { get; set; }
        public DateTime ToSalesDate { get; set; }
        public int NumberOfParts { get; set; }
        public string Comments { get; set; }
        public string Program { get; set; }

        private string _project;
        public string Project { get { return _project; } set { _project = value; RaisePropertyChanged(() => Name); } }  // Isn't this really the RFQ Description?
        public string Quotator { get; set; }
        public string BDM { get; set; }
        public string Status { get; set; }
        public string SubmitType { get; set; }
        public int PercentComplete { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateDue { get; set; }
        public string QuoteFolderPath { get; set; }

        public ObservableCollection<PartModel> Parts { get; set; } = new ObservableCollection<PartModel>();

        public RFQModel()
        {

        }
        public RFQModel(int rfqNumber, int id)
        {
            ID = id;
            RFQNumber = rfqNumber;
            RevisionNumber = 0;
            DateCreated = DateTime.Today;
            DateDue = DateTime.Today.AddDays(14);
        }
        public RFQModel(string test)
        {
            ID = 1;
            DateCreated = DateTime.Today;
            DateDue = DateTime.Today.AddDays(14);
            //RFQNumberStr = "6001";
            RFQNumber = 6001;
            RevisionNumber = 0;
            Program = "Bottle";
            Project = "5429-Cap";
            //QuoteRevision = "1";

            Customer = "UBI";
            //CustomerStreetAddress = "577 CHIPETA WAY";
            //CustomerCityStateZIP = "SALT LAKE CITY, UT 84108";

            //CustomerContactName = "David Shuart";
            //CustomerContactPhone = "801-588-6573";
            //CustomerContactEmail = "David.Shuart@tevapharm.com";

            //SMCContactName = "Bob Bryne";
            //SMCContactPhone = "908-513-5208";
            //SMCContactEmail = "bob.bryne@smcltd.com";

            Quotator = "Scott Wolf";
            BDM = "Greg Frost";
            Status = "Quote Review";
            PercentComplete = 15;
            QuoteFolderPath = @"X:\TOOLROOM\QUOTE21\";

            Parts.Add(new PartModel() { ItemNumber = 1, Description = "PIBA, 24 mm Bottle Well", DrawingNumber = "NA", RevisionNumber = "NA", Material = "Lustran 348-000000 Natural", Packaging = "Bulk", Cavitation = 2, MoldBase = "Dedicated Base # 2", Warranty = "100,000 Shots, 2 Yrs", StartUpHours = 0, PartWeight = .025, RunnerWeight = .0023, MoldPrice = 36000, MoldLeadTime = 5, ValidationPrice = 45000 });
            Parts.Add(new PartModel() { ItemNumber = 2, Description = "Retainer, PIBA Well Valve", DrawingNumber = "NA", RevisionNumber = "NA", Material = "Wacker Silpuran 6610/40", Packaging = "Bulk", Cavitation = 1, MoldLeadTime = 4, MoldPrice = 35000 });
            Parts.Add(new PartModel() { ItemNumber = 3, Description = "Valve, 24mm PIBA Well", DrawingNumber = "NA", RevisionNumber = "NA", Material = "Petrothane NA 952-094", Packaging = "Bulk", Cavitation = 1, MoldLeadTime = 4, MoldPrice = 37000 });
            Parts.Add(new PartModel() { ItemNumber = 4, Description = "PIBA Assembly, 24mm", DrawingNumber = "NA", RevisionNumber = "NA", Material = "PIBA, 24mm Bottle Well", Packaging = "Labeled and Double Bagged Material Certs Included", Cavitation = 1, MoldLeadTime = 4, MoldPrice = 40000, Picture = PartModel.Base64ToByteArr("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") });

            Parts[0].QuotedQuantities.Add(new QuotedQuantityModel() { ID = 1, MOQ = 5000, EAU = 5000, Price = 1.71 });
            Parts[0].QuotedQuantities.Add(new QuotedQuantityModel() { ID = 2, MOQ = 6000, EAU = 6000, Price = 1.75 });

            Parts[1].QuotedQuantities.Add(new QuotedQuantityModel() { MOQ = 5000, EAU = 5000, Price = 3.00 });

            Parts[2].QuotedQuantities.Add(new QuotedQuantityModel() { MOQ = 5000, EAU = 5000, Price = 1.96 });

            Parts[3].QuotedQuantities.Add(new QuotedQuantityModel() { MOQ = 5000, EAU = 5000, Price = 7.81 });
        }

        public RFQModel(RFQModel rfq)
        {
            ID = rfq.ID + 1;
            DateCreated = rfq.DateCreated;
            DateDue = rfq.DateDue;
            RFQNumber = rfq.RFQNumber;
            RevisionNumber = rfq.RevisionNumber + 1;
            Project = rfq.Project;

            Customer = rfq.Customer;
            Quotator = rfq.Quotator;

            BDM = rfq.BDM;
            Status = rfq.Status;
            PercentComplete = rfq.PercentComplete;
            QuoteFolderPath = rfq.QuoteFolderPath;

            // Parts is an observable collection therefore it cannot use a lambda expression in place of a foreach loop.
            foreach (var part in rfq.Parts)
            {
                Parts.Add(new PartModel(part));
            }
        }
    }
}

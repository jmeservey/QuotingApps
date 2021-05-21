﻿using DevExpress.Mvvm;
using DevExpress.Utils.Base;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace ClassLibrary.Models
{
    public class PartModel : BindableBase
    {
        public int ID { get; set; }
        public int ItemNumber { get; set; }
        public string PartNumber { get; set; }
        public string Name // This only exists to display a caption in the treeview in the RFQ View.
        { 
            get { return Description; } 
            set 
            { 
                Description = value;
                RaisePropertyChanged(() => Description);
            }
        }
        public string QuoteNumber { get; set; }
        private string _description;
        public string Description 
        {
            get { return _description; } 
            set 
            { 
                SetProperty(ref _description, value, "Description");
                RaisePropertyChanged(() => Name);
            } 
        }
        public string Material { get; set; }
        public string Packaging { get; set; }

        public string PictureStr = "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";

        //private static readonly ImageConverter ImageConverter = new ImageConverter();

        public byte[] Picture { get; set; }

        //public byte[] Picture { get { return ImageToByteArray(picture); } set { picture = NullByteArrayCheck(value); } }  // ImageToByteArray(picture); Base64ToByteArr(PictureStr);

        private Image _image;              
        public Image Image 
        { 
            get 
            { 
                return NullByteArrayCheck(Picture); 
            } 
            set 
            {
                //_image = value;
                Picture = ImageToByteArray(value);
            } 
        }
        public string Finish_Texture { get; set; }  // Tool quote sheet.
        public int EstimatedAnnualVolumes { get; set; }  // Tool quote sheet.
        public int PressSize { get; set; }  // Tool quote sheet.
        public string PressSizeStr { get; set; }  // Tool quote sheet.
        public int RJGSensorQty { get; set; }  // Tool quote sheet.
        public string ToolClass { get; set; }  // Tool quote sheet.
        public int SideActionQty { get; set; }  // Tool quote sheet.
        public string SideActionType { get; set; }  // Tool quote sheet.
        public string DrawingNumber { get; set; }  // Quote letter. Can also be called Print Number depending on the customer.
        public string RevisionNumber { get; set; }  // Quote letter.
        public int Cavitation { get; set; }  // Quote letter & Tool Quote sheet.
        public string MoldBase { get; set; }  // Quote letter
        public string FeedSystem { get; set; }  // Quote letter & Tool Quote Sheet.
        public string Gating { get; set; }  // Is this the same thing as gate style on the tool quote sheet.
        public string Actions { get; set; }  // Quote letter.
        public string Warranty { get; set; }  // Quote letter & Tool Quote Sheet.
        public int DesignCost { get; set; }  // From Tool Quote Sheet.
        public int DesignMarkup { get; set; }  // Tooling Panel.
        public int DesignPrice { get; set; }  // Tooling Panel.
        public int ToolBuildCost { get; set; } // From Tool Quote Sheet.
        public int ToolBuildMarkup { get; set; }  // Tooling Panel.
        public int ToolBuildPrice { get; set; }  // Tooling panel.
        public int SpareCost { get; set; }  // From Tool Quote Sheet.
        public int SpareMarkup { get; set; } // Tooling panel.
        public int SparePrice { get; set; } // Tooling panel.
        public int ManifoldCost { get; set; } // From Tool Quote Sheet.
        public int ManifoldMarkup { get; set; }  // Tooling panel.
        public int ManifoldPrice { get; set; }  // Tooling panel.
        public int MiscCost { get; set; }  // From Tool Quote sheet.
        public int MiscMarkup { get; set; }  // Tooling panel.
        public int MiscPrice { get; set; }  // Tooling panel.
        public int MoldCost { get { return (DesignCost + ToolBuildCost + SpareCost + ManifoldCost + MiscCost); } } // 
        public int? MoldPrice { get; set; }  // Quote letter
        public int? MoldMarkup { get; set; }  // ToolingPanel
        public int? MoldLeadTime { get; set; }  // Quote letter
        public string Spares { get; set; }  // Tool quote sheet
        public int? SparesPrice { get; set; }  // Quote letter
        public int? SparesLeadTime { get; set; }  // Quote letter
        public string EjectionType { get; set; }  // Tool quote sheet
        public string ManifoldDropsQty { get; set; }  // Tool quote sheet
        public string BaseMaterial { get; set; }  // Tool Quote Sheet
        public string ValidationFAI { get; set; }  // Quote letter
        public string ValidationCTQ { get; set; }  // Quote letter
        public int? ValidationPrice { get; set; }  // Quote letter
        public int? ValidationLeadTime { get; set; }  // Quote letter
        public string Gages { get; set; }  // Quote letter
        public int? GagesPrice { get; set; }  // Quote letter
        public string AdditionalNotes { get; set; }  // Tool Quote sheet.
        public string ToolQuoteFilePath { get; set; }
        public int StartUpHours { get; set; } // Unit price panel
        public int NumberOfLaborers { get; set; }  // Unit price panel
        public int CycleTime { get; set; } // Unit Price Panel
        public int MaterialCost { get; set; } // Unit Price Panel
        public int ScrapPercent { get; set; }  // Unit Price Panel
        public int EfficiencyPercent { get; set; }  // Unit price panel
        public string ManufacturingLocation { get; set; }  // Unit price panel
        public double LaborRate { get; set; }  // Unit price panel
        public double MachineRate { get; set; } // Unit price panel
        public double StartUpRate { get; set; } // Unit price panel
        public double PartWeight { get; set; } // Unit price panel
        public double RunnerWeight { get; set; } // Unit price panel  This includes the sprue.
        public int Markup { get; set; } = 30; // Unit price panel.
        public int TotalPrice { get { return (MoldPrice ?? 0) + (SparesPrice ?? 0) + (ValidationPrice ?? 0) + (GagesPrice ?? 0); } }
        public List<QuotedQuantityModel> QuotedQuantities { get; set; } = new List<QuotedQuantityModel>();
        public List<PartSubcategory> Subcatagories { get; set; }

        public PartModel()
        {
            Subcatagories = InitializeSubcategories();
        }

        public PartModel(PartModel part)
        {
            ItemNumber = part.ItemNumber;
            Description = part.Description;
            DrawingNumber = part.DrawingNumber;
            RevisionNumber = part.RevisionNumber;
            Material = part.Material;
            Packaging = part.Packaging;
            Cavitation = part.Cavitation;
            MoldLeadTime = part.MoldLeadTime;
            MoldBase = part.MoldBase;
            MoldPrice = part.MoldPrice;
            Image = part.Image;
            ValidationPrice = part.ValidationPrice;
            Subcatagories = InitializeSubcategories();
            part.QuotedQuantities.ForEach(quotedQuantity => QuotedQuantities.Add(new QuotedQuantityModel(quotedQuantity)));
        }
        public PartModel(string description)
        {
            this.Description = description;
            Subcatagories = InitializeSubcategories();
        }
        public static byte[] ImageToByteArray(Image imageIn)
        {
            if (imageIn == null) return null;

            var image2 = new Bitmap(imageIn);

            MemoryStream ms = new MemoryStream();
            image2.Save(ms, System.Drawing.Imaging.ImageFormat.Png);  // System.Drawing.Imaging.ImageFormat.Bmp
            return ms.ToArray();



            //if (imageIn != null)
            //{
            //    MemoryStream ms = new MemoryStream();
            //    imageIn.Save(ms, imageIn.RawFormat);  // System.Drawing.Imaging.ImageFormat.Bmp
            //    return ms.ToArray();
            //}
            //else
            //{
            //    return (byte[])ImageConverter.ConvertTo(imageIn, typeof(byte[]));  // WPF does not like it when I try to do this and use databinding.
            //}

            //ImageConverter converter = new ImageConverter();
            //return (byte[])converter.ConvertTo(imageIn, typeof(byte[]));
        }

        public static Image ByteArrayToImage(byte[] byteArrayIn)
        {
            //MemoryStream ms = new MemoryStream(byteArrayIn);
            //Image returnImage = Image.FromStream(ms);
            //return returnImage;

            if (byteArrayIn.Length == 0)
            {
                return null;
            }

            ImageConverter converter = new ImageConverter();
            Image img = (Image)converter.ConvertFrom(byteArrayIn);
            return img;
        }

        public static Image NullByteArrayCheck(object obj)
        {
            if (!DBNull.Value.Equals(obj) && obj != null)
            {
                return ByteArrayToImage((byte[])obj);
            }
            else
            {
                return null;
            }
        }

        public static Image Base64ToImage(string base64Image)
        {
            using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(base64Image)))
            {
                Image image = Image.FromStream(ms, true);
                return image;
            }
        }

        public static byte[] Base64ToByteArr(string base64Image)
        {
            return Convert.FromBase64String(base64Image);
        }

        public List<PartSubcategory> InitializeSubcategories()
        {
            List<string> categories = new List<string>() { "Tooling", "Validation", "Unit Price" };
            List<PartSubcategory> subcategories = new List<PartSubcategory>();

            foreach (var item in categories)
            {
                subcategories.Add(new PartSubcategory(this, item));
            }

            return subcategories;
        }

        public class PartSubcategory
        {
            public PartModel Part { get; set; }

            public string Name { get; set; }

            public PartSubcategory(PartModel part, string subcategory)
            {
                Part = part;
                Name = subcategory;
            }
        }

    }

}

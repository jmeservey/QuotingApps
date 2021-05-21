using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Models
{
    public class QuotedQuantityModel : BindableBase
    {
        public int ID { get; set; } = 0;

        private int quantity;

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        private int cycleTime;

        public int CycleTime
        {
            get { return cycleTime; }
            set { cycleTime = value; CalculateProductionHours(); }
        }

        private int scrapPercent;

        public int ScrapPercent
        {
            get { return scrapPercent; }
            set { scrapPercent = value; CalculateProductionHours(); }
        }

        private int efficiencyPercent;

        public int EfficiencyPercent
        {
            get { return efficiencyPercent; }
            set { efficiencyPercent = value; CalculateProductionHours(); }
        }

        private int cavities;

        public int Cavities
        {
            get { return cavities; }
            set { cavities = value; CalculateProductionHours(); }
        }

        private double laborRate;

        public double LaborRate
        {
            get { return laborRate; }
            set 
            { 
                laborRate = value;
                CalculateCost();
            }
        }

        private double machineRate;

        public double MachineRate
        {
            get { return machineRate; }
            set 
            { 
                machineRate = value;
                CalculateCost();
            }
        }

        private double startupRate;

        public double StartupRate
        {
            get { return startupRate; }
            set { startupRate = value; }
        }

        public double productionHours { get; set; }
        public double ProductionHours 
        { 
            get 
            {
                return productionHours;
            }
            set
            {
                productionHours = value;
                CalculateTPH();
            }
        }

        private int moq;

        public int MOQ
        {
            get { return moq; }
            set 
            { 
                moq = value;
                CalculateProductionHours();
            }
        }
          // Minimum order quantity.
        public int EAU { get; set; }
        private int startupHours;

        public int StartupHours  // Stays at quote quantity level because this is dependent on quote quantity.
        {
            get 
            { 
                return startupHours; 
            }
            set 
            { 
                startupHours = value;
                CalculateTPH();
            }
        }
        private double tph { get; set; }
        public double TPH  // Stays at quote quantity level because start up and production hours are dependent on quote quantity.
        {
            get
            {
                return tph;
            }

            set
            {
                tph = value;
            }
        }
        public double Cost { get; set; }

        private int markup;

        public int Markup
        {
            get { return markup; }
            set 
            { 
                markup = value;
                CalculatePrice();
            }
        }

        public double Price { get; set; }

        public QuotedQuantityModel()
        {

        }
        public QuotedQuantityModel(PartModel part)
        {

        }
        public QuotedQuantityModel(QuotedQuantityModel quotedQuantity)
        {
            ID = quotedQuantity.ID;
            Quantity = quotedQuantity.Quantity;
            MOQ = quotedQuantity.MOQ;
            EAU = quotedQuantity.EAU;
            Cost = quotedQuantity.Cost;
            Price = quotedQuantity.Price;
        }
        public bool CanCalculateProductionHours
        {
            get
            {
                if (CycleTime != 0 && EfficiencyPercent != 0 && Cavities != 0)  // ScrapRate can be zero I think.
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        private void CalculateProductionHours()
        {
            if (CanCalculateProductionHours)
            {
                ProductionHours = MOQ / (double)(3600 / CycleTime) / (1 - ((double)ScrapPercent / 100)) / ((double)EfficiencyPercent / 100) / Cavities;
            }
            else
            {
                ProductionHours = 0;
            }

            RaisePropertyChanged(() => ProductionHours);
        }
        public bool CanCalculateTPH
        {
            get
            {
                if (CanCalculateProductionHours && StartupHours != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public void CalculateTPH()
        {
            if (CanCalculateTPH)
            {
                TPH = ProductionHours + (StartupHours * Math.Ceiling(ProductionHours / 168));
            }
            else
            {
                TPH = 0;
            }

            RaisePropertyChanged(() => TPH);
            CalculateCost();
        }
        public void CalculateCost()
        {
            Cost = (TPH * MachineRate) + (ProductionHours * LaborRate);  // Taken out per Kris + ((SetupOperatorQuantity * StartupRate) * StartupHours)
            CalculatePrice();
            RaisePropertyChanged(() => Cost);
        }
        private void CalculatePrice()
        {
            Price = Cost * (1 + ((double)Markup / 100));
            RaisePropertyChanged(() => Price);
        }
    }
}

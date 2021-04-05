using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Models
{
    public class QuotedQuantityModel
    {
        public int ID { get; set; } = 0;
        public int Quantity { get; set; }
        public int MOQ { get; set; }
        public int EAU { get; set; }
        public double Cost{ get; set; }
        public double Price { get; set; }

        public QuotedQuantityModel()
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
    }
}

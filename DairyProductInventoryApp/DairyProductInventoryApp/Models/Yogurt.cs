using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DairyProductInventoryApp.Models
{
    public class Yogurt : DairyProduct
    {
        public Yogurt(string name, string brand, string packaging, double fatContent,
            double weightOrVolume, DateTime expiryDate, decimal price)
            : base(name, brand, packaging, fatContent, weightOrVolume, expiryDate, price)
        {
        }

        public override string GetDescription()
        {
            return $"Йогурт - {base.GetDescription()}";
        }
    }
}

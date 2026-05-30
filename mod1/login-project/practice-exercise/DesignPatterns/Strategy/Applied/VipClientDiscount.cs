using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Strategy.Applied
{
    public class VipClientDiscount : IDiscountStrategy
    {
        public double CalculateDiscount(double price)
        {
            return price * 0.2; // 20% discount for VIP customers
        }
    
    }
}

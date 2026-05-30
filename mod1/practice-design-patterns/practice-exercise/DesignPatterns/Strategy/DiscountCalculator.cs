using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Strategy
{
    public class DiscountCalculator
    {
        public double CalculateDiscount(string customerType, double price)
        {
            if (customerType == "regular")
            {
                return price * 0.05; // 10% discount for regular customers
            }
            else if (customerType == "premium")
            {
                return price * 0.1; // 20% discount for premium customers
            }
            else if (customerType == "vip")
            {
                return price * 0.2; // 30% discount for VIP customers
            }
            else
            {
                return 0; // No discount for other customer types
            }
        }
    }
}

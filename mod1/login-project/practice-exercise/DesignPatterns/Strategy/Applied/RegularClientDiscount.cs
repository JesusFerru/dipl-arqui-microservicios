using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Strategy.Applied
{
    public class RegularClientDiscount : IDiscountStrategy
    {
        public double CalculateDiscount(double price)
        {
            return price * 0.05; // 5% discount for regular customers
        }
    }
}

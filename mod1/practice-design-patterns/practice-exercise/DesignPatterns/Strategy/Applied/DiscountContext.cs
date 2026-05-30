namespace DesignPatterns.Strategy.Applied
{
    public class DiscountContext
    {
        private IDiscountStrategy _strategy;
        //public DiscountContext(IDiscountStrategy strategy)
        //{
        //    _strategy = strategy;
        //}
        public void SetStrategy(IDiscountStrategy strategy)
        {
            _strategy = strategy;
        }
        public double CalculateDiscount(double price)
        {
            return _strategy.CalculateDiscount(price);
        }
    }
}

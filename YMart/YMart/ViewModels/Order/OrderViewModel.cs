namespace YMart.ViewModels.Order
{
    public class OrderViewModel
    {
        public Guid Id { get; set; }

        public List<decimal> ItemPrices { get; set; }
        public List<string> ItemNames { get; set; }
        public List<int> ItemQuantities { get; set; }

        public string OrderTime { get; set; }

        public decimal TotalPrice { get; set; }

        public string ClientEmail { get; set; }
    }
}

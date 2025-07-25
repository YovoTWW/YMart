namespace YMart.ViewModels.Order
{
    public class OrderViewModel
    {
        public Guid Id { get; set; }

        //public Dictionary<string,int> Items { get; set; }
        public List<string> ItemNames { get; set; }

        public List<int> ItemQuantities { get; set; }

        public string OrderTime { get; set; }

        public decimal TotalPrice { get; set; }

        public string ClientEmail { get; set; }
    }
}

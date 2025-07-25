namespace YMart.ViewModels.Order
{
    public class MiniOrderViewModel
    {
        public Guid Id { get; set; }

        public string OrderTime { get; set; }

        public decimal TotalPrice { get; set; }
    }
}

namespace YMart.ViewModels.Product
{
    public class DetailedProductViewModel
    {

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;


        public decimal Price { get; set; }


        public string Description { get; set; } = null!;


        public string Category { get; set; } = null!;


        public int Quantity { get; set; }

        public decimal DiscountedPrice { get; set; }

        public decimal? DiscountPercentage { get; set; }

        public string ImageURL { get; set; } = null!;

        public string? PreviousPageAction {  get; set; }

        public string? PreviousPageController { get; set; }
    }
}

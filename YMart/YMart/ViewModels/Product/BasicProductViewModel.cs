namespace YMart.ViewModels.Product
{
    public class BasicProductViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string ImageURL { get; set; } = null!;
    }
}

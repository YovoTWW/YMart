using System.ComponentModel.DataAnnotations;

namespace YMart.ViewModels.Product
{
    public class AddProductViewModel
    {
        public string Id { get; set; } = null!;


        public string Name { get; set; } = null!;


        public decimal Price { get; set; }


        public string Description { get; set; } = null!;


        public string Category { get; set; } = null!;


        public int Quantity { get; set; }


        public string ImageURL { get; set; } = null!;
    }
}

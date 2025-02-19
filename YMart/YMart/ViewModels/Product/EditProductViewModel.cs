using static YMart.Constants.CategoryList;

namespace YMart.ViewModels.Product
{
    public class EditProductViewModel
    {
        public string Name { get; set; } = null!;


        public decimal Price { get; set; }


        public string Description { get; set; } = null!;


        public string Category { get; set; } = null!;


        public int Quantity { get; set; }


        public string ImageURL { get; set; } = null!;

        public List<string> CategoriesList { get; set; } = Categories;
    }
}

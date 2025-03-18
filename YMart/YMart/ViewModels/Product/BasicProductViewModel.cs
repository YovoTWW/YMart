namespace YMart.ViewModels.Product
{
    using YMart.Constants;
    //using static YMart.Constants.CategoryList;
    public class BasicProductViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string ImageURL { get; set; } = null!;
        public int Quantity { get; set; }

       // public List<string> CategoriesList { get; } = CategoryList.Categories;
    }
}

namespace YMart.ViewModels.HomePage
{
    using YMart.ViewModels.Brochure;
    using YMart.ViewModels.Product;
    public class HomePageViewModel
    {
       public List<AddBrochureViewModel> Brochures {  get; set; }

       public IEnumerable<BasicProductViewModel> Offers { get; set; }
    }
}

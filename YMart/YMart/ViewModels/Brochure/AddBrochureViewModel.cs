using System.ComponentModel.DataAnnotations;

namespace YMart.ViewModels.Brochure
{
    using YMart.Data.Models;
    public class AddBrochureViewModel
    {
        [Required]
        public string ImageURL { get; set; }

        public List<string> ProductNames { get; set; }

    }
}

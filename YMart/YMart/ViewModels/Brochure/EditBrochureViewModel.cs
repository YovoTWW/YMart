using System.ComponentModel.DataAnnotations;

namespace YMart.ViewModels.Brochure
{
    public class EditBrochureViewModel
    {

        [Required]
        public Guid Id { get; set; }

        [Required]
        public string ImageURL { get; set; }

        public List<string> ProductNames { get; set; }

        public List<string> AllProducts {  get; set; }

        public bool IsActive { get; set; }
    }
}

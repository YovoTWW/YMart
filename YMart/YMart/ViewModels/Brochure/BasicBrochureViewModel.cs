using System.ComponentModel.DataAnnotations;

namespace YMart.ViewModels.Brochure
{
    public class BasicBrochureViewModel
    {
        [Required]
        public string ImageURL { get; set; }

        public List<string> ProductNames { get; set; }

        [Required]
        public Guid Id { get; set; }

        public bool IsActive { get; set; }
    }
}

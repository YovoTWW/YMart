using System.ComponentModel.DataAnnotations;

namespace YMart.Data.Models
{
    public class Brochure
    {
        public Brochure()
        { 
            this.Id = Guid.NewGuid();
            //this.Products = new List<Product>();
        }

        public Guid Id { get; set; }

        [Required]
        public string ImageURL { get; set; }

        public bool IsActive { get; set; }

        //public List<Product> Products {get; set;} 
        public List<string> ProductNames {  get; set; }
    }
}

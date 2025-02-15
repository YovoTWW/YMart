using System.ComponentModel.DataAnnotations;

namespace YMart.Data.Models
{
    public class Product
    {
        public Product()
        {
            this.Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public int Quantity {  get; set; }

        [Required]
        public string ImageURL { get; set; }
    }
}

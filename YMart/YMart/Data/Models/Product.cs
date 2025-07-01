using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YMart.Data.Models
{
    public class Product
    {
        public Product()
        {
            this.Id = Guid.NewGuid();
            this.Carts = new List<Cart>();
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

        public bool IsDeleted {  get; set; }

        public List<Cart> Carts { get; set; }

        public bool IsOnSale { get; set; } = false;

        public decimal? DiscountPercentage { get; set; } 

        [NotMapped]
        public decimal DiscountedPrice
        {
            get
            {
                if (IsOnSale && DiscountPercentage.HasValue)
                {
                    return Math.Round(Price * (1 - DiscountPercentage.Value / 100), 2);
                }

                return Price;
            }
        }
    }
}

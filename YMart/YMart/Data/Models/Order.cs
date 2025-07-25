using System.ComponentModel.DataAnnotations;

namespace YMart.Data.Models
{
    public class Order
    {
        public Order()
        {
            this.Id = Guid.NewGuid();           
        }

        [Key]
        public Guid Id { get; set; }
        public List<decimal> ItemPrices { get; set; }
        public List<string> ItemNames { get; set; }
        public List<int> ItemQuantities { get; set; }

        public DateTime OrderTime { get; set; }

        public decimal TotalPrice { get; set; }

        public string ClientEmail { get; set; }
    }
}

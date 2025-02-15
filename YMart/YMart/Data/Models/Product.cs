namespace YMart.Data.Models
{
    public class Product
    {
        public Product()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public int Quantity {  get; set; }
    }
}

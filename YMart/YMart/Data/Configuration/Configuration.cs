using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YMart.Data.Models;

namespace YMart.Data.Configuration
{
    public class Configuration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasKey(c => new {c.ProductId,c.ClientId});
        }
    }
}

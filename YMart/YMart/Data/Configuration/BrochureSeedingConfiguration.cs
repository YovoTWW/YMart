using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YMart.Data.Models;

namespace YMart.Data.Configuration
{
    public class BrochureSeedingConfiguration : IEntityTypeConfiguration<Brochure>
    {
        public void Configure(EntityTypeBuilder<Brochure> builder)
        {
            builder.HasKey(b => b.Id);

            builder.HasData(
                new Brochure
                {
                    Id = Guid.Parse("8b5df7bd-3654-4879-b9b2-4d9b98fea8fd"),
                    ImageURL = "https://cdn.dribbble.com/userupload/33482357/file/original-b0a2c3498d5afb8ebb87326104c4bcc2.jpg",
                    IsActive = true,
                    ProductNames = new List<string>
                    {
                    "Гейминг комплект мишка/подложка Redragon",
                    "Table",
                    "Gamer Hoodie",
                    "Desk"
                    }
                },
                new Brochure
                {
                    Id = Guid.Parse("c9dda83d-c8c9-47ee-8e20-f5c892f2043d"),
                    ImageURL = "https://img.freepik.com/free-vector/gradient-gaming-setup-brochure_23-2149833246.jpg?semt=ais_items_boosted&w=740",
                    IsActive = true,
                    ProductNames = new List<string>
                    {
                    "Gamer Hoodie",
                    "Logitech H390 Wired Headset for PC/Laptop",
                    "Logitech K120 Wired Keyboard for Windows"
                    }
                }
            );
        }
    }
}

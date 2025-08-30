using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YMart.Data.Models;

namespace YMart.Data.Configuration
{
    public class ProductSeedingConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasData(
                new Product
                {
                    Id = Guid.Parse("7cc4f639-3036-4548-9c22-0df3491ff635"),
                    Name = "Гейминг комплект мишка/подложка Redragon",
                    Price = 22.50m,
                    Description = "Комплектът Redragon M601-BA включва уникални по рода си геймърска мишка и подложка, в тематична Redragon тема, предлагащи ви стила и функционалността, които ще издигнат гейминга ви на изцяло ново, по-добро ниво.",
                    Category = "Tech",
                    Quantity = 13,
                    ImageURL = "https://cdn.ozone.bg/media/catalog/product/cache/1/image/a4e40ebdc3e371adff845072e1c73f37/4/6/9bc828752ec7732bed4fe7d0e5ddf742/geyming-komplekt-mishka-i-podlozhka-redragon---m601-ba--cheren-31.jpg",
                    IsDeleted = false,
                    DiscountPercentage = 10.00m,
                    IsOnSale = true
                },
                new Product
                {
                    Id = Guid.Parse("6acbbde1-5929-43c0-9a72-4f18b07e3f3d"),
                    Name = "Beginner Football for kids",
                    Price = 12.00m,
                    Description = "A football meant for children , for an easier time playing football",
                    Category = "Sports",
                    Quantity = 105,
                    ImageURL = "https://m.media-amazon.com/images/I/51rCXKaVGTL.jpg",
                    IsDeleted = false,
                    DiscountPercentage = null,
                    IsOnSale = false
                },
                new Product
                {
                    Id = Guid.Parse("cb587e4d-a525-4d8d-814d-6c155182ec30"),
                    Name = "Table",
                    Price = 65.00m,
                    Description = "A normal table for your home",
                    Category = "Furniture",
                    Quantity = 2,
                    ImageURL = "https://images.thdstatic.com/productImages/1c654fe6-311b-4755-b501-a861c534988f/svn/brown-byblight-kitchen-dining-tables-bb-f1889yf-64_600.jpg",
                    IsDeleted = false,
                    DiscountPercentage = 25.00m,
                    IsOnSale = true
                },
                new Product
                {
                    Id = Guid.Parse("1462d7c3-6736-403c-965f-7d899ec21ca1"),
                    Name = "Gamer Hoodie",
                    Price = 44.00m,
                    Description = "A stylish gamer hoodie for devout gamers to wear when going out.",
                    Category = "Clothing",
                    Quantity = 5,
                    ImageURL = "https://5hourenergy.com/cdn/shop/files/Gamer_Hoodie.jpg?v=1738188168",
                    IsDeleted = false,
                    DiscountPercentage = 15.00m,
                    IsOnSale = true
                },
                new Product
                {
                    Id = Guid.Parse("7203c3c8-9a31-420f-a50d-82f203b3d40e"),
                    Name = "Гейминг лаптоп ASUS - TUF A15",
                    Price = 720.00m,
                    Description = "Asus TUF A15 FA506NC-HN012 е висококачествен гейминг лаптоп, комбиниращ производителност от AMD Ryzen и NVIDIA GeForce RTX 30, което ви осигурява отлична бързина и мощност. Той има тънък корпус, малки размери и модерен дизайн, които ви позволяват да играете любимите си игри навсякъде и по всяко време. Създаден за сериозни игри и с елегантен стил, TUF Gaming A15 е геймърски лаптоп, пълен с функции и мощност, която ще ви отведе до победа. Графичният процесор GeForce RTX 30-серия осигурява плавен геймплей на дисплей с честота от 144Hz. От своя страна отличният процесор AMD Ryzen е подсилен от подобрено охлаждане, което повишава производителността на процесора и постига тих звук. Дълготрайната батерия, съчетана с военна издръжливост на TUF, ви гарантират най-добрата ви игра навсякъде.",
                    Category = "Tech",
                    Quantity = 14,
                    ImageURL = "https://cdn.ozone.bg/media/catalog/product/cache/1/image/a4e40ebdc3e371adff845072e1c73f37/g/e/6a4b9050825639716505394da9d5dfca/geyming-laptop-asus---tuf-a15-fa506nc-hn012--156----ryzen-5--144hz-30.jpg",
                    IsDeleted = false,
                    DiscountPercentage = null,
                    IsOnSale = false
                },
                new Product
                {
                    Id = Guid.Parse("0c3fbb68-6ba6-43fa-a9c2-8c1dec5a4097"),
                    Name = "Duplicate Table",
                    Price = 5.00m,
                    Description = "This is an image duplicate for testing purposes",
                    Category = "Furniture",
                    Quantity = 10,
                    ImageURL = "https://images.thdstatic.com/productImages/1c654fe6-311b-4755-b501-a861c534988f/svn/brown-byblight-kitchen-dining-tables-bb-f1889yf-64_600.jpg",
                    IsDeleted = false,
                    DiscountPercentage = null,
                    IsOnSale = false
                },
                new Product
                {
                    Id = Guid.Parse("c81aa4ed-ec46-43aa-90ab-8c3ba87e6d7d"),
                    Name = "Desk",
                    Price = 160.00m,
                    Description = "A wooden desk suitible for studying or gaming",
                    Category = "Furniture",
                    Quantity = 0,
                    ImageURL = "https://wwmake.com/cdn/shop/files/MacieDeskWalnutClear_800x.jpg?v=1689075504",
                    IsDeleted = false,
                    DiscountPercentage = 50.00m,
                    IsOnSale = true
                },
                new Product
                {
                    Id = Guid.Parse("9de4ca2f-9699-4890-b01d-b6d4440eb646"),
                    Name = "Logitech H390 Wired Headset for PC/Laptop",
                    Price = 15.00m,
                    Description = "Digital Stereo Sound: Fine-tuned drivers provide enhanced digital audio for music, calls, meetings and more. Rotating Noise Canceling Mic: Minimizes unwanted background noise for clear conversations; the rotating boom arm can be tucked out of the way when you’re not using it.",
                    Category = "Tech",
                    Quantity = 0,
                    ImageURL = "https://m.media-amazon.com/images/I/61jBnY6paeL._AC_SL1500_.jpg",
                    IsDeleted = false,
                    DiscountPercentage = null,
                    IsOnSale = false
                },
                new Product
                {
                    Id = Guid.Parse("13c865ba-8bc0-49f9-9260-cfe16a93cbb2"),
                    Name = "Logitech K120 Wired Keyboard for Windows",
                    Price = 12.10m,
                    Description = "All-day Comfort: The design of this standard keyboard creates a comfortable typing experience thanks to the deep-profile keys and full-size standard layout with F-keys and number pad. Easy to Set-up and Use: Set-up couldn't be easier, you simply plug in this corded keyboard via USB on your desktop or laptop and start using right away without any software installation.",
                    Category = "Tech",
                    Quantity = 0,
                    ImageURL = "https://m.media-amazon.com/images/I/61j3wQheLXL._UF1000,1000_QL80_.jpg",
                    IsDeleted = false,
                    DiscountPercentage = null,
                    IsOnSale = false
                },
                new Product
                {
                    Id = Guid.Parse("f70293a9-83ee-4447-b96b-e30dcfbd5310"),
                    Name = "Gaming Mouse White Shark Dagonet",
                    Price = 40.00m,
                    Description = "Безжична, лека и свръхпрецизна - геймърската мишка White Shark Dagonet е проектирана за онези, които искат да бъдат свободни от кабели, без да жертват производителност. С тегло само 50 грама, тази мишка е едно от най-леките геймърски предложения на пазара, което гарантира бързи и прецизни реакции при всяко движение.",
                    Category = "Tech",
                    Quantity = 5,
                    ImageURL = "https://cdn.ozone.bg/media/catalog/product/cache/1/image/a4e40ebdc3e371adff845072e1c73f37/g/e/764b120060ff570da8aba3222224d42b/geyming-mishka-white-shark---dagonet--optichna--bezzhichna--cherna-lilava-30.jpg",
                    IsDeleted = false,
                    DiscountPercentage = null,
                    IsOnSale = false
                }
            );
        }
    }
}

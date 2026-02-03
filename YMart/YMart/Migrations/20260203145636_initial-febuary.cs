using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace YMart.Migrations
{
    /// <inheritdoc />
    public partial class initialfebuary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Brochure",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ProductNames = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brochure", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemPrices = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemNames = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemQuantities = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ClientEmail = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsOnSale = table.Column<bool>(type: "bit", nullable: false),
                    DiscountPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => new { x.ProductId, x.ClientId });
                    table.ForeignKey(
                        name: "FK_Cart_AspNetUsers_ClientId",
                        column: x => x.ClientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Cart_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.InsertData(
                table: "Brochure",
                columns: new[] { "Id", "ImageURL", "IsActive", "ProductNames" },
                values: new object[,]
                {
                    { new Guid("8b5df7bd-3654-4879-b9b2-4d9b98fea8fd"), "https://cdn.dribbble.com/userupload/33482357/file/original-b0a2c3498d5afb8ebb87326104c4bcc2.jpg", true, "[\"\\u0413\\u0435\\u0439\\u043C\\u0438\\u043D\\u0433 \\u043A\\u043E\\u043C\\u043F\\u043B\\u0435\\u043A\\u0442 \\u043C\\u0438\\u0448\\u043A\\u0430/\\u043F\\u043E\\u0434\\u043B\\u043E\\u0436\\u043A\\u0430 Redragon\",\"Table\",\"Gamer Hoodie\",\"Desk\"]" },
                    { new Guid("c9dda83d-c8c9-47ee-8e20-f5c892f2043d"), "https://img.freepik.com/free-vector/gradient-gaming-setup-brochure_23-2149833246.jpg?semt=ais_items_boosted&w=740", true, "[\"Gamer Hoodie\",\"Logitech H390 Wired Headset for PC/Laptop\",\"Logitech K120 Wired Keyboard for Windows\"]" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "Description", "DiscountPercentage", "ImageURL", "IsDeleted", "IsOnSale", "Name", "Price", "Quantity" },
                values: new object[,]
                {
                    { new Guid("0c3fbb68-6ba6-43fa-a9c2-8c1dec5a4097"), "Furniture", "This is an image duplicate for testing purposes", null, "https://images.thdstatic.com/productImages/1c654fe6-311b-4755-b501-a861c534988f/svn/brown-byblight-kitchen-dining-tables-bb-f1889yf-64_600.jpg", false, false, "Duplicate Table", 5.00m, 10 },
                    { new Guid("13c865ba-8bc0-49f9-9260-cfe16a93cbb2"), "Tech", "All-day Comfort: The design of this standard keyboard creates a comfortable typing experience thanks to the deep-profile keys and full-size standard layout with F-keys and number pad. Easy to Set-up and Use: Set-up couldn't be easier, you simply plug in this corded keyboard via USB on your desktop or laptop and start using right away without any software installation.", null, "https://m.media-amazon.com/images/I/61j3wQheLXL._UF1000,1000_QL80_.jpg", false, false, "Logitech K120 Wired Keyboard for Windows", 12.10m, 0 },
                    { new Guid("1462d7c3-6736-403c-965f-7d899ec21ca1"), "Clothing", "A stylish gamer hoodie for devout gamers to wear when going out.", 15.00m, "https://5hourenergy.com/cdn/shop/files/Gamer_Hoodie.jpg?v=1738188168", false, true, "Gamer Hoodie", 44.00m, 5 },
                    { new Guid("6acbbde1-5929-43c0-9a72-4f18b07e3f3d"), "Sports", "A football meant for children , for an easier time playing football", null, "https://m.media-amazon.com/images/I/51rCXKaVGTL.jpg", false, false, "Beginner Football for kids", 12.00m, 105 },
                    { new Guid("7203c3c8-9a31-420f-a50d-82f203b3d40e"), "Tech", "Asus TUF A15 FA506NC-HN012 е висококачествен гейминг лаптоп, комбиниращ производителност от AMD Ryzen и NVIDIA GeForce RTX 30, което ви осигурява отлична бързина и мощност. Той има тънък корпус, малки размери и модерен дизайн, които ви позволяват да играете любимите си игри навсякъде и по всяко време. Създаден за сериозни игри и с елегантен стил, TUF Gaming A15 е геймърски лаптоп, пълен с функции и мощност, която ще ви отведе до победа. Графичният процесор GeForce RTX 30-серия осигурява плавен геймплей на дисплей с честота от 144Hz. От своя страна отличният процесор AMD Ryzen е подсилен от подобрено охлаждане, което повишава производителността на процесора и постига тих звук. Дълготрайната батерия, съчетана с военна издръжливост на TUF, ви гарантират най-добрата ви игра навсякъде.", null, "https://cdn.ozone.bg/media/catalog/product/cache/1/image/a4e40ebdc3e371adff845072e1c73f37/g/e/6a4b9050825639716505394da9d5dfca/geyming-laptop-asus---tuf-a15-fa506nc-hn012--156----ryzen-5--144hz-30.jpg", false, false, "Гейминг лаптоп ASUS - TUF A15", 720.00m, 14 },
                    { new Guid("7cc4f639-3036-4548-9c22-0df3491ff635"), "Tech", "Комплектът Redragon M601-BA включва уникални по рода си геймърска мишка и подложка, в тематична Redragon тема, предлагащи ви стила и функционалността, които ще издигнат гейминга ви на изцяло ново, по-добро ниво.", 10.00m, "https://cdn.ozone.bg/media/catalog/product/cache/1/image/a4e40ebdc3e371adff845072e1c73f37/4/6/9bc828752ec7732bed4fe7d0e5ddf742/geyming-komplekt-mishka-i-podlozhka-redragon---m601-ba--cheren-31.jpg", false, true, "Гейминг комплект мишка/подложка Redragon", 22.50m, 13 },
                    { new Guid("9de4ca2f-9699-4890-b01d-b6d4440eb646"), "Tech", "Digital Stereo Sound: Fine-tuned drivers provide enhanced digital audio for music, calls, meetings and more. Rotating Noise Canceling Mic: Minimizes unwanted background noise for clear conversations; the rotating boom arm can be tucked out of the way when you’re not using it.", null, "https://m.media-amazon.com/images/I/61jBnY6paeL._AC_SL1500_.jpg", false, false, "Logitech H390 Wired Headset for PC/Laptop", 15.00m, 0 },
                    { new Guid("c81aa4ed-ec46-43aa-90ab-8c3ba87e6d7d"), "Furniture", "A wooden desk suitible for studying or gaming", 50.00m, "https://wwmake.com/cdn/shop/files/MacieDeskWalnutClear_800x.jpg?v=1689075504", false, true, "Desk", 160.00m, 0 },
                    { new Guid("cb587e4d-a525-4d8d-814d-6c155182ec30"), "Furniture", "A normal table for your home", 25.00m, "https://images.thdstatic.com/productImages/1c654fe6-311b-4755-b501-a861c534988f/svn/brown-byblight-kitchen-dining-tables-bb-f1889yf-64_600.jpg", false, true, "Table", 65.00m, 2 },
                    { new Guid("f70293a9-83ee-4447-b96b-e30dcfbd5310"), "Tech", "Безжична, лека и свръхпрецизна - геймърската мишка White Shark Dagonet е проектирана за онези, които искат да бъдат свободни от кабели, без да жертват производителност. С тегло само 50 грама, тази мишка е едно от най-леките геймърски предложения на пазара, което гарантира бързи и прецизни реакции при всяко движение.", null, "https://cdn.ozone.bg/media/catalog/product/cache/1/image/a4e40ebdc3e371adff845072e1c73f37/g/e/764b120060ff570da8aba3222224d42b/geyming-mishka-white-shark---dagonet--optichna--bezzhichna--cherna-lilava-30.jpg", false, false, "Gaming Mouse White Shark Dagonet", 40.00m, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_ClientId",
                table: "Cart",
                column: "ClientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Brochure");

            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}

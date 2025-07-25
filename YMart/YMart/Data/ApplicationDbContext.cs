using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using YMart.Data.Models;
using YMart.Data.Configuration;

namespace YMart.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Cart> Cart { get; set; }

        public DbSet<Brochure> Brochure { get; set;}

        public DbSet<Order> Orders { get; set; }
    }
}

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineRetailPlatformDiss.Models;
using System;

namespace OnlineRetailPlatformDiss.Data
{
    public class ApplicationDbContext : IdentityDbContext<UserModel>
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<BusinessAccountModel>? BusinessAccount { get; set; }
        public DbSet<ProductModel>? Products { get; set; }
        public DbSet<BasketModel>? Baskets { get; set; }
        public DbSet<OrderModel>? Orders { get; set; }
        public DbSet<OrderLineModel>? OrderLines { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //WARNING: Sensitive Data is potentially being exposed here... 
                //This should ideally be moved out of here, or stored within a secret and accessed using that!
                optionsBuilder.UseSqlServer("Server=tcp:dissertationonlineretailplatform.database.windows.net,1433;Initial Catalog=DissOnlineRetailPlatform;Persist Security Info=False;User ID=connor;Password=Password1!;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

    }
}
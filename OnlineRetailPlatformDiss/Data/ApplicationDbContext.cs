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

        public DbSet<Baskets>? Baskets { get; set; }


        //public DbSet<ShoppingBasketProductModel> ShoppingBasketProducts { get; set;}

    }
}
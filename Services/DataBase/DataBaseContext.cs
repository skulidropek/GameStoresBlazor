using GameStoresBlazor.Models.Product;
using GameStoresBlazor.Models.Transactions;
using GameStoresBlazor.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GameStoresBlazor.Services.DataBase
{
    public class DataBaseContext : IdentityDbContext<SteamIdentityUserModel>
    {
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<ProductModel> Products { get; set; }
        public DbSet<ItemModel> Items { get; set; }
        public DbSet<UserInventoryProductModel> Inventories { get; set; }
        public DbSet<TransactionModel> Transactions { get; set; }

        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
        }
    }
}

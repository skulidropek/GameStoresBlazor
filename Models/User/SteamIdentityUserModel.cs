using GameStoresBlazor.Models.Product;
using GameStoresBlazor.Models.Transactions;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace GameStoresBlazor.Models.User
{
    public class SteamIdentityUserModel : IdentityUser
    {
        public string Name { get; set; }
        public string SteamId { get => base.UserName; set => base.UserName = value; }

        public virtual ICollection<UserInventoryProductModel> Inventory { get; set; }
        public virtual ICollection<TransactionModel> Transactions { get; set; }
    }
}

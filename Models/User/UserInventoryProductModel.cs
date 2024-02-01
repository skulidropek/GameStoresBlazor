using GameStoresBlazor.Models.Product;
using System.ComponentModel.DataAnnotations;

namespace GameStoresBlazor.Models.User
{
    public class UserInventoryProductModel
    {
        [Key]
        public Guid Id { get; set; }

        public bool IsActive { get; set; }

        public string UserId { get; set; }
        public virtual SteamIdentityUserModel User { get; set; }

        public Guid ProductId { get; set; }
        public virtual ProductModel Product { get; set; }

        public uint Quantity { get; set; }
    }
}

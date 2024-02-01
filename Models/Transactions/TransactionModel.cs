using GameStoresBlazor.Models.User;
using System.ComponentModel.DataAnnotations;

namespace GameStoresBlazor.Models.Transactions
{
    public class TransactionModel
    {
        [Key]
        public Guid Id { get; set; }

        public decimal Payment { get; set; }

        public string? Description { get; set; }

        public string UserId { get; set; }
        
        public virtual SteamIdentityUserModel User { get; set; }

        public Guid? UserInventoryProductId { get; set; } 
        public virtual UserInventoryProductModel? UserInventoryProduct { get; set; }
    }   
}

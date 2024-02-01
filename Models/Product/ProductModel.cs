using GameStoresBlazor.Models.User;
using System.ComponentModel.DataAnnotations;

namespace GameStoresBlazor.Models.Product
{
    public class ProductModel
    {
        [Key]
        public Guid Id { get; set; }

        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public uint Quantity { get; set; }
        public decimal Price { get; set; }
        public int Discount { get; set; }

        public Guid CategoryId { get; set; }
        public virtual CategoryModel Category { get; set; }

        public virtual ICollection<ItemModel> Items { get; set; }
        public virtual ICollection<UserInventoryProductModel> UserInventories { get; set; }
    }
}
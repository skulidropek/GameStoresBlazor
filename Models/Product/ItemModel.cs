using System.ComponentModel.DataAnnotations;

namespace GameStoresBlazor.Models.Product
{
    public class ItemModel
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int Count { get; set; }
        public float Price { get; set; }

        public Guid ProductId { get; set; }
        public ProductModel Product { get; set; }
    }
}

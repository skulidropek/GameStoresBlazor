using GameStoresBlazor.Models.User;
using System.ComponentModel.DataAnnotations;

namespace GameStoresBlazor.Models.Product
{
    public class CategoryModel
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ProductModel> Products { get; set; }
    }
}

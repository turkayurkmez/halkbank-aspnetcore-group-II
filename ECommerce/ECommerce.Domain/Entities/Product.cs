using ECommerce.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Domain.Entities
{
    public class Product : IEntity
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Ürün Adı boş olamaz")]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        public string SKU { get; set; } = string.Empty;
       
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }

        //Navigation Property:
        public ICollection<Review>? Reviews { get; set; }
        public ICollection<OrderItem>? OrderItems { get; set; }
        public Category? Category { get; set; }

    }
}

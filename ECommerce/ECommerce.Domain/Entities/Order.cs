namespace ECommerce.Domain.Entities
{
    public class Order : IEntity
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime CreatedDate { get; set; }

        public ICollection<OrderItem> Items { get; set; }

        public decimal Total() => Items.Sum(x => x.UnitPrice * x.Quantity);

        public Customer Customer { get; set; }

    }
}

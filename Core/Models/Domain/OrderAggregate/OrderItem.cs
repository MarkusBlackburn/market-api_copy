namespace Core.Models.Domain.OrderAggregate
{
    public class OrderItem : BaseSimpleEntity
    {
        public ProductItemOrdered ItemOrdered { get; set; } = null!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}

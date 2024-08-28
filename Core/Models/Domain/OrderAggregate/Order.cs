namespace Core.Models.Domain.OrderAggregate
{
    public class Order : BaseSimpleEntity
    {
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public required string BuyerEmail { get; set; } = string.Empty;
        public ShippingAddress ShippingAddress { get; set; } = null!;
        public DeliveryMethod DeliveryMethod { get; set; } = null!;
        public PaymentSummary PaymentSummary { get; set; } = null!;
        public IReadOnlyList<OrderItem> OrderItems { get; set; } = [];
        public decimal SubTotal { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public required string PaymentIntentId { get; set; } = string.Empty;
    }
}

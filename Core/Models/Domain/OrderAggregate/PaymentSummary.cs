namespace Core.Models.Domain.OrderAggregate
{
    public class PaymentSummary
    {
        public int Last4 { get; set; }
        public int ExpMonth { get; set; }
        public int ExpYear { get; set; }
    }
}

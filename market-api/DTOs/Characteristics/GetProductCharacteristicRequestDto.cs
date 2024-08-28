namespace market_api.DTOs.Characteristics
{
    public class GetProductCharacteristicRequestDto
    {
        public int CharacteristicId { get; set; }
        public string ProductCharacteristicName { get; set; } = string.Empty;
        public string ProductCharacteristicDescription { get; set; } = string.Empty;
        public int ProductId { get; set; }
    }
}

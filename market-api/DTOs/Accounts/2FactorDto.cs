using System.ComponentModel.DataAnnotations;

namespace market_api.DTOs.Accounts
{
    public class _2FactorDto
    {
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Provider { get; set; }
        [Required]
        public string? Token { get; set; }
    }
}

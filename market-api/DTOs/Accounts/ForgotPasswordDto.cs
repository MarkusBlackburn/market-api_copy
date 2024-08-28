using System.ComponentModel.DataAnnotations;

namespace market_api.DTOs.Accounts
{
    public class ForgotPasswordDto
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? ClientURI { get; set; }
    }
}

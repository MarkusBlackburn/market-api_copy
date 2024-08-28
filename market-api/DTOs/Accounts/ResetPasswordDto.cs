using System.ComponentModel.DataAnnotations;

namespace market_api.DTOs.Accounts
{
    public class ResetPasswordDto
    {
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Token { get; set; }
    }
}

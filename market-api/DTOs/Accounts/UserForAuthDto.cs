using System.ComponentModel.DataAnnotations;

namespace market_api.DTOs.Accounts
{
    public class UserForAuthDto
    {
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? ClientURI { get; set; }
    }
}

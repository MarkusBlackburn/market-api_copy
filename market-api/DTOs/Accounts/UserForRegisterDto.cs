using System.ComponentModel.DataAnnotations;

namespace market_api.DTOs.Accounts
{
    public class UserForRegisterDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        public string? ClientURI { get; set; }
    }
}

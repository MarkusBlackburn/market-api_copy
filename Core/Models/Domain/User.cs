using Microsoft.AspNetCore.Identity;

namespace Core.Models.Domain
{
    public class User : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public ShippingAddress? Address { get; set; }
    }
}

using Core.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Authentication;
using System.Security.Claims;

namespace market_api.Util.Extensions
{
    public static class ClaimsPrincipleExtension
    {
        public static async Task<User> GetUserByEmail(this UserManager<User> userManager, ClaimsPrincipal user)
        {
            var userToReturn = await userManager.Users.FirstOrDefaultAsync(x => x.Email == user.GetEmail());

            return userToReturn is null ? throw new AuthenticationException("User not found!") : userToReturn;
        }

        public static async Task<User> GetUserByEmailWithAddress(this UserManager<User> userManager, ClaimsPrincipal user)
        {
            var userToReturn = await userManager.Users.Include(x => x.Address).FirstOrDefaultAsync(x => x.Email == user.GetEmail());

            return userToReturn is null ? throw new AuthenticationException("User not found!") : userToReturn;
        }

        public static string GetEmail(this ClaimsPrincipal user)
        {
            var email = user.FindFirstValue(ClaimTypes.Email) ?? throw new AuthenticationException("Email claim not found!");

            return email;
        }
    }
}

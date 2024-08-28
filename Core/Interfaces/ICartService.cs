using Core.Models.Domain;

namespace Core.Interfaces
{
    public interface ICartService
    {
        Task<ShoppingCart?> GetShoppingCartAsync(string key);
        Task<ShoppingCart?> SetShoppingCartAsync(ShoppingCart cart);
        Task<bool> DeleteShoppingCartAsync(string key);
    }
}

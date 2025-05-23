using Core.Domain.Entities;

namespace Core.Infrastructure.Data.Interfaces
{
    public interface IDynamoDbContext
    {
        Task<User> GetUserByNameAsync(string name);
        Task<User> GetUserById(string Id);
        Task<FavoriteCity> InsertFavoriteCity(FavoriteCity favoriteCity);
        Task<List<FavoriteCity>> GetAllFavoriteCityByUserId(string userId);
        Task DeleteFavoriteCity(FavoriteCity favoriteCity);
    }
}

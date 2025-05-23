using Core.Domain.DTOs;
using Core.Domain.Entities;

namespace Core.Services.BusinessRules.Interfaces
{
    public interface IFavoritesBR
    {
        Task CreateFavoriteCity(FavoriteDTO favoriteDTO);
        Task DeleteFavoriteCity(FavoriteDTO favoriteDTO);
        Task<List<FavoriteCity>> GetAllFavoriteCityByUserId(string userId);
    }
}

using Core.Domain.DTOs;
using Core.Domain.Entities;

namespace Core.Services.BusinessRules.Interfaces
{
    public interface IFavoritesBR
    {
        Task<bool> CreateFavoriteCity(FavoriteDTO favoriteDTO);
        Task<bool> DeleteFavoriteCity(FavoriteDTO favoriteDTO);
        Task<List<FavoriteCity>> GetAllFavoriteCityByUserId(string userId);
    }
}

using Core.Domain.DTOs;
using Core.Domain.Entities;
using Core.Domain.Enums;
using Core.Domain.Msgs;
using Core.Infrastructure.Data.Interfaces;
using Core.Services.BusinessRules.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Core.Services.BusinessRules
{
    public class FavoritesBR : IFavoritesBR
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        private readonly IDynamoDbContext _dbContext;
        
        public FavoritesBR(IConfiguration config, IDynamoDbContext dbContext, HttpClient httpClient)
        {
            _config = config;
            _dbContext = dbContext;
            _httpClient = httpClient;
        }

        public async Task<bool> CreateFavoriteCity(FavoriteDTO favoriteDTO)
        {
            try
            {
                await VerifyIfActionIsValid(favoriteDTO.Name, favoriteDTO.UserId, ActionType.Insert);

                FavoriteCity favoriteCity = new FavoriteCity()
                {
                    UserId = favoriteDTO.UserId,
                    CityName = favoriteDTO.Name
                };

                _ = _dbContext.InsertFavoriteCity(favoriteCity);

                return true;
            }
            catch (Exception ex)
            {
                //save log

                return false;
            }
        }

        public async Task<bool> DeleteFavoriteCity(FavoriteDTO favoriteDTO)
        {
            try
            {
                FavoriteCity favoriteCity = await VerifyIfActionIsValid(favoriteDTO.Name, favoriteDTO.UserId, ActionType.Delete);

                _ = _dbContext.DeleteFavoriteCity(favoriteCity.Id);

                return true;
            }
            catch (Exception ex)
            {
                //save log

                return false;
            }
        }

        public Task<List<FavoriteCity>> GetAllFavoriteCityByUserId(string userId)
        {
            return _dbContext.GetAllFavoriteCityByUserId(userId);
        }

        private async Task<FavoriteCity> VerifyIfActionIsValid(string cityName, string userId, ActionType actionType)
        {
            var baseUrl = _config["Urls:RestCountries"];
            var url = $"{baseUrl}{cityName}";

            var response = await _httpClient.GetAsync(url);

            if(response.IsSuccessStatusCode)
                throw new Exception(ApiMsgs.INF001);

            _ = await _dbContext.GetUserById(userId) ?? throw new Exception(ApiMsgs.EXC001);

            FavoriteCity favoriteCity = await _dbContext.GetFavoriteCityByCityNameAndUserId(cityName, userId);

            if (favoriteCity == null && actionType == ActionType.Delete)
                throw new Exception(ApiMsgs.EXC003);
            else if(favoriteCity != null && actionType == ActionType.Insert)
                throw new Exception(ApiMsgs.EXC004);

            return favoriteCity;
        }
    }
}

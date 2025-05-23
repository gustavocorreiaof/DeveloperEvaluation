using Core.Domain.DTOs;
using Core.Domain.Entities;
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
        
        public FavoritesBR(IConfiguration config, IDynamoDbContext dbContext)
        {
            _config = config;
            _dbContext = dbContext;
        }

        public async Task<bool> CreateFavoriteCity(FavoriteDTO favoriteDTO)
        {
            try
            {
                VerifyIfActionIsValid(favoriteDTO.Name, favoriteDTO.UserId);

                FavoriteCity favoriteCity = new FavoriteCity()
                {
                    UserId = favoriteDTO.UserId,
                    CityName = favoriteDTO.Name
                };

                _dbContext.InsertFavoriteCity(favoriteCity);

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
                VerifyIfActionIsValid(favoriteDTO.Name, favoriteDTO.UserId);

                FavoriteCity favoriteCity = new FavoriteCity()
                {
                    UserId = favoriteDTO.UserId,
                    CityName = favoriteDTO.Name
                };

                _dbContext.DeleteFavoriteCity(favoriteCity);

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

        private async void VerifyIfActionIsValid(string cityName, string userId)
        {
            var baseUrl = _config["Urls:RestCountries"];
            var url = $"{baseUrl}{cityName}";

            var response = await _httpClient.GetAsync(url);

            if(response.IsSuccessStatusCode)
                throw new Exception(ApiMsgs.INF001);

            User user = await _dbContext.GetUserById(userId) ?? throw new Exception(ApiMsgs.EXC001);
        }
    }
}

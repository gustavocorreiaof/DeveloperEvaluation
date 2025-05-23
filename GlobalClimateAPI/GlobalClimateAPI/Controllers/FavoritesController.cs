using Core.Domain.DTOs;
using Core.Domain.Entities;
using Core.Services.BusinessRules.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GlobalClimateAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FavoritesController : ControllerBase
    {
        private readonly IFavoritesBR _IFavoritesBR;

        public FavoritesController(IFavoritesBR iFavoritesBR)
        {
            _IFavoritesBR = iFavoritesBR;
        }

        #region CITIES CONTEXT

        [HttpGet]
        public async Task<IActionResult> GetFavoriteCitiesByUserId([FromQuery] string userId)
        {
            List<FavoriteCity> favoriteCities = await _IFavoritesBR.GetAllFavoriteCityByUserId(userId);

            return Ok(favoriteCities);
        }

        [HttpPost]
        public async Task<IActionResult> AddFavoriteCityByUserId([FromQuery] string userId, string cityName)
        {
            FavoriteDTO favoriteDTO = new FavoriteDTO(userId, cityName);

            bool favoriteCityHasCreated = await _IFavoritesBR.CreateFavoriteCity(favoriteDTO);

            if (favoriteCityHasCreated)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFavoriteCityByUserId([FromQuery] string userId, string cityName)
        {
            FavoriteDTO favoriteDTO = new FavoriteDTO(userId, cityName);

            bool favoriteCityHasCreated = await _IFavoritesBR.DeleteFavoriteCity(favoriteDTO);

            if (favoriteCityHasCreated)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        #endregion


        #region COUNTRIES CONTEXT

        /*[HttpGet]
        public IActionResult GetFavoriteCountriesByUserId()
        {

        }

        [HttpPost]
        public IActionResult AddFavoriteCountryByUserId()
        {

        }

        [HttpDelete]
        public IActionResult DeleteFavoriteCountryByUserId()
        {

        }*/

        #endregion
    }
}

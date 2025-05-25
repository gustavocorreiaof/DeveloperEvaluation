using Core.Domain.DTOs;
using Core.Domain.Entities;
using Core.Domain.Exceptions;
using Core.Domain.Msgs;
using Core.Services.BusinessRules.Interfaces;
using GlobalClimateAPI.Requests;
using GlobalClimateAPI.Responses;
using GlobalClimateAPI.Responses.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GlobalClimateAPI.Controllers
{
    [Authorize]
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

        [HttpGet("GetFavoriteCitiesByUserId")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetFavoriteCitiesResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponse))]
        [SwaggerOperation(Summary = "Returns the user's favorite cities.", Description = "Recive Id's User to search his favorite cities.")]
        public async Task<IActionResult> GetFavoriteCitiesByUserId([FromQuery, SwaggerParameter("The userId to search his favorite cities.")] string userId)
        {
            try
            {
                List<FavoriteCity> favoriteCities = await _IFavoritesBR.GetAllFavoriteCityByUserId(userId);

                return Ok(new GetFavoriteCitiesResponse() { Success = true, Cities = favoriteCities });
            }
            catch (ApiException ex)
            {
                return BadRequest(new BaseResponse() { Success = false, Message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponse() { Success = false, Message = ApiMsgs.INF004 });
            }
        }

        [HttpPost("AddFavoriteCityByUserId")]
        public async Task<IActionResult> AddFavoriteCityByUserId([FromBody] CityFavoriteRequest request)
        {
            try
            {
                FavoriteDTO favoriteDTO = new FavoriteDTO(request.UserId, request.CityName);

                await _IFavoritesBR.CreateFavoriteCity(favoriteDTO);

                return Ok(new BaseResponse() { Success = true, Message = ApiMsgs.INF005 });
            }
            catch (ApiException ex)
            {
                return BadRequest(new BaseResponse() { Success = false, Message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponse() { Success = false, Message = ApiMsgs.INF004 });
            }
        }

        [HttpDelete("DeleteFavoriteCityByUserId")]
        public async Task<IActionResult> DeleteFavoriteCityByUserId([FromBody] CityFavoriteRequest request)
        {
            try
            {
                FavoriteDTO favoriteDTO = new FavoriteDTO(request.UserId, request.CityName);

                await _IFavoritesBR.DeleteFavoriteCity(favoriteDTO);

                return Ok(new BaseResponse() { Success = true, Message = ApiMsgs.INF006 });
            }
            catch (ApiException ex)
            {
                return BadRequest(new BaseResponse() { Success = false, Message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponse() { Success = false, Message = ApiMsgs.INF004 });
            }
        }

        #endregion

        #region Countries CONTEXT

        [HttpGet("GetFavoriteCountriesByUserId")]
        public async Task<IActionResult> GetFavoriteCountriesByUserId([FromQuery] string userId)
        {
            try
            {
                List<FavoriteCountry> favoriteCountries = await _IFavoritesBR.GetAllFavoriteCountryByUserId(userId);

                return Ok(new GetFavoriteCountriesResponse() { Success = true, Countries = favoriteCountries });
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponse() { Success = false, Message = ApiMsgs.INF004 });
            }
        }

        [HttpPost("AddFavoriteCountryByUserId")]
        public async Task<IActionResult> AddFavoriteCountryByUserId([FromBody] CountryFavoriteRequest request)
        {
            try
            {
                FavoriteDTO favoriteDTO = new FavoriteDTO(request.UserId, request.CountryName);

                await _IFavoritesBR.CreateFavoriteCountry(favoriteDTO);

                return Ok(new BaseResponse() { Success = true, Message = ApiMsgs.INF005 });
            }
            catch (ApiException ex)
            {
                return BadRequest(new BaseResponse() { Success = false, Message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponse() { Success = false, Message = ApiMsgs.INF004 });
            }
        }

        [HttpDelete("DeleteFavoriteCountryByUserId")]
        public async Task<IActionResult> DeleteFavoriteCountryByUserId([FromBody] CountryFavoriteRequest request)
        {
            try
            {
                FavoriteDTO favoriteDTO = new FavoriteDTO(request.UserId, request.CountryName);

                await _IFavoritesBR.DeleteFavoriteCountry(favoriteDTO);

                return Ok(new BaseResponse() { Success = true, Message = ApiMsgs.INF006 });
            }
            catch (ApiException ex)
            {
                return BadRequest(new BaseResponse() { Success = false, Message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponse() { Success = false, Message = ApiMsgs.INF004 });
            }
        }

        #endregion
    }
}

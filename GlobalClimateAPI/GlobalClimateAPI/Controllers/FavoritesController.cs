using Core.Domain.DTOs;
using Core.Domain.Entities;
using Core.Domain.Exceptions;
using Core.Domain.Msgs;
using Core.Services.BusinessRules.Interfaces;
using GlobalClimateAPI.Requests;
using GlobalClimateAPI.Responses;
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

        [HttpGet("GetFavoriteCitiesByUserId")]
        public async Task<IActionResult> GetFavoriteCitiesByUserId([FromQuery] string userId)
        {
            try
            {
                List<FavoriteCity> favoriteCities = await _IFavoritesBR.GetAllFavoriteCityByUserId(userId);

                return Ok(new ApiResponse<List<FavoriteCity>>() { Success = true, Data = favoriteCities });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>() { Success = false, Message = ApiMsgs.INF004 });
            }
        }

        [HttpPost("AddFavoriteCityByUserId")]
        public async Task<IActionResult> AddFavoriteCityByUserId([FromBody] FavoriteRequest request)
        {
            try
            {
                FavoriteDTO favoriteDTO = new FavoriteDTO(request.UserId, request.CityName);

                await _IFavoritesBR.CreateFavoriteCity(favoriteDTO);

                return Ok(new ApiResponse<string>() { Success = true, Message = ApiMsgs.INF005 });
            }
            catch (ApiException ex)
            {
                return BadRequest(new ApiResponse<string>() { Success = false, Message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>() { Success = false, Message = ApiMsgs.INF004 });
            }
        }

        [HttpDelete("DeleteFavoriteCityByUserId")]
        public async Task<IActionResult> DeleteFavoriteCityByUserId([FromBody] FavoriteRequest request)
        {
            try
            {
                FavoriteDTO favoriteDTO = new FavoriteDTO(request.UserId, request.CityName);

                await _IFavoritesBR.DeleteFavoriteCity(favoriteDTO);

                return Ok(new ApiResponse<string>() { Success = true, Message = ApiMsgs.INF006 });
            }
            catch (ApiException ex)
            {
                return BadRequest(new ApiResponse<string>() { Success = false, Message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>() { Success = false, Message = ApiMsgs.INF004 });
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

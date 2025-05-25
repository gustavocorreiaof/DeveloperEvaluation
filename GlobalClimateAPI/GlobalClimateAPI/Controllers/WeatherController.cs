using Core.Domain.Exceptions;
using Core.Domain.Msgs;
using GlobalClimateAPI.Responses;
using GlobalClimateAPI.Responses.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json;

namespace GlobalClimateAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public WeatherController(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetWeatherResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponse))]
        [SwaggerOperation(Summary = "Returns information about a weather in a city.", Description = "Searches for information in the OpenWeather API based on the city's name provided by the user.")]
        public async Task<IActionResult> GetWeather([FromQuery, SwaggerParameter("The name of city to search and know his weather infos", Required = true)] string city)
        {
            try
            {
                var apiKey = _config["OpenWeather:ApiKey"];
                var url = string.Format(_config["Urls:OpenWeatherMap"]!, city, apiKey);

                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                    return NotFound(new BaseResponse() { Success = false, Message = ApiMsgs.INF001 });

                var json = await response.Content.ReadAsStringAsync();

                return Ok(new GetWeatherResponse() { Success = true, WeatherInfos = JsonDocument.Parse(json) .ToString()});
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
    }
}

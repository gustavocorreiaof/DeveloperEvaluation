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
    public class CountryController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public CountryController(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetCountryResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponse))]
        [SwaggerOperation(Summary = "Returns information about a country.", Description = "Searches for information in the RestCountries API based on the name provided by the user.")]
        public async Task<IActionResult> GetCountry([FromQuery, SwaggerParameter("The name of the country to retrieve.", Required = true)] string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                    throw new ApiException(ApiMsgs.INF003);

                var baseUrl = _config["Urls:RestCountries"];
                var url = $"{baseUrl}{name}";

                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                    return NotFound(new BaseResponse() { Success = false, Message = string.Format(ApiMsgs.INF002, name) });

                var json = await response.Content.ReadAsStringAsync();
                return Ok(new GetCountryResponse() { Success = true, CountryInfo = JsonDocument.Parse(json).ToString() });
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

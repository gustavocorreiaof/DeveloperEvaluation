using Core.Domain.Msgs;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace GlobalClimateAPI.Controllers
{
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
        public async Task<IActionResult> GetCountry([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest(ApiMsgs.INF003);

            var baseUrl = _config["Urls:RestCountries"];
            var url = $"{baseUrl}{name}";

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return NotFound(string.Format(ApiMsgs.INF002, name));

            var json = await response.Content.ReadAsStringAsync();
            return Ok(JsonDocument.Parse(json));
        }
    }
}

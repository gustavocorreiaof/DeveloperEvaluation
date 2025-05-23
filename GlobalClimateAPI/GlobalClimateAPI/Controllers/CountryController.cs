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

        public CountryController(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public async Task<IActionResult> GetCountry([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest(ApiMsgs.INF003);

            var url = $"https://restcountries.com/v3.1/name/{name}";

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return NotFound(string.Format(ApiMsgs.INF002, name));

            var json = await response.Content.ReadAsStringAsync();
            return Ok(JsonDocument.Parse(json));
        }
    }
}

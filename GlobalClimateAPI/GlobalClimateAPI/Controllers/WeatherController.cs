using Core.Domain.Msgs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GetWeather([FromQuery] string city)
        {
            var apiKey = _config["OpenWeather:ApiKey"];
            var url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric&lang=pt_br";

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return NotFound(ApiMsgs.INF001);

            var json = await response.Content.ReadAsStringAsync();

            return Ok(JsonDocument.Parse(json));
        }
    }
}

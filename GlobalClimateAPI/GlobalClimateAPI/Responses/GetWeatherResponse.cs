using GlobalClimateAPI.Responses.Base;
using Swashbuckle.AspNetCore.Annotations;

namespace GlobalClimateAPI.Responses
{
    public class GetWeatherResponse:BaseResponse
    {
        [SwaggerSchema("Contains the Weather's infos.")]
        public string WeatherInfos { get; set; }
    }
}

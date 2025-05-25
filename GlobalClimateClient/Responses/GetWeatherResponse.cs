using GlobalClimateAPI.Responses.Base;

namespace GlobalClimateAPI.Responses
{
    public class GetWeatherResponse:BaseResponse
    {
        public string WeatherInfos { get; set; }
    }
}

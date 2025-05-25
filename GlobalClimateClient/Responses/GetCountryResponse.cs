using GlobalClimateAPI.Responses.Base;

namespace GlobalClimateAPI.Responses
{
    public class GetCountryResponse: BaseResponse
    {
        public string CountryInfos { get; set; }
    }
}

using GlobalClimateAPI.Responses.Base;
using Swashbuckle.AspNetCore.Annotations;

namespace GlobalClimateAPI.Responses
{
    public class GetCountryResponse: BaseResponse
    {
        [SwaggerSchema("Contains the Country's infos.")]
        public string CountryInfo { get; set; }
    }
}

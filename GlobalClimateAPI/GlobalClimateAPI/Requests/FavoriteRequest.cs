using System.ComponentModel.DataAnnotations;

namespace GlobalClimateAPI.Requests
{
    public class FavoriteRequest
    {
        [Required]
        public required string UserId { get; set; }

        [Required]
        public required string CityName { get; set; }
    }
}

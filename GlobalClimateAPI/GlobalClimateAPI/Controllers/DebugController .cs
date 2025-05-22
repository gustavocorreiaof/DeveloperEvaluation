using Microsoft.AspNetCore.Mvc;

namespace GlobalClimateAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DebugController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAwsInfo()
        {
            var region = Environment.GetEnvironmentVariable("AWS_REGION");
            var accessKey = Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_ID");

            return Ok(new { region, accessKey });
        }
    }
}

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UdsideDownKittenGenerator.Services;

namespace UpsideDownKittenGenerator.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("[controller]")]
    public class KittenController : ControllerBase
    {
        private readonly IKittenService _kittenService;

        public KittenController(IKittenService kittenService)
        {
            _kittenService = kittenService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var stream = await _kittenService.GetKitten();

            return File(stream, "image/jpeg");
        }
    }
}

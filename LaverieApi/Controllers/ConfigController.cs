using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LaverieApi.Models.Services;
using LaverieApi.Models.Business;

namespace LaverieApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        private readonly IConfigService _gererConfigService;
        public ConfigController(IConfigService gererConfigService)
        {
            _gererConfigService = gererConfigService;
        }

        [HttpPost("init")]
        public IActionResult InitConfig()
        {
            string filePath = @"C:\Users\lenovo\Desktop\.net\config.json";
            _gererConfigService.InitConfig(filePath);
            return Ok("Fichier de configuration généré avec succès.");
        }
        [HttpGet]
        public async Task<IActionResult> GetConfig()
        {
            string filePath = @"C:\Users\lenovo\Desktop\.net\config.json";
            string json = await _gererConfigService.GetConfig(filePath);
            return Ok(json);
        }
    }
}

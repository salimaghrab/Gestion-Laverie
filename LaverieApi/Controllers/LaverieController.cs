using Microsoft.AspNetCore.Mvc;
using LaverieApi.Models.IDAO;
using LaverieDomain;
using LaverieApi.Filters;

namespace LaverieApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [JWTTokenAuthenticationFilter]
    public class LaverieController : ControllerBase
    {
        private readonly ILaverieDAO _laverieDAO;

        public LaverieController(ILaverieDAO laverieDAO)
        {
            _laverieDAO = laverieDAO;
        }

        [HttpPost]
        public IActionResult AddLaverie([FromBody] Laverie laverie)
        {
            _laverieDAO.AddLaverie(laverie);
            return Ok("Laverie ajoutée avec succès.");
        }

        [HttpGet]
        public IActionResult GetAllLaveries()
        {
            var laveries = _laverieDAO.GetAllLaveries();
            return Ok(laveries);
        }

        [HttpGet("ByProprietaire/{proprietaireId}")]
        public IActionResult GetLaveriesByProprietaireId(int proprietaireId)
        {
            var laveries = _laverieDAO.GetLaveriesByProprietaireId(proprietaireId);

            if (laveries == null || !laveries.Any())
            {
                return NotFound("Aucune laverie trouvée pour ce propriétaire.");
            }

            return Ok(laveries);
        }

    }
}

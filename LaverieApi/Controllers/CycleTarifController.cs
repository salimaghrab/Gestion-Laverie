using Microsoft.AspNetCore.Mvc;
using LaverieApi.Models.IDAO;
using LaverieDomain;

namespace LaverieApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CycleTarifController : ControllerBase
    {
        private readonly ICycleTarif _cycleTarifDAO;

        public CycleTarifController(ICycleTarif cycleTarifDAO)
        {
            _cycleTarifDAO = cycleTarifDAO;
        }

        [HttpPost]
        public IActionResult AddCycle([FromBody] CycleTarif cycle)
        {
            _cycleTarifDAO.AddCycle(cycle);
            return Ok("Cycle ajouté avec succès.");
        }

        [HttpGet]
        public IActionResult GetAllCycles()
        {
            var cycles = _cycleTarifDAO.GetAll();
            return Ok(cycles);
        }
        [HttpGet("ByMachine/{machineId}")]
        public IActionResult GetLaveriesByProprietaireId(int machineId)
        {
            var cycles = _cycleTarifDAO.GetCycleByMachine(machineId);

            if (cycles == null || !cycles.Any())
            {
                return NotFound("Aucune cycle pour ce machine trouvé.");
            }

            return Ok(cycles);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using LaverieApi.Models.IDAO;
using LaverieDomain;
using LaverieApi.Models.Business;

namespace LaverieApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MachineController : ControllerBase
    {
        private readonly GererMachine _gererMachine;

        public MachineController(GererMachine machineDAO)
        {
            _gererMachine = machineDAO;
        }

        [HttpPost]
        public IActionResult AddMachine([FromBody] Machine machine)
        {
            _gererMachine.AjouterMachine(machine);
            return Ok("Machine ajoutée avec succès.");
        }

        [HttpGet]
        public IActionResult GetAllMachines()
        {
            var machines = _gererMachine.GetAllMachines();
            return Ok(machines);
        }

        [HttpGet("ByLaverie/{laverieId}")]
        public IActionResult GetLaveriesByProprietaireId(int laverieId)
        {
            var machines = _gererMachine.GetMachinesByLaverieId(laverieId);

            if (machines == null || !machines.Any())
            {
                return NotFound("Aucune machine trouvé");
            }

            return Ok(machines);
        }
    }
}

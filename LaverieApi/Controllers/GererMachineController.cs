using LaverieApi.Models.Business;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LaverieApi.Models.Services;

namespace LaverieApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GererMachineController : ControllerBase
    {

        private readonly GererMachine _gererMachine;


        public GererMachineController(GererMachine gererMachine)
        {
            _gererMachine = gererMachine;
        }

        [HttpPost("demarrer")]
        public IActionResult DemarrerMachine(int machineId, int cycleId)
        {
            try
            {
                _gererMachine.DemarrerMachine(machineId, cycleId);
                return Ok($"La machine {machineId} a été démarrée avec succès.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erreur : {ex.Message}");
            }
        }

        [HttpPost("arreter")]
        public IActionResult ArreterMachine(int machineId)
        {
            try
            {
                _gererMachine.ArreterMachine(machineId);
                return Ok($"La machine {machineId} a été arrêtée avec succès.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erreur : {ex.Message}");
            }
        }
    }
}

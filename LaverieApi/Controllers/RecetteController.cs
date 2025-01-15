using LaverieApi.Models.Business;
using LaverieApi.Models.Services;
using LaverieDomain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LaverieApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecetteController : ControllerBase
    {
        private readonly GererRecette _recetteService;


        public RecetteController(GererRecette recetteService)
        {
            _recetteService = recetteService;
        }
        [HttpPost]

        public async Task<IActionResult> EnregistrerRecette([FromBody] RecetteMachine recette)
        {
            if (recette == null)
                return BadRequest("Les données de la recette sont invalides.");

            try
            {
                await _recetteService.AjouterOuMettreAJourRecetteAsync(recette.MachineId, recette.Montant, recette.Date);
                return Ok("Recette enregistrée avec succès.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur serveur : {ex.Message}");
            }
        }

        [HttpGet("machine/{machineId}/date/{date}")]
        public async Task<IActionResult> GetRecette(int machineId, DateTime date)
        {
            try
            {

                double recette = await _recetteService.ObtenirRecetteParMachineEtDateAsync(machineId, date);

                if (recette == 0)
                {
                    return NotFound($"Aucune recette trouvée pour la machine {machineId} à la date {date}.");
                }


                return Ok($"La recette pour la machine {machineId} à la date {date:yyyy-MM-dd} est de {recette} EUR.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur serveur : {ex.Message}");
            }
        }



    }
}

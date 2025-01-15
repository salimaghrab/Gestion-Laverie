using Microsoft.AspNetCore.Mvc;
using LaverieApi.Models.IDAO;
using LaverieDomain;


namespace LaverieApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ProprietaireController : ControllerBase
    {
        private readonly IProprietaireDAO _proprietaireDAO;

        public ProprietaireController(IProprietaireDAO proprietaireDAO)
        {
            _proprietaireDAO = proprietaireDAO;
        }

        [HttpPost]
        public IActionResult AddProprietaire([FromBody] Proprietaire proprietaire)
        {
            _proprietaireDAO.AddProprietaire(proprietaire);
            return Ok("Propriétaire ajouté avec succès.");
        }

        [HttpGet]
        
        public IActionResult GetAllProprietaires()
        {
            var proprietaires = _proprietaireDAO.GetAllProprietaire();
            return Ok(proprietaires);
        }
        [HttpGet("getprop")]
        public IActionResult GetProprietairebyEmail(string email)
        {
            var proprietaires = _proprietaireDAO.GetProprietaireByEmail(email);
            return Ok(proprietaires);
        }
    }
}

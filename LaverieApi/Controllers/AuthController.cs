using LaverieApi.Models.Services;
using Microsoft.AspNetCore.Mvc;
using LaverieDomain;

namespace LaverieApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class JWTAuthentificationController : ControllerBase
    {
        private JWTTokenManager tokenManager;

        public JWTAuthentificationController(JWTTokenManager tokenManager)
        {
            this.tokenManager = tokenManager;
        }
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateRequest request)
        {
            if (tokenManager.Authenticate(request.Email, request.Password, out Proprietaire proprietaire))
            {
                var token = tokenManager.NewToken(proprietaire);

                return Ok(new
                {
                    token,
                    proprietaire = new
                    {
                        id = proprietaire.Id,
                        email = proprietaire.Email,
                        nom = proprietaire.Nom,
                        prenom = proprietaire.Prenom
                    }
                });
            }
            else
            {
                ModelState.AddModelError("Unauthorised", "You are not authorized");
                return BadRequest();
            }
        }


    }
}

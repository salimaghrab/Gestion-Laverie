using LaverieApi.Models.Services;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using LaverieApi.Infrastructure.DAO;
using System.Security.Claims;

namespace LaverieApi.Filters
{
    public class JWTTokenAuthenticationFilter : Attribute, IAuthorizationFilter
    {
        //public void OnAuthorization(AuthorizationFilterContext context)
        //{
        //    // Récupérer le token du header Authorization
        //    string token = context.HttpContext.Request.Headers["Authorization"];

        //    if (string.IsNullOrEmpty(token))
        //    {
        //        context.Result = new UnauthorizedResult(); // Si pas de token, retourner Unauthorized
        //        return;
        //    }

        //    try
        //    {
        //        // Vérifier si le token commence par "Bearer " (cas courant pour les tokens JWT)
        //        if (token.StartsWith("Bearer "))
        //        {
        //            token = token.Substring(7); // Supprimer "Bearer " pour obtenir seulement le token
        //        }
        //        else
        //        {
        //            context.Result = new UnauthorizedResult(); // Si format incorrect, retourner Unauthorized
        //            return;
        //        }

        //        var jwtTokenManager = new JWTTokenManager();

        //        // Vérifier si le token est valide
        //        var claims = jwtTokenManager.VerifyToken(token);

        //        // Si le token est valide, afficher les claims
        //        Console.WriteLine("Token is valid. Claims:");
        //        foreach (var claim in claims.Claims)
        //        {
        //            Console.WriteLine($"Claim Type: {claim.Type}, Claim Value: {claim.Value}");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Si une erreur se produit (par exemple, token invalide ou expiré)
        //        Console.WriteLine($"Error: {ex.Message}");
        //        context.Result = new UnauthorizedResult(); // Retourner Unauthorized en cas d'erreur
        //    }
        //}

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string token = context.HttpContext.Request.Headers["Authorization"];

            if (string.IsNullOrEmpty(token))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            try
            {
                if (token.StartsWith("Bearer "))
                {
                    token = token.Substring(7);
                }
                else
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }

                var jwtTokenManager = new JWTTokenManager();
                var claimsPrincipal = jwtTokenManager.VerifyToken(token);

                // Extraire les informations du propriétaire depuis les claims
                var userId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var userEmail = claimsPrincipal.FindFirst(ClaimTypes.Email)?.Value;

                Console.WriteLine($"Authenticated User: ID = {userId}, Email = {userEmail}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                context.Result = new UnauthorizedResult();
            }
        }


    }
}

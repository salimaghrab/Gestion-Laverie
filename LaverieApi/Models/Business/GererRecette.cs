using LaverieApi.Infrastructure;
using LaverieApi.Models.IDAO;
using LaverieApi.Models.Services;
using LaverieDomain;
using MySql.Data.MySqlClient;
using System.Globalization;

namespace LaverieApi.Models.Business
{
    public class GererRecette
    {
        private readonly IRecetteDAO _recetteDAO;
        public GererRecette(IRecetteDAO recetteDAO)
        {
            _recetteDAO = recetteDAO;
        }
        public async Task AjouterOuMettreAJourRecetteAsync(int machineId, double montant, DateTime date)
        {
            await _recetteDAO.AjouterOuMettreAJourRecetteAsync(machineId, montant, date);
        }

        public async Task<double> ObtenirRecetteParMachineEtDateAsync(int machineId, DateTime date)
        {
            return await _recetteDAO.ObtenirRecetteParMachineEtDateAsync(machineId, date);
        }


    }
}


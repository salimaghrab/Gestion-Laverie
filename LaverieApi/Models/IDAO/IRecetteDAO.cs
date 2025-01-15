namespace LaverieApi.Models.IDAO
{
    public interface IRecetteDAO
    {
        Task AjouterOuMettreAJourRecetteAsync(int machineId, double montant, DateTime date);
        Task<double> ObtenirRecetteParMachineEtDateAsync(int machineId, DateTime date);
    }
}

using LaverieDomain;

namespace LaverieApi.Models.IDAO
{
    public interface IProprietaireDAO
    {
        // Méthode pour ajouter un propriétaire
        void AddProprietaire(Proprietaire proprietaire);

        // Méthode pour récupérer un propriétaire par ID
       // Proprietaire GetProprietaire(int id);

        // Méthode pour récupérer tous les propriétaires
        List<Proprietaire> GetAllProprietaire();
        public Proprietaire GetProprietaireByEmail(string email);
    }
}

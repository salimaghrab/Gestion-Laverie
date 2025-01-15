using LaverieDomain;

namespace LaverieApi.Models.IDAO
{
    public interface ILaverieDAO
    {
        
        void AddLaverie(Laverie laverie);
        //Laverie GetLaveries(int id);
        List<Laverie> GetAllLaveries();
        public List<Laverie> GetLaveriesByProprietaireId(int proprietaireId);
    }
}

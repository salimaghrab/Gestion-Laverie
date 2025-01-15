using LaverieDomain;
namespace LaverieApi.Models.IDAO
{
    public interface ICycleTarif
    {
        void AddCycle(CycleTarif cycle);
        //CycleTarif Get(int id);
        List<CycleTarif> GetAll();
        public List<CycleTarif> GetCycleByMachine(int MachineId);
    }
}

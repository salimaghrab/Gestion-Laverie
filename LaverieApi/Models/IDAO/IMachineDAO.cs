using LaverieDomain;
namespace LaverieApi.Models.IDAO
{
    public interface IMachineDAO
    {

        void AddMachine(Machine machine);
        //Machine GetMachine(int id);
        List<Machine> GetAllMachines();
        void DemarrerMachine(int machineId, int cycleId);
        void ArreterMachine(int machineId);
        List<Machine> GetMachinesByLaverieId(int LaverieId);
    }
}

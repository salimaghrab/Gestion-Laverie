using LaverieApi.Infrastructure;
using LaverieApi.Models.IDAO;
using LaverieApi.Models.Services;
using LaverieDomain;
using MySql.Data.MySqlClient; 

namespace LaverieApi.Models.Business
{
public class GererMachine
    {
        private readonly IMachineDAO _machineservices;
        public GererMachine(IMachineDAO _machineservice)
        {
            _machineservices = _machineservice;
        }
        public void AjouterMachine(Machine machine)
        {
            _machineservices.AddMachine(machine);
        }
        public List<Machine> GetAllMachines()
        {
            return _machineservices.GetAllMachines();
        }

        public void DemarrerMachine(int machineId, int cycleId)
        {
            _machineservices.DemarrerMachine(machineId, cycleId);
        }
        public void ArreterMachine(int machineId)
        {
            _machineservices.ArreterMachine(machineId);
        }
        public List<Machine> GetMachinesByLaverieId(int LaverieId)
        {
            return _machineservices.GetMachinesByLaverieId(LaverieId);
        }

    }
}

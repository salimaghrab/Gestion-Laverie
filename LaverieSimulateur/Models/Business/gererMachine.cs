using System.Threading.Tasks;
using LaverieSimulateur.Models.Services;

namespace LaverieSimulateur.Models.Business
{
    public class GererMachine
    {
        private readonly GererMachineService _machineService;

        public GererMachine()
        {
            _machineService = new GererMachineService();
        }

        public async Task DemarrerMachineAsync(int machineId,int cycleId)
        {
            await _machineService.DemarrerMachineAsync(machineId,cycleId);
        }

        public async Task ArreterMachineAsync(int machineId)
        {
            await _machineService.ArreterMachineAsync(machineId);
        }
    }
}

using LaverieDomain;
using LaverieSimulateur.Models.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaverieSimulateur.Models.Services
{
    internal class CycleSimulationService

    {
        private readonly GererMachine _gererMachine;
          
        public CycleSimulationService()
        {
            _gererMachine = new GererMachine();
        } 
        
       public async Task RunCycleAsync(int machineId, int duree)
        {
            Console.WriteLine($"Cycle démarré pour une durée totale de {duree} minutes...");

            for (int minute = 1; minute <= duree; minute++)
            {
                await Task.Delay(60 * 10);
                Console.WriteLine($"Cycle en cours : {minute}/{duree} minutes terminées.");
            }

            Console.WriteLine("\nLe cycle est terminé.");

            try
            {
                await _gererMachine.ArreterMachineAsync(machineId);
                Console.WriteLine("La machine a été arrêtée avec succès.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de l'arrêt de la machine : {ex.Message}");
            }
        }

    }
}

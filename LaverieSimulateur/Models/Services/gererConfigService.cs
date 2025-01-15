using LaverieDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using LaverieDomain;
using LaverieSimulateur.Models.Utils;
using System.Runtime.CompilerServices;

namespace LaverieSimulateur.Models.Services
{
    internal class gererConfigService
    {
        private readonly HttpClient _client;
        private const string BaseUrl = "https://localhost:7131/api/Config";
        public gererConfigService()
        {
            _client = new HttpClient();
        }
        public async Task InitConfigAsync()
        {
            await _client.PostAsync($"{BaseUrl}/init", null);
        }

        public async Task<DeserializeData> GetConfigAsync()
        {
            var response = await _client.GetStringAsync($"{BaseUrl}/");
            
     
            var configData = JsonConvert.DeserializeObject<DeserializeData>(response);


           
            foreach (var laverie in configData.Laveries)
            {
                laverie.Proprietaire = configData.Proprietaires.Find(p => p.Id == laverie.ProprietaireId); 
                laverie.Proprietaire?.Laveries.Add(laverie); 
            }

            
            foreach (var machine in configData.Machines)
            {
                machine.Laverie = configData.Laveries.Find(l => l.Id == machine.LaverieId); 
                machine.Laverie?.Machines.Add(machine); 
            }

           
            foreach (var cycle in configData.Cycles)
            {
                cycle.Machine = configData.Machines.Find(m => m.Id == cycle.MachineId); 
                cycle.Machine?.Cycles.Add(cycle); 
            }

            return configData;
        }




    }
}

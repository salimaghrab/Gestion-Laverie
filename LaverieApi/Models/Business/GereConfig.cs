using LaverieApi.Models.IDAO;
using LaverieApi.Models.Services;
using Newtonsoft.Json;
using System.Text;

namespace LaverieApi.Models.Business
{
    public class GereConfig : IConfigService
    {
        private readonly IProprietaireDAO _proprietaireDAO;
        private readonly ILaverieDAO _laverieDAO;
        private readonly IMachineDAO _machineDAO;
        private readonly ICycleTarif _cycleDAO;

        public GereConfig(IProprietaireDAO proprietaireDAO,
            ILaverieDAO laverieDAO,
            IMachineDAO machineDAO,
            ICycleTarif cycleDAO)
        {
            _proprietaireDAO = proprietaireDAO;
            _laverieDAO = laverieDAO;
            _machineDAO = machineDAO;
            _cycleDAO = cycleDAO;
        }

        public async void InitConfig(string outputFilePath)
        {
            var proprietaires = _proprietaireDAO.GetAllProprietaire();
            var laveries = _laverieDAO.GetAllLaveries();
            var machines = _machineDAO.GetAllMachines();
            var cycles = _cycleDAO.GetAll();

            var data = new
            {
                proprietaires,
                laveries,
                machines,
                cycles
            };

            string json = JsonConvert.SerializeObject(data, Formatting.Indented);

            // Assurer l'encodage UTF-8
            await File.WriteAllTextAsync(outputFilePath, json, Encoding.UTF8);
        }

        public async Task<string> GetConfig(string jsonFilePath)
        {
            if (!File.Exists(jsonFilePath))
            {
                return null;
            }

            // Lire le fichier avec encodage UTF-8
            using (var reader = new StreamReader(jsonFilePath, Encoding.UTF8))
            {
                return await reader.ReadToEndAsync();
            }
        }
    }
}

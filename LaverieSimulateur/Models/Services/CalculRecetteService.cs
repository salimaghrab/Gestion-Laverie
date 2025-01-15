using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LaverieDomain;
using System.Text.Json;
namespace LaverieSimulateur.Models.Services
{
    internal class CalculRecetteService
    {
        private Dictionary<int, double> _recettesDuJour; // MachineId -> Recette
        private DateTime _dernierJour;
        public CalculRecetteService() {
            _recettesDuJour = new Dictionary<int, double>();
            _dernierJour = DateTime.Today;
        }
        public async Task AjouterRecetteAsync(int machineId, double tarif)
        {
            // Vérifier si le jour a changé
            if (DateTime.Today > _dernierJour)
            {
                // Envoyer les recettes cumulées au Web API
                foreach (var recette in _recettesDuJour)
                {
                    await EnregistrerRecetteAsync(recette.Key, recette.Value, _dernierJour);
                }

                // Réinitialiser les données pour le nouveau jour
                _recettesDuJour.Clear();
                _dernierJour = DateTime.Today;
            }

            // Ajouter la recette au cumul
            if (!_recettesDuJour.ContainsKey(machineId))
            {
                _recettesDuJour[machineId] = 0;
            }

            _recettesDuJour[machineId] += tarif;
            Console.WriteLine($"Recette mise à jour pour la machine {machineId}: {_recettesDuJour[machineId]} €");
        }


        public async Task EnregistrerRecetteAsync(int machineId, double montant, DateTime date)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7131/api/Recette"); // Remplacez par l'URL réelle

                var recetteData = new
                {
                    MachineId = machineId,
                    Montant = montant,
                    Date = date
                };

                var jsonData = JsonSerializer.Serialize(recetteData);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                try
                {
                    var response = await client.PostAsync("", content);
                    response.EnsureSuccessStatusCode();

                    Console.WriteLine($"Recette de {montant} € pour la machine {machineId} enregistrée avec succès via l'API.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erreur lors de l'envoi des données au Web API : {ex.Message}");
                }
            }
        }


        public  async Task ObtenirRecetteAsync(int machineId)
        {
            
            string baseUrl = "https://localhost:7131/api/Recette";
            DateTime date = DateTime.Now;
            string formattedDate = date.ToString("yyyy-MM-dd");

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    
                    string url = $"{baseUrl}/machine/{machineId}/date/{formattedDate}";

                    Console.WriteLine($"Envoi de la requête à : {url}");

                    
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                       
                        string message = await response.Content.ReadAsStringAsync();

                        Console.WriteLine("Réponse de l'API :");
                        Console.WriteLine(message);
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        Console.WriteLine($"Aucune recette trouvée pour la machine {machineId} à la date {formattedDate}.");
                    }
                    else
                    {
                        Console.WriteLine($"Erreur lors de la requête : {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Erreur lors de l'appel à l'API : {ex.Message}");
                }
            }
        }


    }
}

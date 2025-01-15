using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
namespace LaverieSimulateur.Models.Services
{
   public class NotificationService
    {
        private readonly HttpClient _httpClient;

        public NotificationService()
        {
            // Initialisation du HttpClient
            _httpClient = new HttpClient();
        }

        public async Task SendMessageToOwnerAsync(string proprietaireId, string message)
        {
            using var httpClient = new HttpClient();
            var url = $"https://localhost:7131/api/WebSocket/send/{proprietaireId}";
            var content = new StringContent(JsonSerializer.Serialize(message), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Message envoyé au propriétaire avec l'ID {proprietaireId}.");
            }
            else
            {
                Console.WriteLine($"Erreur lors de l'envoi du message : {response.StatusCode}");
            }
        }
    }
}

using LaverieDomain;
using LaverieSimulateur.Models.Business;

class Program
{
    static async Task Main(string[] args)
    {
        var configService = new gererConfig();
        var menuManager = new MenuManager();

        Console.WriteLine("Initialisation de la configuration...");
        await configService.InitConfigAsync();
        Console.WriteLine("Configuration initialisée avec succès.\n");

        var deserializeData = await configService.GetConfigAsync();

        if (deserializeData == null || !deserializeData.Proprietaires.Any())
        {
            Console.WriteLine("Aucune donnée de configuration trouvée. Fin du programme.");
            return;
        }

        while (true)
        {
            Console.WriteLine("\n--- Menu Principal ---");
            Console.WriteLine("1. Démarrer une simulation");
            Console.WriteLine("2. Quitter");
            Console.Write("Votre choix : ");

            string input = Console.ReadLine();
            if (input == "2")
            {
                Console.WriteLine("Merci d'avoir utilisé le simulateur !");
                break;
            }

            if (input == "1")
            {
                try
                {
                    var proprietaire = MenuManager.SelectProprietaire(deserializeData.Proprietaires);
                    var laverie = MenuManager.SelectLaverie(proprietaire);
                    var machine = MenuManager.SelectMachine(laverie);
                    await menuManager.SelectCycleAsync(machine, proprietaire);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Une erreur s'est produite : {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Choix invalide. Veuillez réessayer.");
            }
        }
    }
}

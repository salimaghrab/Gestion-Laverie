using LaverieDomain;
using LaverieSimulateur.Models.Business;
using LaverieSimulateur.Models.Services;
using System.Net.Http;
using System.Text.Json;
using System.Text;

internal class MenuManager
{
    private readonly GererMachine _gererMachine;
    private readonly NotificationService _notificationService;
    private readonly CycleSimulationService _cycleSimulationService;
    private readonly CalculRecetteService _calculRecette;

    public MenuManager()
    {
        _gererMachine = new GererMachine();
        _notificationService = new NotificationService();
        _cycleSimulationService = new CycleSimulationService();
        _calculRecette = new CalculRecetteService();

    }

    public static int GetUserChoice(int maxOption)
    {
        int choice;
        do
        {
            Console.Write("Votre choix : ");
        } while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > maxOption);
        return choice;
    }

    public static Proprietaire SelectProprietaire(List<Proprietaire> proprietaires)
    {
        Console.WriteLine("Sélectionnez un propriétaire :");
        for (int i = 0; i < proprietaires.Count; i++)
            Console.WriteLine($"{i + 1}. {proprietaires[i].Nom} {proprietaires[i].Prenom}");

        int choice = GetUserChoice(proprietaires.Count);
        return proprietaires[choice - 1];
    }

    public static Laverie SelectLaverie(Proprietaire proprietaire)
    {
        Console.WriteLine("\nSélectionnez une laverie :");
        for (int i = 0; i < proprietaire.Laveries.Count; i++)
            Console.WriteLine($"{i + 1}. {proprietaire.Laveries[i].Nom}");

        int choice = GetUserChoice(proprietaire.Laveries.Count);
        return proprietaire.Laveries[choice - 1];
    }

    public static Machine SelectMachine(Laverie laverie)
    {
        Console.WriteLine("\nSélectionnez une machine :");
        for (int i = 0; i < laverie.Machines.Count; i++)
            Console.WriteLine($"{i + 1}. {laverie.Machines[i].Nom} - Modèle : {laverie.Machines[i].Modele}");

        int choice = GetUserChoice(laverie.Machines.Count);
        return laverie.Machines[choice - 1];
    }

    public async Task SelectCycleAsync(Machine machine, Proprietaire proprietaire)
    {
        Console.WriteLine("\nSélectionnez un cycle :");
        for (int i = 0; i < machine.Cycles.Count; i++)
            Console.WriteLine($"{i + 1}. {machine.Cycles[i].Id} - Durée : {machine.Cycles[i].duree} min - Tarif : {machine.Cycles[i].Tarif} €");

        int choice = GetUserChoice(machine.Cycles.Count);
        var cycle = machine.Cycles[choice - 1];

        Console.WriteLine($"\nDémarrage du cycle '{cycle.Id}' pour la machine '{machine.Nom}'...");
        Console.WriteLine($"Machine ID: {machine.Id}, Cycle ID: {cycle.Id}");

        string proprietaireId = proprietaire.Id.ToString();
        string message = $"Machine ID: {machine.Id} démarrée avec le cycle ID: {cycle.Id}.";

        await _gererMachine.DemarrerMachineAsync(machine.Id, cycle.Id);
        
        await _notificationService.SendMessageToOwnerAsync(proprietaire.Id.ToString(), message);
        await _cycleSimulationService.RunCycleAsync(machine.Id, cycle.duree);
        await _calculRecette.AjouterRecetteAsync(machine.Id, cycle.Tarif);
        await _calculRecette.EnregistrerRecetteAsync(machine.Id,cycle.Tarif,DateTime.Now);
        await _calculRecette.ObtenirRecetteAsync(machine.Id);



    }


   

}

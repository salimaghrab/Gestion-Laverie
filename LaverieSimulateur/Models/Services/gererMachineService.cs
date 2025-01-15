internal class GererMachineService
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "https://localhost:7131/api/GererMachine";

    public GererMachineService()
    {
        _httpClient = new HttpClient();
    }

    public async Task DemarrerMachineAsync(int machineId, int cycleId)
    {
        
        var url = $"{BaseUrl}/demarrer?machineId={machineId}&cycleId={cycleId}";

        var response = await _httpClient.PostAsync(url, null);
        response.EnsureSuccessStatusCode();
    }

    public async Task ArreterMachineAsync(int machineId)
    {
        
        var url = $"{BaseUrl}/arreter?machineId={machineId}";

        
        var response = await _httpClient.PostAsync(url, null);
        response.EnsureSuccessStatusCode();
    }
}

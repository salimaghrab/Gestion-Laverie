namespace LaverieApi.Models.Services
{
    public interface IConfigService
    {
        public void InitConfig(string outputFilePath);
        Task<string> GetConfig(string jsonFilePath);
    }
}

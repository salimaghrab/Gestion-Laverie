using LaverieDomain;
using LaverieSimulateur.Models.Services;
using LaverieSimulateur.Models.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaverieSimulateur.Models.Business
{
    internal class gererConfig
    {
        private readonly gererConfigService  _ConfigHttpClient;

        public gererConfig()
        {
            _ConfigHttpClient = new gererConfigService();
        }

        public async Task InitConfigAsync()
        {
            await _ConfigHttpClient.InitConfigAsync();
        }
        public async Task<DeserializeData> GetConfigAsync()
        {
            return await _ConfigHttpClient.GetConfigAsync();
        }
    }
}

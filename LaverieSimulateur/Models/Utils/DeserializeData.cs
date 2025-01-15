using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LaverieDomain;

namespace LaverieSimulateur.Models.Utils
{
   public class DeserializeData
    {
        public List<Proprietaire> Proprietaires { get; set; }
        public List<Laverie> Laveries { get; set; }
        public List<Machine> Machines { get; set; }
        public List<CycleTarif> Cycles { get; set; }
    }
}

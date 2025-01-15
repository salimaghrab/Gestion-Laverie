using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaverieDomain
{
    public class Machine
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Modele { get; set; }
        public bool Etat { get; set; }
        public int LaverieId { get; set; }
        public Laverie   Laverie { get; set; } 
        public List<CycleTarif> Cycles { get; set; } = new List<CycleTarif>();
    }
}

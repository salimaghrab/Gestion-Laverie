using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace LaverieDomain
{
    public class Laverie
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Adresse { get; set; }
        public int ProprietaireId { get; set; }
        public Proprietaire Proprietaire { get; set; }
        public List<Machine> Machines { get; set; } = new List<Machine>();
    }
}

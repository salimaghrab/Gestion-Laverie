using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaverieDomain
{
    public class CycleTarif
    {
        public int Id { get; set; }
        public int duree { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public int MachineId { get; set; }
        public Machine Machine { get; set; } // Référence à la machine
        public double Tarif { get; set; }
    }
}

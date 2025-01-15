using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaverieDomain
{
    public class RecetteMachine
    {
        public int Id { get; set; }
        public int MachineId { get; set; } // Identifiant de la machine
        public double Montant { get; set; } // Montant de la recette
        public DateTime Date { get; set; } // Date de la recette
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaverieDomain
{
    public class Proprietaire
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Adresse { get; set; }
        public string Password { get; set; }
        public List<Laverie> Laveries { get; set; } = new List<Laverie>();
    }
}

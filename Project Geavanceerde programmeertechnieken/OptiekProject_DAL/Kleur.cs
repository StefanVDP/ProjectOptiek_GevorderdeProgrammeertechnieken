using System;
using System.Collections.Generic;
using System.Text;

namespace OptiekProject_DAL
{
    public class Kleur
    {
        public int KleurID { get; set;}
        public string Naam { get; set; }
        public ICollection<Kleurcombinatie> Kleurcombinaties { get; set; }
    }
}

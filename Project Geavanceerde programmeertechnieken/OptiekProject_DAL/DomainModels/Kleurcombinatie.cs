using System;
using System.Collections.Generic;
using System.Text;

namespace OptiekProject_DAL.DomainModels
{
    public class Kleurcombinatie
    {
        public int KleurCombinatieID { get; set; }
        public int KleurID { get; set; }
        public Kleur kleur { get; set; }
        public int ModelID { get; set; }
        public Model Model { get; set; }
    }
}

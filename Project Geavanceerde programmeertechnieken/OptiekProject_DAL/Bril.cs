using System;
using System.Collections.Generic;
using System.Text;

namespace OptiekProject_DAL
{
    public class Bril
    {
        public int BrilID { get; set; }
        public string Naam { get; set; }
        public string Omschrijving { get; set; }
        public int Korting { get; set; }
        public bool Beschikbaar { get; set; }
        public int ModelID { get; set; }
        public Model Model { get; set; }
        public int BriltypeID { get; set; }
        public BrilType BrilTypes { get; set; }
        public int SterkteID { get; set; }
        public Sterkte Sterkte { get; set; }
    }
}

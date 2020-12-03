using System;
using System.Collections.Generic;
using System.Text;

namespace OptiekProject_DAL
{
    public class BrilType
    {
        public int BriltypeID { get; set; }
        public string Omschrijving { get; set; }
        public string Naam { get; set; }
        public ICollection<Bril> Brillen { get; set; }
    }
}

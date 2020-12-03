using System;
using System.Collections.Generic;
using System.Text;

namespace OptiekProject_DAL
{
    public class Model
    {
        public int ModelID { get; set; }
        public string Naam { get; set; }
        public int MerkID { get; set; }
        public Merk Merk { get; set; }
        public ICollection<Kleurcombinatie> Kleurcombinaties { get; set; }
        public ICollection<Bril> Brillen { get; set; }
    }
}

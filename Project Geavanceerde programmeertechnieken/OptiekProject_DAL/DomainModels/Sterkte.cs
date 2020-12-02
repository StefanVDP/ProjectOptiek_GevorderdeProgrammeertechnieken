using System;
using System.Collections.Generic;
using System.Text;

namespace OptiekProject_DAL.DomainModels
{
    public class Sterkte
    {
        public int SterkteID { get; set; }
        public string sterkte { get; set; }
        public ICollection<Bril> Brillen { get; set; }
    }
}

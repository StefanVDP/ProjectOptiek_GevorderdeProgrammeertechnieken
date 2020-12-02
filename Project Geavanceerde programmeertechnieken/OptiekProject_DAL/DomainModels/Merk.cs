using System;
using System.Collections.Generic;
using System.Text;

namespace OptiekProject_DAL.DomainModels
{
    public class Merk
    {
        public int MerkID { get; set; }
        public string Naam { get; set; }
        public DateTime Oprichtingsdatum { get; set; }
        public string Omschrijving { get; set; }
        public ICollection<Model> Modellen { get; set; }
    }
}

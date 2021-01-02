using System;
using OptiekProject_DAL.BasisModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiekProject_DAL
{
    [Table("Modellen")]
    public class Model : Basisklasse
    {
        public int ModelID { get; set; }

        public decimal Prijs { get; set; }

        [Required]
        [MaxLength(150)]
        public string Naam { get; set; }

        [MaxLength(5000)]
        public string Omschrijving { get; set; }

        [Required]
        public int BriltypeID { get; set; }
        public BrilType BrilTypes { get; set; }

        [Required]
        public int MerkID { get; set; }
        public Merk Merk { get; set; }
        public ICollection<Kleurcombinatie> Kleurcombinaties { get; set; }
        public ICollection<Bril> Brillen { get; set; }
    }
}

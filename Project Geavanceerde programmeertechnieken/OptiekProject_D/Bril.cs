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
    [Table("Brillen")]
    public class Bril : Basisklasse
    {
        public int BrilID { get; set; }
        [Required]
        [MaxLength(150)]
        public string Naam { get; set; }
        public int Korting { get; set; }
        [Required]
        public bool Beschikbaar { get; set; }

        [Required]
        public int ModelID { get; set; }
        public Model Model { get; set; }
        [Required]
        public int SterkteID { get; set; }
        public Sterkte Sterkte { get; set; }
    }
}

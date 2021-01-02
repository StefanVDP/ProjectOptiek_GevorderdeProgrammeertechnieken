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
    [Table("Kleurcombinaties")]
    public class Kleurcombinatie : Basisklasse
    {
        public int KleurCombinatieID { get; set; }

        [Required]
        public int KleurID { get; set; }
        public Kleur Kleur { get; set; }

        [Required]
        public int ModelID { get; set; }
        public Model Model { get; set; }
    }
}

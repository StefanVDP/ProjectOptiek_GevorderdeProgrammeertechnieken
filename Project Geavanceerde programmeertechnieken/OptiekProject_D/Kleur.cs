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
    [Table("Kleuren")]
    public class Kleur : Basisklasse
    {
        public int KleurID { get; set;}
        [Required]
        [MaxLength(150)]
        public string Naam { get; set; }
        public ICollection<Kleurcombinatie> Kleurcombinaties { get; set; }
    }
}

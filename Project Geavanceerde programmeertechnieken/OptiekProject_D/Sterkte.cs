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
    [Table("Sterktes")]
    public class Sterkte : Basisklasse
    {
        public int SterkteID { get; set; }

        public decimal Prijs { get; set; }

        [Required]
        [MaxLength(150)]
        public string sterkte { get; set; }
        public ICollection<Bril> Brillen { get; set; }
    }
}

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
    [Table("BrilTypes")]
    public class BrilType : Basisklasse
    {
        public int BriltypeID { get; set; }
        [MaxLength(5000)]
        public string Omschrijving { get; set; }
        [Required]
        [MaxLength(150)]
        public string Naam { get; set; }
        public ICollection<Model> Modellen { get; set; }
    }
}

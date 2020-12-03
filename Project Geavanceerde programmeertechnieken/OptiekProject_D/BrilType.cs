using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiekProject_DAL
{
    [Table("BrilTypes")]
    public class BrilType
    {
        public int BriltypeID { get; set; }
        public string Omschrijving { get; set; }
        [Required]
        public string Naam { get; set; }
        public ICollection<Bril> Brillen { get; set; }
    }
}

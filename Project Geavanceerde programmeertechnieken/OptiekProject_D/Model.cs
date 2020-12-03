using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiekProject_DAL
{
    [Table("Modellen")]
    public class Model
    {
        public int ModelID { get; set; }

        [Required]
        public string Naam { get; set; }

        [Required]
        public int MerkID { get; set; }
        public Merk Merk { get; set; }
        public ICollection<Kleurcombinatie> Kleurcombinaties { get; set; }
        public ICollection<Bril> Brillen { get; set; }
    }
}

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
    public class Sterkte
    {
        public int SterkteID { get; set; }

        [Required]
        public string sterkte { get; set; }
        public ICollection<Bril> Brillen { get; set; }
    }
}

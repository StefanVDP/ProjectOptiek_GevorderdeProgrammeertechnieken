using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiekProject_DAL
{
    [Table("Merken")]
    public class Merk
    {
        public int MerkID { get; set; }

        [Required]
        public string Naam { get; set; }
        public DateTime? Oprichtingsdatum { get; set; }
        public string Omschrijving { get; set; }
        public ICollection<Model> Modellen { get; set; }
    }
}

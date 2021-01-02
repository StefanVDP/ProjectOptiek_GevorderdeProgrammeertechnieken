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
    [Table("GPGebruikers")]
    public class Gebruiker : Basisklasse
    {
        public int GebruikerID { get; set; }
        [Required]
        public string Mail { get; set; }
        [Required]
        public string Wachtwoord { get; set; }

        [Required]
        public int GebruikerTypeID { get; set; }
        public GebruikerType GebruikerType { get; set; }
    }
}

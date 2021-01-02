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
    [Table("GebruikerTypes")]
    public class GebruikerType : Basisklasse
    {
        public int GebruikerTypeID { get; set; }
        [Required]
        public string naam { get; set; }
        public ICollection<Gebruiker> Gebruikers { get; set; }
    }
}

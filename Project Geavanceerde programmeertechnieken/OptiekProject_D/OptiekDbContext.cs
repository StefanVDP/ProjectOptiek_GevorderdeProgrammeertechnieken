using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiekProject_DAL
{
    public class OptiekDbContext: DbContext
    {
        public OptiekDbContext(): base ("name=BrilbeheerDBConnectionString") 
        { 
        
        }

        public DbSet<Bril> Brillen { get; set; }
        public DbSet<BrilType> BrilTypes { get; set; }
        public DbSet<Kleur> Kleuren { get; set; }
        public DbSet<Kleurcombinatie> Kleurcombinaties { get; set; }
        public DbSet<Merk> Merken { get; set; }
        public DbSet<Model> Modellen { get; set; }
        public DbSet<Sterkte> Sterkten { get; set; }
        public DbSet<Gebruiker> GPGebruikers { get; set; }
        public DbSet<GebruikerType> GebruikerTypes { get; set; }
    }
}

using OptiekProject_DAL.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiekProject_DAL.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Bril> BrilRepo { get; }
        IRepository<BrilType> BrilTypeRepo { get; }
        IRepository<Gebruiker> GebruikerRepo { get; }
        IRepository<GebruikerType> GebruikerTypeRepo { get; }
        IRepository<Kleur> KleurRepo { get; }
        IRepository<Kleurcombinatie> KleurcombinatieRepo { get; }
        IRepository<Merk> MerkRepo { get; }
        IRepository<Model> ModelRepo { get; }
        IRepository<Sterkte> SterkteRepo { get; }
        int Save();
    }
}

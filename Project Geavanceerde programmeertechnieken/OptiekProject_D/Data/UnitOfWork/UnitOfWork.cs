using OptiekProject_DAL.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiekProject_DAL.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {

        private IRepository<Bril> _brilRepo;
        private IRepository<BrilType> _briltypeRepo;
        private IRepository<Gebruiker> _gebruikerRepo;
        private IRepository<GebruikerType> _gebruikertypeRepo;
        private IRepository<Kleur> _kleurRepo;
        private IRepository<Kleurcombinatie> _kleurcombinatieRepo;
        private IRepository<Merk> _merkRepo;
        private IRepository<Model> _modelRepo;
        private IRepository<Sterkte> _sterkteRepo;

        public UnitOfWork(OptiekDbContext OptiekEntities)
        {
            this.OptiekEntities = OptiekEntities;

        }
        private OptiekDbContext OptiekEntities { get; }


        public IRepository<Bril> BrilRepo
        {
            get
            {
                if (_brilRepo == null)
                {
                    _brilRepo = new Repository<Bril>(this.OptiekEntities);
                }
                return _brilRepo;
            }
        }

        public IRepository<BrilType> BrilTypeRepo
        {
            get
            {
                if (_briltypeRepo == null)
                {
                    _briltypeRepo = new Repository<BrilType>(this.OptiekEntities);
                }
                return _briltypeRepo;
            }
        }

        public IRepository<Gebruiker> GebruikerRepo
        {
            get
            {
                if (_gebruikerRepo == null)
                {
                    _gebruikerRepo = new Repository<Gebruiker>(this.OptiekEntities);
                }
                return _gebruikerRepo;
            }
        }

        public IRepository<GebruikerType> GebruikerTypeRepo
        {
            get
            {
                if (_gebruikertypeRepo == null)
                {
                    _gebruikertypeRepo = new Repository<GebruikerType>(this.OptiekEntities);
                }
                return _gebruikertypeRepo;
            }
        }

        public IRepository<Kleur> KleurRepo
        {
            get
            {
                if (_kleurRepo == null)
                {
                    _kleurRepo = new Repository<Kleur>(this.OptiekEntities);
                }
                return _kleurRepo;
            }
        }

        public IRepository<Kleurcombinatie> KleurcombinatieRepo
        {
            get
            {
                if (_kleurcombinatieRepo == null)
                {
                    _kleurcombinatieRepo = new Repository<Kleurcombinatie>(this.OptiekEntities);
                }
                return _kleurcombinatieRepo;
            }
        }

        public IRepository<Merk> MerkRepo
        {
            get
            {
                if (_merkRepo == null)
                {
                    _merkRepo = new Repository<Merk>(this.OptiekEntities);
                }
                return _merkRepo;
            }
        }

        public IRepository<Model> ModelRepo
        {
            get
            {
                if (_modelRepo == null)
                {
                    _modelRepo = new Repository<Model>(this.OptiekEntities);
                }
                return _modelRepo;
            }
        }

        public IRepository<Sterkte> SterkteRepo
        {
            get
            {
                if (_sterkteRepo == null)
                {
                    _sterkteRepo = new Repository<Sterkte>(this.OptiekEntities);
                }
                return _sterkteRepo;
            }
        }

        public void Dispose()
        {
            OptiekEntities.Dispose();
        }

        public int Save()
        {
            return OptiekEntities.SaveChanges();
        }
    }
}

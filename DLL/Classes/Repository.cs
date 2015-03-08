using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Classes
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private IUnitOfWork _unit;
        public Repository(IUnitOfWork iunit)
        {
            if (iunit != null)
                _unit = iunit;
            else
                _unit = new UnitOfWork();
        }
        public void Add(T entity)
        {
            _unit.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _unit.Set<T>().Remove(entity);
        }

        public void SaveChanges()
        {
            _unit.SaveMyChanges();
        }
        public ICollection<T> Get()
        {
            return _unit.Set<T>().ToList<T>();
        }
    }
}

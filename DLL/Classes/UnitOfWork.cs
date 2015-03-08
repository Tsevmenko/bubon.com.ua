using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Classes
{
    public class UnitOfWork : IUnitOfWork
    {
        IDbContext _context;
        public UnitOfWork()
        {
            _context = new CustomEntities();
        }

        public DbSet<T> Set<T>() where T : class
        {
            return _context.Set<T>();
        }

        public void SaveMyChanges()
        {
            _context.SaveMyChanges();
        }
    }
}

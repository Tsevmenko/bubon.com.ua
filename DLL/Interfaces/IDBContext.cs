using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    internal interface IDbContext : IDisposable
    {
        DbSet<T> Set<T>() where T : class;
        void SaveMyChanges();
    }
}

using DAL.Interfaces;
using DLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Classes
{
    internal class CustomEntities : uh343423_dbEntities, IDbContext
    {
        System.Data.Entity.DbSet<T> IDbContext.Set<T>()
        {
            return this.Set<T>();
        }

        public void SaveMyChanges()
        {
            this.SaveChanges();
        }
    }
}

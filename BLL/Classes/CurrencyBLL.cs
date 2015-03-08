using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLL;
using DAL.Classes;
using DAL.Interfaces;
using BLL.Interfaces;
using ViewModels;

namespace BLL.Classes
{
    internal class CurrencyBLL : ICurrency
    {
        private Repository<Currency> repository { get; set; }

        public CurrencyBLL(Repository<Currency> repository)
        {
            this.repository = repository;
        }

        public int Add(string name)
        {
            Currency entity = new Currency()
            {
             Name = name
            };
            repository.Add(entity);
            repository.SaveChanges();
            return entity.Id;
        }

        public bool Del(int id)
        {
            var res = repository.Get().FirstOrDefault(p=>p.Id == id);
            if (res != null)
            {
                repository.Delete(res);
                repository.SaveChanges();
                return true;
            }
            else 
            {
                return false;
            }
        }

        public bool Update(int id, string name)
        {
            var res = repository.Get().FirstOrDefault(p => p.Id == id);
            if (res != null)
            {
                res.Name = name;
                repository.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<CurrencyVM> Get()
        {
            return repository.Get().Select(p => new CurrencyVM()
            {
                id = p.Id,
                name = p.Name
            }).ToList();
        } 
    }
}

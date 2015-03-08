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
    internal class LanguageBLL : ILanguage
    {
        private Repository<Language> repository { get; set; }

        public LanguageBLL(Repository<Language> repository)
        {
            this.repository = repository;
        }

        public int Add(string name, bool isDefault)
        {
            Language entity = new Language()
            {
                Name = name,
                IsItDefault = isDefault
            };
            repository.Add(entity);
            repository.SaveChanges();
            return entity.Id;
        }

        public bool Del(int id)
        {
            var res = repository.Get().FirstOrDefault(p => p.Id == id);
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

        public bool Update(int id, string name, bool isDefault)
        {
            var res = repository.Get().FirstOrDefault(p => p.Id == id);
            if (res != null)
            {
                res.Name = name;
                res.IsItDefault = isDefault;
                repository.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<LanguageVM> Get()
        {
            return repository.Get().Select(p => new LanguageVM()
            {
                id = p.Id,
                name = p.Name,
                isDefault = p.IsItDefault
            }).ToList();
        }
    }
}

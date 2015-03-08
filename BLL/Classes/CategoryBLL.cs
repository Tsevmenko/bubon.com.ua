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
    internal class CategoryBLL : ICategory
    {
        private Repository<Category> repository { get; set; }
        private Repository<CategoryDescription> repositoryDescription { get; set; }

        public CategoryBLL(Repository<Category> repository, Repository<CategoryDescription> repositoryDescription)
        {
            this.repository = repository;
            this.repositoryDescription = repositoryDescription;
        }

        public int Add(string name, int languageId, int? parentCategory)
        {
            Category entity = new Category();
            entity.ParentCategoryID = parentCategory;
            repository.Add(entity);
            repository.SaveChanges();
            CategoryDescription entityDescription = new CategoryDescription()
            {
                LanguageId = languageId,
                Name = name,
                CategoryId = entity.Id//catch for null id
            };
            repositoryDescription.Add(entityDescription);
            repository.SaveChanges();
            return entity.Id;
        }

        public bool Del(int id)
        {
            var res = repository.Get().FirstOrDefault(p => p.Id == id);
            if (res != null)
            {
                var resDescription = repositoryDescription.Get().Where(p => p.CategoryId == id).AsEnumerable();
                foreach (var el in resDescription)
                {
                    repositoryDescription.Delete(el);
                }
                repository.Delete(res);
                repository.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Update(int id, string name, int languageId, int? parentCategory)
        {
            var res = repository.Get().FirstOrDefault(p => p.Id == id);
            if (res == null) return false;
            if(res.ParentCategoryID == null)
                res.ParentCategoryID = parentCategory;
            if (res != null)
            {
                var resDescription = repositoryDescription.Get().FirstOrDefault(p => p.CategoryId == id && p.LanguageId == languageId);
                if (resDescription != null)
                {
                    
                    resDescription.Name = name;
                    repository.SaveChanges();
                    return true;
                }
                else
                {
                    resDescription = new CategoryDescription()
                    {
                        CategoryId = id,
                        LanguageId = languageId,
                        Name = name
                    };
                    repositoryDescription.Add(resDescription);
                    repositoryDescription.SaveChanges();
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        public List<CategoryVM> Get(int languageId)
        {
            return (from c in repository.Get()
                    join o in repositoryDescription.Get()
                         on languageId equals o.LanguageId
                         into result
                    select new CategoryVM()
                    {
                        id = c.Id,
                        name = (c.CategoryDescriptions.Count() > 0) ? c.CategoryDescriptions.FirstOrDefault(p => p.LanguageId == languageId).Name : "",
                        languageId = languageId,
                        parentCategory = c.ParentCategoryID
                    }).ToList();
        }
    }
}

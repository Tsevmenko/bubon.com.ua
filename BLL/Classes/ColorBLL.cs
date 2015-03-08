using BLL.Interfaces;
using DAL.Classes;
using DLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace BLL.Classes
{
    public class ColorBLL : IColor
    {
        private Repository<Product> repositoryProduct { get; set; }
        private Repository<Color> repository { get; set; }

        public ColorBLL(Repository<Color> repository, Repository<Product> repositoryProduct)
        {
            this.repository = repository;
            this.repositoryProduct = repositoryProduct;
        }

        public bool Add(int productId, string name, string value, bool isActive)
        {
            var product = repositoryProduct.Get().FirstOrDefault(p => p.Id == productId);
            if (product != null)
            {
                repository.Add(new Color()
                {
                    Name = name,
                    ProductId = productId,
                    IsActive = isActive,
                    Value = value
                });
                repository.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Del(int id)
        {
            var photo = repository.Get().FirstOrDefault(p => p.Id == id);
            if (photo != null)
            {
                repository.Delete(photo);
                repository.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Update(int id, int productId, string name, string value, bool isActive)
        {
            var photo = repository.Get().FirstOrDefault(p => p.Id == id);
            if (photo != null)
            {
                photo.Name = name;
                photo.ProductId = productId;
                photo.Value = value;
                photo.IsActive = isActive;
                repository.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<ColorVM> Get(int productId)
        {
            return repository.Get().Select(p => new ColorVM()
            {
                id = p.Id,
                name = p.Name,
                productId = p.ProductId,
                isActive = p.IsActive,
                value = p.Value
            }).ToList();
        }
    }
}

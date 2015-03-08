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
    public class SizeBLL : ISize
    {
        private Repository<Product> repositoryProduct { get; set; }
        private Repository<Size> repository { get; set; }

        public SizeBLL(Repository<Size> repository, Repository<Product> repositoryProduct)
        {
            this.repository = repository;
            this.repositoryProduct = repositoryProduct;
        }

        public bool Add(int productId, string name, bool isActive)
        {
            var product = repositoryProduct.Get().FirstOrDefault(p => p.Id == productId);
            if (product != null)
            {
                repository.Add(new Size()
                {
                    Name = name,
                    ProductId = productId,
                    IsActive = isActive
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
            var size = repository.Get().FirstOrDefault(p => p.Id == id);
            if (size != null)
            {
                repository.Delete(size);
                repository.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Update(int id, int productId, string name, bool isActive)
        {
            var photo = repository.Get().FirstOrDefault(p => p.Id == id);
            if (photo != null)
            {
                photo.Name = name;
                photo.ProductId = productId;
                photo.IsActive = isActive;
                repository.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<SizeVM> Get(int productId)
        {
            return repository.Get().Select(p => new SizeVM()
            {
                id = p.Id,
                name = p.Name,
                productId = p.ProductId,
                isActive = p.IsActive
            }).ToList();
        }
    }
}

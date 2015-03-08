using BLL.Interfaces;
using DAL.Classes;
using DLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Views;

namespace BLL.Classes
{
    public class PhotoBLL : IPhoto
    {
        private Repository<Product> repositoryProduct { get; set; }
        private Repository<Photo> repository { get; set; }

        public PhotoBLL(Repository<Photo> repository, Repository<Product> repositoryProduct)
        {
            this.repository = repository;
            this.repositoryProduct = repositoryProduct;
        }

        public bool Add(int productId, string name)
        {
            var product = repositoryProduct.Get().FirstOrDefault(p => p.Id == productId);
            if (product != null)
            {
                repository.Add(new Photo()
                {
                    Name = name,
                    ProductId = productId
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

        public bool Update(int id, int productId, string name)
        {
            var photo = repository.Get().FirstOrDefault(p => p.Id == id);
            if (photo != null)
            {
                photo.Name = name;
                photo.ProductId = productId;
                repository.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public IEnumerable<Photo> Get(int productId)
        {
            return repository.Get().Where(p=>p.ProductId == productId/*p => new PhotoVM()
            {
                id = p.Id,
                name = p.Name,
                productId = p.ProductId
            }*/);
        }
    }
}

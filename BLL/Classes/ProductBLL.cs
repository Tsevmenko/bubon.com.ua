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
using ViewModels.Views;

namespace BLL.Classes
{
    internal class ProductBLL : IProduct
    {

        private Repository<Product> repository { get; set; }
        private Repository<Category> categories { get; set; }

        private SizeBLL sizes { get; set; }
        private ColorBLL colors { get; set; }

        public ProductBLL(Repository<Product> repository, Repository<Category> categories, SizeBLL sizes, ColorBLL colors)
        {
            this.repository = repository;
            this.categories = categories;
            this.sizes = sizes;
            this.colors = colors;
        }

        public int Add(string name, decimal price, decimal? oldPrice, string description, List<int> categoryIds, List<SizeVM> sizes, List<ColorVM> colors/*int categoryId, string name, double price, double old_price, string description, List<SizeVM> sizes, List<ColorVM> colors, List<PhotoVM> photos*/)
        {
            Product entity = new Product()
            {
                Name = name,
                Price = price,
                OldPrice = oldPrice,
                Description = description
            };
            repository.Add(entity);
            repository.SaveChanges();

            int id = entity.Id;

            foreach (var el in categoryIds)
            {
                var cat = categories.Get().FirstOrDefault(p => p.Id == el);
                if (cat != null)
                {
                    entity.Categories.Add(cat);
                }
            }
            repository.SaveChanges();
            foreach (var el in sizes)
            {
                sizes.Add(el);
            }
            foreach (var el in colors)
            {
                colors.Add(el);
            }
            repository.SaveChanges();
            return id;
        }

        public bool Del(int id)
        {
            var entity = repository.Get().FirstOrDefault(p => p.Id == id);
            if (entity != null)
            {
                var deletedSizes = sizes.Get(entity.Id);
                foreach (var el in deletedSizes)
                {
                    sizes.Del(el.id);
                }
                var deletedColors = colors.Get(entity.Id);
                foreach (var el in deletedColors)
                {
                    colors.Del(el.id);
                }
                foreach (var el in entity.Categories)
                {
                    el.Category1.Remove(el);
                }
                repository.Delete(entity);
                repository.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Update(int id, string name, double price, double old_price, string description, List<ViewModels.SizeVM> sizes, List<ViewModels.ColorVM> colors, List<ViewModels.Views.PhotoVM> photos)
        {
            var entity = repository.Get().FirstOrDefault(p => p.Id == id);
            foreach (var el in sizes)
            {
                this.sizes.Update(el.id, el.productId, el.name, el.isActive);
            }
            foreach (var el in colors)
            {
                this.colors.Update(el.id, el.productId, el.name, el.value, el.isActive);
            }
            entity.Name = name;
            entity.Price = (decimal)price;
            entity.OldPrice = (decimal)old_price;
            entity.Description = description;
            repository.SaveChanges();
            return true;
        }

        public List<ViewModels.ProductVM> Get(int categoryId)
        {
            var res = repository.Get().Select(p => new ProductVM()
            {
                id = p.Id,
                name = p.Name,
                price = (double)p.Price,
                mainImage = "~/images/products/" + p.Id + "/main/main_thumbnail.jpg",
                oldPrice = (double)p.OldPrice,
                description = p.Description
            }).ToList();
            foreach (var el in res)
            {
                el.colors = colors.Get(el.id);
                el.sizes = sizes.Get(el.id);
            }
            return res;
        }

        public List<ProductVM> GetFromCategory(Category category)
        {
            var res = repository.Get().Where(p => p.Categories.Contains(category)).Select(p => new ProductVM()
                {
                    id = p.Id,
                    description = p.Description,
                    name = p.Name,
                    mainImage = "~/images/products/" + p.Id + "/main/main_thumbnail.jpg",
                    price = (double)p.Price,
                    oldPrice = (double)p.OldPrice
                }).ToList();
            return res;
        }

        public List<ProductVM> Get()
        {
            var res = repository.Get().Select(p => new ProductVM()
            {
                id = p.Id,
                name = p.Name,
                price = (double)p.Price,
                oldPrice = (double)p.OldPrice,
                mainImage = "~/images/products/" + p.Id + "/main/main_thumbnail.jpg",
                description = p.Description
            }).ToList();
            /*foreach (var el in res)
            {
                el.colors = colors.Get(el.id);
                el.sizes = sizes.Get(el.id);
            }*/
            return res;
        }
    }
}

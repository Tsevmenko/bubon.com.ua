using BLL.Classes;
using DAL.Classes;
using DLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace BLL
{
    public class BLL : IBLL
    {
        UnitOfWork _unit;

        Repository<Currency> currencies { get; set; }
        CurrencyBLL currenciesBLL { get; set; }

        Repository<Language> languages { get; set; }
        LanguageBLL languagesBLL { get; set; }

        Repository<Category> categories { get; set; }
        Repository<CategoryDescription> categoryDescriptions { get; set; }

        Repository<Product> products { get; set; }
        ProductBLL productsBLL { get; set; }

        Repository<Photo> photos { get; set; }
        PhotoBLL photosBLL { get; set; }

        Repository<Price> prices { get; set; }
        PriceBLL pricesBLL { get; set; }

        Repository<Size> sizes { get; set; }
        SizeBLL sizesBLL { get; set; }

        Repository<Color> colors { get; set; }
        ColorBLL colorsBLL { get; set; }

        CategoryBLL categoriesBLL { get; set; }

        public BLL()
        {
            _unit = new UnitOfWork();

            currencies = new Repository<Currency>(_unit);
            currenciesBLL = new CurrencyBLL(currencies);

            languages = new Repository<Language>(_unit);
            languagesBLL = new LanguageBLL(languages);

            categories = new Repository<Category>(_unit);
            categoryDescriptions = new Repository<CategoryDescription>(_unit);
            categoriesBLL = new CategoryBLL(categories, categoryDescriptions);

            prices = new Repository<Price>(_unit);
            //pricesBLL = new PriceBLL(prices);

            products = new Repository<Product>(_unit); 
            
            colors = new Repository<Color>(_unit);
            colorsBLL = new ColorBLL(colors, products);

            sizes = new Repository<Size>(_unit);
            sizesBLL = new SizeBLL(sizes, products);

            productsBLL = new ProductBLL(products, categories, sizesBLL, colorsBLL);

            photos = new Repository<Photo>(_unit);
            photosBLL = new PhotoBLL(photos, products);
        }
        public int AddCurrency(string name)
        {
            return currenciesBLL.Add(name);
        }
        public bool UpdateCurrency(int id, string name)
        {
            return currenciesBLL.Update(id, name);
        }
        public bool DelCurrency(int id)
        {
            return currenciesBLL.Del(id);
        }
        public List<CurrencyVM> GetCurrency()
        {
            return currenciesBLL.Get();
        }
        public int AddLanguage(string name, bool isDefault)
        {
            return languagesBLL.Add(name, isDefault);
        }
        public bool UpdateLanguage(int id, string name, bool isDefault)
        {
            return languagesBLL.Update(id, name, isDefault);
        }
        public bool DelLanguage(int id)
        {
            return languagesBLL.Del(id);
        }
        public List<LanguageVM> GetLanguages()
        {
            return languagesBLL.Get();
        }
        public int AddCategory(string name, int languageId, int? parentCategory)
        {
            return categoriesBLL.Add(name, languageId, parentCategory);
        }
        public bool UpdateCategory(int id, string name, int languageId, int? parentCategory)
        {
            return categoriesBLL.Update(id, name, languageId, parentCategory);
        }
        public bool DelCategory(int id)
        {
            return categoriesBLL.Del(id);
        }
        public List<CategoryVM> GetCategories(int languageId)
        {
            return categoriesBLL.Get(languageId);
        }
        // short product adding
        public int AddProduct(string name, decimal price, decimal? oldPrice, string description, List<int> categoryIds, List<SizeVM> sizes, List<ColorVM> colors/*int categoryId, string name, double price, double old_price, string description, List<SizeVM> sizes, List<ColorVM> colors, List<PhotoVM> photos*/)
        {
            return productsBLL.Add(name, price, oldPrice, description, categoryIds, sizes, colors);
        }
        public bool DelProduct(int id)
        {
            var entity = products.Get().FirstOrDefault(p => p.Id == id);
            if (entity != null)
            {
                var photo = photosBLL.Get(id);
                if (photo != null)
                    foreach (var el in photo)
                    {
                        photos.Delete(el);
                    }
                productsBLL.Del(entity.Id);
                products.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool UpdateProduct(int id, string name, decimal price, decimal? oldPrice, string description, List<int> categoryIds, List<SizeVM> sizes, List<ColorVM> colors)
        {
            var entity = products.Get().FirstOrDefault(p => p.Id == id);
            if (entity != null)
            {
                entity.Name = name;
                entity.Price = price;
                entity.OldPrice = oldPrice;
                entity.Description = description;
                foreach(var el in categoryIds)
                {
                    var cat = categories.Get().FirstOrDefault(p => p.Id == el);
                    if (cat != null)
                    {
                        entity.Categories.Add(cat);
                    }
                }
                products.SaveChanges();
                return true;    
            }
            return false;
        }
        public List<ProductVM> GetProducts()
        {
            return productsBLL.Get();
        }
        public List<ProductVM> GetProducts(int id)
        {
            Category entity = categories.Get().FirstOrDefault(p => p.Id == id);
            if (entity != null)
            {
                return productsBLL.Get(entity.Id);
            }
            else
            {
                return null;
            }
        }
        // short product adding
    }
}

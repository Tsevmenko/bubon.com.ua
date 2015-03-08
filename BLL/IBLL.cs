using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace BLL
{
    public interface IBLL
    {
        //currency
        int AddCurrency(string name);
        bool UpdateCurrency(int id, string name);
        bool DelCurrency(int id);
        List<CurrencyVM> GetCurrency();
        //end-currency
        //language
        int AddLanguage(string name, bool isDefault);
        bool UpdateLanguage(int id, string name, bool isDefault);
        bool DelLanguage(int id);
        List<LanguageVM> GetLanguages();
        //end-language
        //category
        int AddCategory(string name, int languageId, int? parentCategory);
        bool UpdateCategory(int id, string name, int languageId, int? parentCategory);
        bool DelCategory(int id);
        List<CategoryVM> GetCategories(int languageId);
        //end-category
        //product
        int AddProduct(string name, decimal price, decimal? oldPrice, string description, List<int> categoryIds, List<SizeVM> sizes, List<ColorVM> colors);
        bool DelProduct(int id);

        //end-product
    }
}

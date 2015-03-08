using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;
using ViewModels.Views;

namespace BLL.Interfaces
{
    public interface IProduct
    {
        /*int Add(int categoryId);
        bool AddPrice(int id, decimal price, int currencyId);
        bool ChangePrice(int id, decimal newPrice, int currencyId);
        bool AddPhotos(int id, string name);
        bool DelPhotos(int id, string name);
        int AddCharacteristic(int productId, string name, int languageId, bool isFiltered);
        bool ChangeCharacteristic(int id, int productId, string name, int languageId, bool isFiltered);
        bool DelCharacteristic(int id);
        int AddCharacteristicValue(int productId, int characteristicId, string value);
        bool ChangeCharacteristicValue(int id, int productId, int characteristicId, string value);
        bool DelCharacteristicValue(int id);
        bool Del(int id);
        bool Get(int languageId);*/

        int Add(string name, decimal price, decimal? oldPrice, string description, List<int> categoryIds, List<SizeVM> sizes, List<ColorVM> colors/*int categoryId, string name, double price, double old_price, string description, List<SizeVM> sizes, List<ColorVM> colors, List<PhotoVM> photos*/);
        bool Del(int id);
        bool Update(int id, string name, double price, double old_price, string description, List<SizeVM> sizes, List<ColorVM> colors, List<PhotoVM> photos);
        List<ProductVM> Get(int categoryId);
    }
}

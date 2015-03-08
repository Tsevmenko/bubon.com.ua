using System;
using DBModels;
using ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICategory
    {
        int Add(string name, int languageId, int? parentCategory);
        bool Del(int id);
        bool Update(int id, string name, int languageId, int? parentCategory);
        List<CategoryVM> Get(int languageId);
    }
}

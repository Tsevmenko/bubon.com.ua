using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBModels;
using ViewModels;

namespace BLL.Interfaces
{
    public interface ILanguage
    {
        int Add(string name, bool isDefault);
        bool Del(int id);
        bool Update(int id, string name, bool isDefault);
        List<LanguageVM> Get();
    }
}

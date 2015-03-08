using DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace BLL.Interfaces
{
    public interface ICurrency
    {
        int Add(string name);
        bool Del(int id);
        bool Update(int id, string name);
        List<CurrencyVM> Get();
    }
}

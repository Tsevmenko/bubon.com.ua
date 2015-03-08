using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace BLL.Interfaces
{
    public interface IColor
    {
        bool Add(int productId, string name, string value, bool isActive);
        bool Del(int id);
        bool Update(int id, int productId, string name, string value, bool isActive);
        List<ColorVM> Get(int productId);
    }
}

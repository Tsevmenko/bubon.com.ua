using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace BLL.Interfaces
{
    public interface ISize
    {
        bool Add(int productId, string name, bool isActive);
        bool Del(int id);
        bool Update(int id, int productId, string name, bool isActive);
        List<SizeVM> Get(int productId);
    }
}

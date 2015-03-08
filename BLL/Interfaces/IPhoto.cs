using DLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Views;

namespace BLL.Interfaces
{
    public interface IPhoto
    {
        bool Add(int productId, string name);
        bool Del(int id);
        bool Update(int id, int productId, string name);
        IEnumerable<Photo> Get(int productId);
    }
}

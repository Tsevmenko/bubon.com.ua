using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IBuyer
    {
        bool Add();
        bool Del();
        bool Update();
        bool Get();
    }
}

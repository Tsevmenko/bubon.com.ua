using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModels
{
    public class CategoryDBM
    {
        int id { get; set; }
        string name { get; set; }
        int parentCategory { get; set; }
    }
}

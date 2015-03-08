using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class CurrencyVM
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool del { get; set; }
        public CurrencyVM()
        {
            del = false;
        }
    }
}

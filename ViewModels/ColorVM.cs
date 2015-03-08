using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class ColorVM
    {
        public int id { get; set; }
        public int productId { get; set; }
        public string name { get; set; }
        public string value { get; set; }
        public bool isActive { get; set; }
    }
}

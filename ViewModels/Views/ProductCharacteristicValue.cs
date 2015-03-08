using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Views
{
    public class ProductCharacteristicValue
    {
        public int characteristicId { get; set; }
        public int languageId { get; set; }
        public int productId { get; set; }
        public string name { get; set; }
        public string value { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ViewModels.Views
{
    public class NewProduct
    {
        public int id { get; set; }
        public List<int> categoryIds { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public double oldPrice { get; set; }
        public string description { get; set; }
        public IEnumerable<SelectListItem> categories { get; set; }
        public IEnumerable<SelectListItem> languages { get; set; }
        public IEnumerable<SelectListItem> currencies { get; set; }
        public IEnumerable<SelectListItem> characteristics { get; set; }
        public List<ProductDescription> descriptions { get; set; }
        public List<ProductPrice> prices { get; set; }
        public List<ProductPhoto> photos { get; set; }
        public List<ProductCharacteristicValue> characteristicsValue { get; set; }
        public bool save { get; set; }
    }
}

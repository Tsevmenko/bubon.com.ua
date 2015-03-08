using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Views;

namespace ViewModels
{
    public class ProductVM
    {
        public int id { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public double oldPrice { get; set; }
        public string description { get; set; }
        public string mainImage { get; set; }
        public List<SizeVM> sizes { get; set; }
        public List<ColorVM> colors { get; set; }
        public List<PhotoVM> photos { get; set; }

        public ProductVM()
        {
            sizes = new List<SizeVM>();
            colors = new List<ColorVM>();
            photos = new List<PhotoVM>();
        }
    }
}

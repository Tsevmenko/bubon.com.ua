using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Views
{
    public class Categories
    {
        public NewCategory newCategory { get; set; }
        public List<CategoriesInsider> categories { get; set; } // categories that sorting by language
        public Dictionary<int, string> languages { get; set; }
        public bool res { get; set; }
        public Categories()
        {
            categories = new List<CategoriesInsider>();
        }
    }
}

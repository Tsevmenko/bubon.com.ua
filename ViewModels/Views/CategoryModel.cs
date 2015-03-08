using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Views
{
    public class CategoryModel
    {
        public int id { get; set; }
        public int? parentId { get; set; }
        public string name { get; set; }
        public string parentName { get; set; }
        public List<CategoryVM> categories { get; set; }
        public List<ProductVM> product { get; set; }

        public CategoryModel()
        {
            categories = new List<CategoryVM>();
            product = new List<ProductVM>();
        }
    }
}

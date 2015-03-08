using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ViewModels.Views
{
    public class NewCategory
    {
        public int? parentCategoryId { get; set; }
        public List<CategoryName> names { get; set; }
        public IEnumerable<SelectListItem> ParentCategory { get; set; }
        public NewCategory()
        {
            names = new List<CategoryName>();
        }
    }
}

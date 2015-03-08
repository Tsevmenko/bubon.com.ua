using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Views
{
    public class CategoriesInsider
    {
        public int languageId { get; set; }
        public List<CategoryVM> categories { get; set; }
    }
}

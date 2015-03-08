using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class CategoryVM
    {
        public int id { get; set; }
        public string name { get; set; }
        public int languageId { get; set; }
        public int? parentCategory { get; set; }
        public bool del { get; set; }
    }
}
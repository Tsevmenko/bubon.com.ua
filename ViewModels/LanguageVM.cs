using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class LanguageVM
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool isDefault { get; set; }
        public bool del { get; set; }
    }
}
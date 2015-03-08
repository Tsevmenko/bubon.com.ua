using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Views
{
    public class Language
    {
        public List<LanguageVM> languages { get; set; }
        public LanguageVM newLanguage { get; set; }
        public bool res { get; set; }
    }
}

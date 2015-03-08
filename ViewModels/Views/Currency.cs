using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Views
{
    public class Currency
    {
        public List<CurrencyVM> currencies { get; set; }
        public CurrencyVM newCurrency { get; set; }
        public bool res { get; set; }
    }
}

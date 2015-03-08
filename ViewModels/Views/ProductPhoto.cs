using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ViewModels.Views
{
    public class ProductPhoto
    {
        public string name { get; set; }
        public HttpPostedFileBase files { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Views
{
    public class CheckoutModel
    {
        public List<CartEntity> list { get; set; }
        public string message { get; set; }
        public string name { get; set; }
        public string delivery { get; set; }
        public string deliveryDetail { get; set; }
        public string payWay { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string comment { get; set; }
        public double total { get; set; }

        public CheckoutModel()
        {
            list = new List<CartEntity>();
        }
    }
}

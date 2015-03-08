using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Views
{
    public class OrderEntity
    {
        public int id { get; set; }
        public string name { get; set; }
        public string deliveryType { get; set; }
        public string deliveryDetail { get; set; }
        public string payWay { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string comment { get; set; }
        public List<double> list { get; set; }

        public OrderEntity()
        {
            list = new List<double>();
        }
    }
}

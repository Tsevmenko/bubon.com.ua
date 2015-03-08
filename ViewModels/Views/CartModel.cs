using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Views
{
    public class CartModel
    {
        public string message { get; set; }
        public List<CartEntity> list { get; set; }
        public CartModel()
        {
            list = new List<CartEntity>();
        }
    }
}

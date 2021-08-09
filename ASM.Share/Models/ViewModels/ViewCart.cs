using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASM.Share.Models.ViewModels
{
    public class Cart
    {
        public int KhanghangId { get; set; }
        public List<CartItem> ListViewCart { get; set; } = new List<CartItem>();
        public double Tongtien { get; set; }
    }

    public class CartItem
    {
        public MonAn MonAn { get; set; }
        public int Quantity { get; set; }
        public double Sotien { get; set; }
    }

    public class ViewCart
    {
        public MonAn MonAn { get; set; }
        public int Quantity { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TruyumClientApp.Models
{
    public class CartItem
    {
        public int CartItemID { get; set; }
        public int UserID { get; set; }
        public int MenuItemID { get; set; }
    }
}

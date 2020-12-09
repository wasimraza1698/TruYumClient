using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TruyumClientApp.Models
{
    public class MenuItemCust
    {
        public int MenuItemCustID { get; set; }
        public string Name { get; set; }
        public string FreeDelivery { get; set; }
        public float Price { get; set; }
        public string Category { get; set; }
    }
}

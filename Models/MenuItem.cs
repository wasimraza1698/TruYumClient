using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TruyumClientApp.Models
{
    public class MenuItem
    {
        public int MenuItemID { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Active { get; set; }
        public DateTime DateOfLaunch { get; set; }
        public string Category { get; set; }
        public string FreeDelivery { get; set; }
    }
}

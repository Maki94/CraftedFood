using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs
{
    public class MenuMealItem
    {
        public string Title { get; set; }
        public string Desription { get; set; }
        public byte[] Image { get; set; }
        public double Price { get; set; }
        public double? Quantity { get; set; }
        public Data.Enums.Units? Unit { get; set; }
        public Data.Enums.Categories? Category { get; set; }
        public double? Rating { get; set; }
    }
}

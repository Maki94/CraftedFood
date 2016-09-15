using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Data.DTOs
{
    public class MenuMealItem
    {
        public int MealId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public float Price { get; set; }
        public float Quantity { get; set; }
        public Data.Enums.Units Unit { get; set; }
        public Data.Enums.Categories Category { get; set; }
        public float? Rating { get; set; }
        public HttpPostedFileBase file { get; set; }

        public static MenuMealItem Load(int mealId)
        {
            return Meals.GetMealItem(mealId);
        }
    }
    

    public class MealCommentDTO
    {
        public string Date { get; set; }
        public string Comment { get; set; }
        public string CommenterName { get; set; }
    }
}

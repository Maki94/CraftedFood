using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data;
using Data.DTOs;
using Data.Entities;

namespace CrarftedFood.Models
{
    public class MenuViewModel
    {
        public List<MenuMealItem> menu { get; set; }
    }

    public class MealViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Data.Entities.Categories Category { get; set; }
        public float Quantity { get; set; }
        public byte[] Image { get; set; }
        public float Price { get; set; }
        public Data.Entities.Units Unit { get; set; }

        public static MealViewModel Load(int mealId)
        {
            
            var meal = Meals.GetMealAt(mealId);

            return new MealViewModel()
            {
                Id = meal.MealId,
                Title = meal.Title,
                Description = meal.Description,
                Category = (Data.Entities.Categories) meal.CategoryId,
                Quantity = meal.Quantity,
                Image = meal.Image.ToArray(),
                Price = meal.Price,
                Unit = (Data.Entities.Units) meal.UnitId
            };
        }

    }

}
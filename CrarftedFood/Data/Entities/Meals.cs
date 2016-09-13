using System;
using System.Linq;

namespace Data.Entities
{
    public static class Meals
    {
        public static void AddMeal(string title, string descirption, byte[] image, float price, float quantity,
            Units unit, Categories category)
        {
            using (var dc = new DataClassesDataContext())
            {
                Meal meal = new Meal
                {
                    Title = title,
                    Description = descirption,
                    CategoryId = (int) category,
                    Quantity = quantity,
                    Image = image,
                    Price = price,
                    UnitId = (int) unit
                };

                dc.Meals.InsertOnSubmit(meal);
                dc.SubmitChanges();
            }
        }

        public static void DeleteMeal(Meal meal)
        {
            using (var dc = new DataClassesDataContext())
            {
                dc.Meals.DeleteOnSubmit(meal);
                dc.SubmitChanges();
            }
        }

        public static void EditMeal(int mealId, string title = null, string descirption = null, byte[] image = null,
            float price = -1, float quantity = -1, Units unit = 0, Categories category = 0)
        {
            using (var dc = new DataClassesDataContext())
            {
                try
                {
                    var meal = dc.Meals.First(x => x.MealId == mealId);

                    if (title != null) meal.Title = title;
                    if (descirption != null) meal.Description = descirption;
                    if (image != null) meal.Image = image;
                    if (price != -1) meal.Price = price;
                    if (quantity != -1) meal.Quantity = quantity;
                    if (unit != 0) meal.UnitId = (int) unit;
                    if (category != 0) meal.CategoryId = (int) category;

                    dc.SubmitChanges();
                }
                catch (Exception e)
                {
                    throw;
                }
            }
        }
    }
}
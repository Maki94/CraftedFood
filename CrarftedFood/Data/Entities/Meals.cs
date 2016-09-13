namespace Data.Entities
{
    public static class Meals
    {
        public static void AddMeal(string title, string descirption, byte[] image, float price, float quantity, Units unit, Categories category)
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

        public static void EditMeal(Meal meal)
        {
            // TODO:

        }
    }
}
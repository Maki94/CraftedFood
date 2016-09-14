using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace Data.Entities
{
    public class Ratings
    {
        public static void AddRating(string comment, float rating1, DateTime date, int employeeId, int mealId)
        {
            try
            {
                using (var dc = new DataClassesDataContext())
                {
                    var rating = new Rating
                    {
                        Comment = comment,
                        MealId = mealId,
                        Date = date,
                        EmployeeId = employeeId,
                        Rating1 = rating1
                    };

                    dc.Ratings.InsertOnSubmit(rating);
                    dc.SubmitChanges();
                }
            }
            catch (Exception)
            {
                
                throw;
            }

        }

        public static void DeleteRating(int ratingId)
        {
            try
            {
                using (var dc = new DataClassesDataContext())
                {
                    var rating = dc.Ratings.First(x => x.RatingId == ratingId);

                    dc.Ratings.DeleteOnSubmit(rating);
                    dc.SubmitChanges();
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public static void EditRating(int ratingId, string comment = null, float rating1 = -1, DateTime? date = null, int employeeId = -1, int mealId = -1)
        {
            try
            {
                using (var dc = new  DataClassesDataContext())
                {
                    var rating = dc.Ratings.First(x => x.RatingId == ratingId);

                    if (comment != null) rating.Comment = comment;
                    if (rating1 != -1) rating.Rating1 = rating1;
                    if (date != null) rating.Date = date.Value;
                    if (employeeId != -1) rating.EmployeeId = employeeId;
                    if (mealId != -1) rating.EmployeeId = mealId;

                    dc.SubmitChanges();
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.ModelBinding;
using System.Web.UI.WebControls;
using Data.DTOs;
using Data.Enums;

namespace Data.Entities
{
    public static class Meals
    {
        public static void AddMeal(string title, string descirption, byte[] image, float price, float quantity,
            Units unit, Categories category)
        {
            try
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
                        UnitId = (int) unit,
                        IsDeleted = false
                    };

                    dc.Meals.InsertOnSubmit(meal);
                    dc.SubmitChanges();
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public static List<Data.DTOs.SendMailDto> DeleteMeal(int mealId)
        {
            using (var dc = new DataClassesDataContext())
            {
                try
                {
                    var ratings = dc.Ratings.Where(x => x.MealId == mealId).ToList();
                    dc.Ratings.DeleteAllOnSubmit(ratings);

                    var meal = dc.Meals.First(x => x.MealId == mealId);
                    meal.IsDeleted = true;
 
                    var requests =
                        dc.Requests.Where(x => x.MealId == mealId && x.DateToDeliver > DateTime.Today).Include(x => x.Employee).Include(x => x.Meal).ToList();
                    dc.Requests.DeleteAllOnSubmit(requests);

                    var sendMail = new List<Data.DTOs.SendMailDto>();
                    foreach (var request in requests)
                    {
                        string body =
                        "<p>Poštovani {0},</p> <p> Vaša narudžbina za obrok <strong>{1}</strong> je otkazana zbog uklanjanja istog iz naše baze podataka. Tako da Vas molimo da napravite novu porudžbu za <font color=blue>{2}</p><p>Srdačno, <br>Vatrene školjke</p>";
                        string message = string.Format(body, request.Employee.Name, request.Meal.Title, request.DateToDeliver.ToShortDateString());
                        sendMail.Add(new SendMailDto()
                        {
                            Email = request.Employee.Email,
                            Message = message
                        });
                    }

                    dc.SubmitChanges();

                    return sendMail;
                }
                catch (Exception)
                {
                    throw;
                }
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

        public static void editImage(int mealId, byte[] file)
        {
            using (DataClassesDataContext dc = new DataClassesDataContext())
            {
                Meal m = dc.Meals.First(a => a.MealId == mealId && !a.IsDeleted);
                m.Image = new System.Data.Linq.Binary(file);
                dc.SubmitChanges();

            }
        }

        public static float GetAverageRate(int? mealId)
        {
            using (DataClassesDataContext dc = new DataClassesDataContext())
            {
                return dc.Ratings.Where(x => x.MealId == mealId && x.Rating1 != null).Select(x => x.Rating1.Value).ToList().Average();
            }
        }

        public static List<MenuMealItem> GetMenu()
        {
            using (var dc = new DataClassesDataContext())
            {
                return dc.Meals.Where(x => !x.IsDeleted).Select(meal => new MenuMealItem
                {
                    MealId = meal.MealId,
                    Title = meal.Title,
                    Description = meal.Description,
                    Image = meal.Image == null ? null : meal.Image.ToArray(),
                    Price = meal.Price,
                    Quantity = meal.Quantity,
                    Unit = (Data.Enums.Units) meal.UnitId,
                    Category = (Data.Enums.Categories) meal.CategoryId,
                    Rating = meal.Ratings.Select(a => a.Rating1).Average()
                }).ToList();
            }
        }

        public static void CommentMeal(int empId, int mealId, string comment)
        {
            using (var dc = new DataClassesDataContext())
            {
                Rating r = new Rating
                {
                    EmployeeId = empId,
                    Comment = comment,
                    Date = DateTime.Now,
                    MealId = mealId
                };
                dc.Ratings.InsertOnSubmit(r);
                dc.SubmitChanges();
            }
        }

        public static List<MealCommentDTO> GetComments(int mealId)
        {
            using (var dc = new DataClassesDataContext())
            {
                return dc.Ratings.Where(a=> a.MealId == mealId && a.Comment!=null).Select(r => new MealCommentDTO
                {
                    Date = $"{r.Date:MM-dd-yy}",
                    Comment = r.Comment,
                    CommenterName = r.Employee.Name
                }).ToList();
            }
        }

        public static void RateMeal(int empId, int mealId, float rating)
        {
            using (var dc = new DataClassesDataContext())
            {
                Rating rate = dc.Ratings.FirstOrDefault(x => x.EmployeeId == empId && x.MealId == mealId);
                if (rate != null)
                {
                    rate.Rating1 = rating;
                }
                else
                {
                    Rating r = new Rating
                    {
                        EmployeeId = empId,
                        Rating1 = (float)rating,
                        Date = DateTime.Now,
                        MealId = mealId
                    };
                    dc.Ratings.InsertOnSubmit(r);
                }
                dc.SubmitChanges();
            }
        }

        public static Meal GetMealAt(int mealId)
        {
            using (var dc = new DataClassesDataContext())
            {
                return dc.Meals.First(x => x.MealId == mealId && !x.IsDeleted);
            }
        }

        public static MenuMealItem GetMealItem(int mealId)
        {
            using (var dc = new DataClassesDataContext())
            {
                return dc.Meals.Where(x => x.MealId == mealId && !x.IsDeleted).Select(x=> new Data.DTOs.MenuMealItem()
                {
                    MealId = x.MealId,
                    Title = x.Title,
                    Description = x.Description,
                    Category = (Data.Enums.Categories)x.CategoryId,
                    Quantity = x.Quantity,
                    Image = x.Image.ToArray(),
                    Price = x.Price,
                    Unit = (Data.Enums.Units)x.UnitId
                }).First();
            }
        }
        
        public static bool OrderMeal(int mealId, int empId, DateTime dateRequested, DateTime dateToDeliver, string note, float quantity)
        {
            using (DataClassesDataContext dc = new DataClassesDataContext())
            {
                try
                {
                    Request r = new Request()
                    {
                        MealId = mealId,
                        EmployeeId = empId,
                        DateRequested = dateRequested,
                        DateDelivered = null,
                        DateToDeliver = dateToDeliver,
                        Note = note,
                        Quantity = quantity,
                        payedDate = null,
                        Comment = null
                    };
                    dc.Requests.InsertOnSubmit(r);
                    dc.SubmitChanges();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

        public static bool CancelOrder(int requestId)
        {
            using (DataClassesDataContext dc = new DataClassesDataContext())
            {
                try
                {
                    Request mealRequest = dc.Requests.First(x => x.RequestId == requestId);
                    dc.Requests.DeleteOnSubmit(mealRequest);
                    dc.SubmitChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        
        public static void CommentRequest(int reqId, int empId, string comment)
        {
            using (DataClassesDataContext dc = new DataClassesDataContext())
            {
                Request request = dc.Requests.First(x => x.RequestId == reqId);
                if (request.EmployeeId == empId)
                {
                    request.Comment = comment;
                }
                dc.SubmitChanges();
            }
        }

        public static string GetLastCommentRequest(int reqId)
        {
            try
            {
                using (DataClassesDataContext dc = new DataClassesDataContext())
                {
                    Request request = dc.Requests.First(x => x.RequestId == reqId);
                    return request.Comment;
                }
            }
            catch (Exception)
            {
                return "";
                throw;
            }
        }
    }
}
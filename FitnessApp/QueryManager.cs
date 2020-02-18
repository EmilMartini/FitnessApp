using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace FitnessTracker
{
    public class QueryManager
    {
        public Result ValidateLogOn(FitnessContext context, HttpRequest request, out User user)
        {
            Result result = new Result();
            string password = request.Form["password"];
            string email = request.Form["email"];
            User newUser = context.Users.Where(o => o.Email == email && o.Password == password).FirstOrDefault();
            if (newUser != default)
            {
                user = newUser;
                result.Success = true;
            }
            else
            {
                user = null;
                result.Success = false;
                result.ErrorMessages.Add("email or password is not correct.");
            }
            return result;
        }

        public Result CreateUser(FitnessContext context, HttpRequest request)
        {
            Result result = new Result();
            var user = new User
            {
                FirstName = request.Form["firstname"],
                LastName = request.Form["lastname"],
                Age = int.Parse(request.Form["age"]),
                Email = request.Form["email"],
                Password = request.Form["password"],
                Activities = new System.Collections.Generic.List<Activity>()
            };
            if (context.Users.Where(o => o.Email == user.Email).Count() > 0)
            {
                result.Success = false;
                result.ErrorMessages.Add("Email address already in use.");
            }
            else
            {
                context.Users.Add(user);
                context.SaveChanges();
                result.Success = true;
            }
            return result;
        }

        public Result TryLogIn(FitnessContext context, User user, ActiveUserManager activeUserManager)
        {
            Result result = new Result();
            try
            {
                user.Activities = (from u in context.Users
                                   join a in context.Activities
                                   on u.Id equals a.UserId
                                   select a).ToList();

                activeUserManager.ActiveUser = user;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessages.Add(ex.Message);
            }
            return result;
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace FitnessTracker.Controllers
{
    public class HomeController : Controller
    {
        QueryManager queryManager = new QueryManager();
        ActiveUserManager activeUserManager = new ActiveUserManager();

        public ActionResult Index(List<string> errorMessages)
        {
            return View(new LoginFormViewModel { ErrorMessages = errorMessages });
        }

        public ActionResult MainScreen(User user)
        {
            return View(new UserViewModel(user));
        }

        public ActionResult TryLogIn()
        {
            using (var context = new FitnessContext())
            {
                User user;
                var validateResult = queryManager.ValidateLogOn(context, Request, out user);
                if(validateResult.Success)
                {
                    Result LogOnResult = queryManager.TryLogIn(context, user, activeUserManager);
                    if (LogOnResult.Success)
                    {
                        return View("MainScreen", activeUserManager.ActiveUser);
                    } else
                    {
                        return View("Index", validateResult.ErrorMessages);
                    }
                } 
                else
                {
                    return View("Index", validateResult.ErrorMessages);
                }
            }
        }

        public ActionResult CreateUser()
        {
            using(var context = new FitnessContext())
            {
                var result = queryManager.CreateUser(context, Request);
                if (result.Success)
                {
                    return View("MainScreen", activeUserManager.ActiveUser);
                } 
                else
                {
                    return View("Index", result.ErrorMessages);
                }
            }
        }
    }
}

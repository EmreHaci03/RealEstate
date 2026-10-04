using Microsoft.AspNetCore.Mvc;

namespace RealEstate_Dapper_UI.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/404")]
        public IActionResult NotFound()
        {
            return View();
        }

        [Route("Error/401")]
        public IActionResult Unauthorized()
        {
            return View();
        }

        [Route("Error/403")]
        public IActionResult Forbidden()
        {
            return View();
        }
        [Route("Error/StatusCode")]
       public IActionResult Error(int statusCode)
        {
            return statusCode switch
            {
                404 => View("NotFound"),
                     403=> View("Forbidden"),
                          401=> View("Unauthorized"),
                             _ => View("Error")
            };
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RealEstate_Dapper_UI.Areas.EstateAgent.Controllers
{
    [Area("EstateAgent")]
    public class AdvertsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

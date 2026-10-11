using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RealEstate_Dapper_UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class LiveStatisticController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

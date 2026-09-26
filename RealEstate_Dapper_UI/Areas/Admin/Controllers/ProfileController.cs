using Microsoft.AspNetCore.Mvc;

namespace RealEstate_Dapper_UI.Areas.Member.Controllers
{
    [Area("Admin")]
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

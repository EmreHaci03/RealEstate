using Microsoft.AspNetCore.Mvc;

namespace RealEstate_Dapper_UI.Areas.Admin.ViewComponents.AdminLayout
{
    public class AdminLayoutSideBarComponentPartial:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}

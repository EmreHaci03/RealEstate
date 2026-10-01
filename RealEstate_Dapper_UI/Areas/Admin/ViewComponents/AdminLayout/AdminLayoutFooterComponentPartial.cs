using Microsoft.AspNetCore.Mvc;

namespace RealEstate_Dapper_UI.Areas.Admin.ViewComponents.AdminLayout
{
    public class AdminLayoutFooterComponentPartial:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}

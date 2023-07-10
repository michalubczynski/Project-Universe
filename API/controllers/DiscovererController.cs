using BLL_BuisnessLogicLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace API.controllers
{
    public class DiscovererController : Controller
    {
        private readonly IService _iService;
        public DiscovererController(IService workService)
        {
            _iService = workService;
        }
        public IActionResult Discoverers()
        {
            return RedirectToPage("/Discoverers");
        }

    }
}

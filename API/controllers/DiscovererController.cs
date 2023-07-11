using BLL_BuisnessLogicLayer;
using Microsoft.AspNetCore.Mvc;

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

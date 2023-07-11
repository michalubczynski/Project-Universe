using BLL_BuisnessLogicLayer;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Universe.Models.discoverer;

namespace API.Pages
{
    public class DiscovererModel : PageModel
    {
        private readonly IService _iService;

        public DiscovererModel(IService workService)
        {
            _iService = workService;
        }

        public IEnumerable<Discoverer> Discoverers { get; set; }

        public void OnGet()
        {
            Discoverers = _iService.ShowDetailsDiscovererers();
        }
    }
}

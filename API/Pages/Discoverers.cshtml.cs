using BLL_BuisnessLogicLayer;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Universe.Models.discoverer;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task OnGet()
        {
            Discoverers = await _iService.ShowDetailsDiscovererers();
        }
    }
}

using BLL;
using BLL_BuisnessLogicLayer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Universe.Models.galaxy;
using Universe.Models.starsystem;

namespace TestingBLL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministracjaController : Controller
    {
       private readonly IWorkService workService;

       public AdministracjaController(IWorkService workService)
       {
           this.workService = workService;
       }

       [HttpPost("DodajPracownika")]
       public async Task HireNewDiscoverer(string name, string surname, int age) { 
           await workService.HireNewDiscoverer(name, surname, age);
       }

       [HttpPost("PrzeniesSystemGwiazd")]
       public async Task MoveStarSystemToAnotherGalaxy(StarSystem starsystemToMove, Galaxy destinationGalaxy)
       {
           await workService.MoveStarSystemToAnotherGalaxy(starsystemToMove, destinationGalaxy);
       }
    }
}

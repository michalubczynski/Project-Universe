using BLL;
using BLL_BuisnessLogicLayer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost("UsunMiasto")]
        public async Task UsunMiasto(int idMiasta)
        {
            await this.workService.UsunMiasto(idMiasta);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Universe.Models.galaxy;

namespace BLL
{
    public interface IGalaxyService
    {
        Task<IEnumerable<Galaxy>> GetAllGalaxiesAsync();
        Task<Galaxy> GetGalaxyByIdAsync(int? id);
        Task AddGalaxyAsync(Galaxy galaxy);
        Task UpdateGalaxyAsync(Galaxy galaxy);
        Task RemoveGalaxyAsync(int id);
        bool GalaxyExists(int id);
    }
}

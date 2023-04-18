using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Universe.Models.galaxy;
using Universe.Models;

namespace BLL
{
public class GalaxyService : IGalaxyService
{
    private readonly IUnitOfWork _unitOfWork;

        public GalaxyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Galaxy>> GetAllGalaxies()
        {
        return await _unitOfWork.GetRepository<Galaxy>().GetListAsync();
        }

        public async Task<IEnumerable<Galaxy>> GetAllGalaxiesAsync()
        {
            return await _unitOfWork.GetRepository<Galaxy>().GetListAsync();
        }
        public async Task<Galaxy> GetGalaxyByIdAsync(int? id)
        {
            if (id == null)
            {
                return null;
            }

            return await _unitOfWork.GetRepository<Galaxy>().FirstOrDefaultAsync(m => m.GalaxyId == id);
        }
        public async Task AddGalaxyAsync(Galaxy galaxy)
        {
            await _unitOfWork.GetRepository<Galaxy>().InsertAsync(galaxy);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateGalaxyAsync(Galaxy galaxy)
        {
            _unitOfWork.GetRepository<Galaxy>().Update(galaxy);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task RemoveGalaxyAsync(int id)
        {
            var galaxy = await _unitOfWork.GetRepository<Galaxy>().GetByIDAsync(id);
            _unitOfWork.GetRepository<Galaxy>().DeleteByT(galaxy);
            await _unitOfWork.SaveChangesAsync();
        }

        public bool GalaxyExists(int id)
        {
            return _unitOfWork.GetRepository<Galaxy>().Any(e => e.GalaxyId == id);
        }
    }
}

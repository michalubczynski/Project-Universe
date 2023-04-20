using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Universe.Models.galaxy;
using Universe.Models;
using Universe.Models.spaceobject;
using Universe.Models.planet;
using Universe.Models.ship;
using Universe.Models.star;

namespace BLL
{
    public interface ISpaceObjectService<T> where T : SpaceObject
    {
        Task<IEnumerable<T>> GetAllSpaceObjectsAsync();
        Task<T> GetSpaceObjectByIdAsync(int? id);
        Task AddSpaceObjectAsync(T obj);
        Task UpdateSpaceObjectAsync(T obj);
        Task RemoveSpaceObjectAsync(int id);
        bool SpaceObjectExists(int id);
    }
    public class SpaceObjectService<T> : ISpaceObjectService<T> where T : SpaceObject
    {
        private readonly IUnitOfWork _unitOfWork;
        public SpaceObjectService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<T>> GetAllSpaceObjects()
        {
            return await _unitOfWork.GetRepository<T>().GetListAsync();
        }

        public async Task<IEnumerable<T>> GetAllSpaceObjectsAsync()
        {
            return await _unitOfWork.GetRepository<T>().GetListAsync();
        }
        public async Task<T> GetSpaceObjectByIdAsync(int? id)
        {
            if (id == null)
            {
                return null;
            }
            return await _unitOfWork.GetRepository<T>().FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task AddSpaceObjectAsync(T obj)
        {
            await _unitOfWork.GetRepository<T>().InsertAsync(obj);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateSpaceObjectAsync(T obj)
        {
            _unitOfWork.GetRepository<T>().Update(obj);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task RemoveSpaceObjectAsync(int id)
        {
            //var obj = await _unitOfWork.GetRepository<T>().GetByIDAsync(id);
            _unitOfWork.GetRepository<T>().Delete(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public bool SpaceObjectExists(int id)
        {
            return _unitOfWork.GetRepository<T>().Any(e => e.Id == id);
        }

    }
}

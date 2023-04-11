using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Universe.Models;

namespace BuisnessLogicLayer
{
    // Interfejs dla klasy Administracja
    public interface IAdministrationOperation
    {
        public bool CreateDiscoverer();
        public bool KillDiscoverer();
        public bool CreateGalaxy();
        public bool MigrateSolarSystem();
    }

    internal class AdministractionActions:IAdministrationOperation
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdministractionActions(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool CreateDiscoverer()
        {
            return false;
        }
        public bool KillDiscoverer() {  return false; } 
        public bool MigrateGalaxy() { return false; }
        public bool CreateGalaxy() { return false; }
        public bool MigrateSolarSystem() { return false; }


    }
}

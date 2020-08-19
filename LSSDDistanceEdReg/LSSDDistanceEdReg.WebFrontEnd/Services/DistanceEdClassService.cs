using LSSD.DistanceEdReg;
using LSSD.DistanceEdReg.Data;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LSSDDistanceEdReg.WebFrontEnd.Services
{
    public class DistanceEdClassService
    {
        private IConfiguration _configuration { get; set; }
        private DistanceEdClassRepository _classRepo;

        public DistanceEdClassService(IConfiguration configuration)
        {
            this._configuration = configuration;
            this._classRepo = new DistanceEdClassRepository(_configuration.GetConnectionString(FrontendSettings.ConnectionStringName));
        }


        public List<DistanceEdClass> GetAll()
        {
            return _classRepo.GetAllClasses();
        }

        public List<DistanceEdClass> GetAllAvailable(DateTime effectiveDate)
        {
            return _classRepo.GetAvailableClasses(effectiveDate);
        }

        public DistanceEdClass Get(int ID)
        {
            return _classRepo.Get(ID);
        }

        public void Add(DistanceEdClass DEClass)
        {
            _classRepo.Add(DEClass);
        }

        public void Update(DistanceEdClass DEClass)
        {
            _classRepo.Update(DEClass);
        }

    }
}

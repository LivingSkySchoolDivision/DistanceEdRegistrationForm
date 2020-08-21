using LSSD.DistanceEdReg;
using LSSD.DistanceEdReg.Data;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LSSDDistanceEdReg.AdminFrontEnd.Services
{
    public class DistanceEdRequestService
    {
        private IConfiguration _configuration { get; set; }
        private DistanceEdRequestRepository _repository { get; set; }

        public DistanceEdRequestService(IConfiguration configuration)
        {
            this._configuration = configuration;
            this._repository = new DistanceEdRequestRepository(_configuration.GetConnectionString(FrontendSettings.ConnectionStringName));
        }

        public List<DistanceEdRequest> GetAll()
        {
            return _repository.GetAll();
        }

        public List<DistanceEdRequest> GetForCourse(DistanceEdClass Course)
        {
            return _repository.GetForCourse(Course);
        }

        public List<DistanceEdRequest> GetForCourse(int Course)
        {
            return _repository.GetForCourse(Course);
        }

        public List<DistanceEdRequest> GetForCourse(string Course)
        {
            return _repository.GetForCourse(Course);
        }

        public void Add(DistanceEdRequest NewRequest)
        {
            _repository.AddNewRequest(NewRequest);
        }

        public void Add(List<DistanceEdRequest> NewRequests)
        {
            _repository.AddNewRequests(NewRequests);
        }

    }
}

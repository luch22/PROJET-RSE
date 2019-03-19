using System;
using DS = DAL.Services;
using Client.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Mappers;

namespace Client.Services {
    public class PlanningService {

        private DS.PlanningService service;

        public PlanningService() {
            service = new DS.PlanningService();
        }

        public IEnumerable<Planning> GetByEmployee(int id, DateTime start, DateTime end) {
            return service.GetByEmployee(id, start, end).Select(a => a.ToPlanning());
        }

        public IEnumerable<Planning> GetAnniversaire(DateTime start, DateTime end) {
            return service.GetAnniversaire(start, end).Select(a => a.ToPlanning());
        }
    }
}

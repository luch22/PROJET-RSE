using Client.Mappers;
using Client.Models;
using DS = DAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;

namespace Client.Services {
    public class StatutEmployeeService {

        private DS.StatutEmployeeService service;

        public StatutEmployeeService() {
            service = new DS.StatutEmployeeService();
        }

        public IEnumerable<StatutEmployee> GetAll() {
            return service.GetAll().Select(a => a.ToClient());
        }

        public IEnumerable<StatutEmployee> GetByEmployee(int id) {
            return service.GetByEmployee(id).Select(a => a.ToClient());
        }
    }
}

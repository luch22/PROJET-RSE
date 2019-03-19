using System;
using DS = DAL.Services;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Models;
using Client.Mappers;

namespace Client.Services {
    public class RueService {

        private DS.RueService service;

        public RueService() {
            service = new DS.RueService();
        }

        public IEnumerable<Rue> Search(string nomRue, int zip) {
            return service.Search(nomRue, zip).Select(a => a.ToClient());
        }
    }
}

using System;
using DS = DAL.Services;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Models;
using Client.Mappers;

namespace Client.Services {
    public class NotificationService {

        private DS.NotificationService service;

        public NotificationService() {
            service = new DS.NotificationService();
        }

        public IEnumerable<Notification> GetByEmployee(int id) {
            return service.GetByEmployee(id).Select(a => a.ToClient());
        }

        public void MarkAsRead(int id) {
            service.MarkAsRead(id);
        }
    }
}

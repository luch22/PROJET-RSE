using DAL.Mappers;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolBox;

namespace DAL.Services {
    public class NotificationService {

        private readonly string providerName = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/_WebApp").ConnectionStrings.ConnectionStrings["SQLConnection"].ProviderName;
        private readonly string connString = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/_WebApp").ConnectionStrings.ConnectionStrings["SQLConnection"].ConnectionString;

        public IEnumerable<Notification> GetByEmployee(int id) {
            Connection connection = new Connection(providerName, connString);
            Command command = new Command("SELECT * FROM Notifications WHERE Id_Employee = @id AND (Date > DATEADD(DAY, -7, GETDATE()) OR Lu = 0);");
            command.AddParameter("id", id);

            return connection.ExecuteReader(command, (dr) => dr.ToNotification());
        }

        public void MarkAsRead(int id) {
            Connection connection = new Connection(providerName, connString);
            Command command = new Command("UPDATE Notifications SET Lu = 1 WHERE Id_Employee = @id AND Lu = 0;");
            command.AddParameter("id", id);

            connection.ExecuteNonQuery(command);
        }
    }
}

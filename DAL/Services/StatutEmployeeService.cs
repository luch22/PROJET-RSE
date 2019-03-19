using DAL.Models;
using DAL.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolBox;
using System.Configuration;

namespace DAL.Services {
    public class StatutEmployeeService {

        private readonly string providerName = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/_WebApp").ConnectionStrings.ConnectionStrings["SQLConnection"].ProviderName;
        private readonly string connString = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/_WebApp").ConnectionStrings.ConnectionStrings["SQLConnection"].ConnectionString;

        public IEnumerable<StatutEmployee> GetAll() {
            Connection connection = new Connection(providerName, connString);
            Command command = new Command("SELECT Id_Employee, Date_Debut, Date_Fin, Nom_Employee_Statut FROM Employee_Statut, Employee_Statut_Employee;");

            return connection.ExecuteReader(command, (dr) => dr.ToStatutEmployee());
        }

        public IEnumerable<StatutEmployee> GetByEmployee(int id) {
            Connection connection = new Connection(providerName, connString);
            Command command = new Command("EXEC SP_GetStatutByEmployee @id = @i;");
            command.AddParameter("i", id);

            return connection.ExecuteReader(command, (dr) => dr.ToStatutEmployee());
        }
    }
}

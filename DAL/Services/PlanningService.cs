using DAL.Mappers;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolBox;

namespace DAL.Services {
    public class PlanningService {

        private readonly string providerName = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/_WebApp").ConnectionStrings.ConnectionStrings["SQLConnection"].ProviderName;
        private readonly string connString = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/_WebApp").ConnectionStrings.ConnectionStrings["SQLConnection"].ConnectionString;

        public IEnumerable<Planning> GetByEmployee(int id, DateTime start, DateTime end) {
            Connection connection = new Connection(providerName, connString);
            Command command = new Command("EXEC SP_PlanningEmployee @id = @i, @start = @s, @end = @e;");
            command.AddParameter("i", id);
            command.AddParameter("s", start);
            command.AddParameter("e", end);

            return connection.ExecuteReader(command, (dr) => dr.ToPlanning());
        }

        public IEnumerable<Planning> GetAnniversaire(DateTime start, DateTime end) {
            Connection connection = new Connection(providerName, connString);
            Command command = new Command("EXEC SP_Anniversaires @start = @s, @end = @e");
            command.AddParameter("s", start);
            command.AddParameter("e", end);

            return connection.ExecuteReader(command, (dr) => dr.ToPlanning());
        }
    }
}

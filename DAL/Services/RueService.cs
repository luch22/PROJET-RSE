using DAL.Mappers;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolBox;

namespace DAL.Services {
    public class RueService {

        private readonly string providerName = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/_WebApp").ConnectionStrings.ConnectionStrings["SQLConnection"].ProviderName;
        private readonly string connString = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/_WebApp").ConnectionStrings.ConnectionStrings["SQLConnection"].ConnectionString;

        public IEnumerable<Rue> Search(string nomRue, int zip) {
            Connection connection = new Connection(providerName, connString);
            Command command = new Command("EXEC SP_AdresseSearch @inp = @np, @zip = @zp;");
            command.AddParameter("np", nomRue);
            command.AddParameter("zp", zip);

            return connection.ExecuteReader(command, (dr) => dr.ToRue());
        }
    }
}

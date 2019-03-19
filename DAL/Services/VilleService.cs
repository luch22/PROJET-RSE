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
    public class VilleService {

        private readonly string providerName = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/_WebApp").ConnectionStrings.ConnectionStrings["SQLConnection"].ProviderName;
        private readonly string connString = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/_WebApp").ConnectionStrings.ConnectionStrings["SQLConnection"].ConnectionString;

        public IEnumerable<Ville> GetAll() {
            Connection connection = new Connection(providerName, connString);
            Command command = new Command("SELECT * FROM Ville;");

            return connection.ExecuteReader(command, (dr) => dr.ToVille());
        }
        public IEnumerable<Ville> GetByVille() {
            Connection connection = new Connection(providerName, connString);
            Command command = new Command("SELECT * FROM Ville;");

            return connection.ExecuteReader(command, (dr) => dr.ToVille());
        }
        

        public Ville GetById(int id) {
            Connection connection = new Connection(providerName, connString);
            Command command = new Command("SELECT * FROM Ville WHERE Id_Ville = @Id;");
            command.AddParameter("Id", id);

            return connection.ExecuteReader(command, (dr) => dr.ToVille()).SingleOrDefault();
        }

        public Ville GetByNomZipPays(string nom, string zip, int id) {
            Connection connection = new Connection(providerName, connString);
            Command command = new Command("SELECT * FROM Ville WHERE Nom_Ville = @nv AND Zip = @zp AND Id_Pays = @id;");
            command.AddParameter("nv", nom);
            command.AddParameter("zp", zip);
            command.AddParameter("id", id);

            return connection.ExecuteReader(command, (dr) => dr.ToVille()).SingleOrDefault();
        }

        public IEnumerable<Ville> GetByZip(string zip)
        {
            Connection connection = new Connection(providerName, connString);
            Command command = new Command("SELECT nom_ville,Zip,Nom_Francais,Id_Ville,ville.Id_Pays FROM Ville INNER JOIN Pays ON Ville.Id_Pays = Pays.Id_Pays WHERE Zip = @zip; ");
            command.AddParameter("zip", zip);
            return connection.ExecuteReader(command, (dr) => dr.ToVille());
        }
    }
}

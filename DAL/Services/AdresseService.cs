using DAL.Mappers;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolBox;

namespace DAL.Services {
    public class AdresseService {

        private readonly string providerName = ConfigurationManager.ConnectionStrings["SQLConnection"].ProviderName;
        private readonly string connString = ConfigurationManager.ConnectionStrings["SQLConnection"].ConnectionString;

        public IEnumerable<Adresse> GetAll() {
            Connection connection = new Connection(providerName, connString);
            Command command = new Command("SELECT * FROM Adresse;");

            return connection.ExecuteReader(command, (dr) => dr.ToAdresse());
        }

        public IEnumerable<Adresse> GetByEvent()
        {
            Connection connection = new Connection(providerName, connString);
            Command command = new Command("SELECT * FROM Adresse WHERE Id_Adresse IN (SELECT e.Id_Adresse FROM Employee e INNER JOIN Employee_Participe_Event epe ON e.Id_Employee = epe.Id_Employee WHERE Id_Event = @id AND Present = 1 AND e.Id_Adresse IS NOT NULL)");

            return connection.ExecuteReader(command, (dr) => dr.ToAdresse());
        }

        public Adresse GetById(int id) {
            Connection connection = new Connection(providerName, connString);
            Command command = new Command("SELECT * FROM Adresse WHERE Id_Adresse = @Id;");
            command.AddParameter("Id", id);

            return connection.ExecuteReader(command, (dr) => dr.ToAdresse()).SingleOrDefault();
        }

        public int GetByRueVille(Adresse ad)
        {
            Connection connection = new Connection(providerName, connString);
            Command command = new Command("EXEC SP_GetByRueVille @rue = @ru ,@boitepostal2 = @bp ,@idville2 = @id;");
            command.AddParameter("id", ad.Id_Ville);
            command.AddParameter("bp", ad.Boite_Postal);
            command.AddParameter("ru", ad.Nom_Rue);
            
            return (int)connection.ExecuteScalar(command);
        }

        public Adresse Insert(Adresse a) {
            Connection connection = new Connection(providerName, connString);
            Command command = new Command("EXEC SP_AddAdresse @nomrue = @nr, @numero = @nm, @boitepostal = @bp, @idville = @iv;");
            command.AddParameter("nr", a.Nom_Rue);
            command.AddParameter("nm", a.Numero);
            command.AddParameter("bp", a.Boite_Postal == null ? (object)DBNull.Value : a.Boite_Postal);
            
            command.AddParameter("iv", a.Id_Ville);

            a.Id = (int)connection.ExecuteScalar(command);

            return a;
        }

        public bool Update(Adresse a) {
            Connection connection = new Connection(providerName, connString);
            Command command = new Command("EXEC SP_UpdateAdresse @id = @i, @nomrue = @nr, @boitepostal = @bp, @idville = @iv;");
            command.AddParameter("nr", a.Nom_Rue);
            command.AddParameter("bp", a.Boite_Postal);
            command.AddParameter("iv", a.Id_Ville);
            command.AddParameter("i", a.Id);

            return connection.ExecuteNonQuery(command) == 1;
        }

        public bool Delete(int id) {
            Connection connection = new Connection(providerName, connString);
            Command command = new Command("EXEC SP_DeleteAdresse @id = @i");
            command.AddParameter("i", id);

            return connection.ExecuteNonQuery(command) == 1;
        }
    }
}

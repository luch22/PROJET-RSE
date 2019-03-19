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
    public class EventService {

        private readonly string providerName = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/_WebApp").ConnectionStrings.ConnectionStrings["SQLConnection"].ProviderName;
        private readonly string connString = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/_WebApp").ConnectionStrings.ConnectionStrings["SQLConnection"].ConnectionString;

        public IEnumerable<Event> GetAll() {
            Connection connection = new Connection(providerName, connString);
            Command command = new Command("SELECT * FROM Events WHERE Date_Fin >= GETDATE();");

            return connection.ExecuteReader(command, (dr) => dr.ToEvent());
        }

        public Event GetById(int id) {
            Connection connection = new Connection(providerName, connString);
            Command command = new Command("SELECT * FROM Events WHERE Id_Event = @Id;");
            command.AddParameter("Id", id);

            return connection.ExecuteReader(command, (dr) => dr.ToEvent()).SingleOrDefault();
        }

        public Event Insert(Event e) {
            Connection connection = new Connection(providerName, connString);
            Command command = new Command("INSERT INTO Events (Nom_Event, Description, Lieu, Date_Debut, Date_Fin, FullDay, Id_Employee) VALUES (@ne, @d, @l, @dd, @df, @fd, @ie); SELECT CONVERT(int, @@IDENTITY);");
            command.AddParameter("ne", e.Nom);
            command.AddParameter("d", e.Description);
            command.AddParameter("l", e.Lieu);
            command.AddParameter("dd", e.DateDebut);
            command.AddParameter("df", e.DateFin);
            command.AddParameter("fd", e.FullDay);
            command.AddParameter("ie", e.Id_Employee);

            e.Id = (int)connection.ExecuteScalar(command);

            return e;
        }

        public bool Update(Event e) {
            Connection connection = new Connection(providerName, connString);
            Command command =
                new Command("UPDATE Events SET Nom_Event = @ne, Description = @d, Lieu = @l, Date_Debut = @dd, Date_Fin = @df, FullDay = @fd WHERE Id_Event = @id;");
            command.AddParameter("ne", e.Nom);
            command.AddParameter("d", e.Description);
            command.AddParameter("l", e.Lieu);
            command.AddParameter("dd", e.DateDebut);
            command.AddParameter("df", e.DateFin);
            command.AddParameter("fd", e.FullDay);
            command.AddParameter("ie", e.Id_Employee);
            command.AddParameter("id", e.Id);

            return connection.ExecuteNonQuery(command) == 1;
        }

        public bool Delete(int id) {
            Connection connection = new Connection(providerName, connString);
            Command command =
                new Command("UPDATE Events SET Date_Debut = Convert(datetime,'01-01-1970',103), Date_Fin = Convert(datetime,'01-01-1970',103) WHERE Id_Event = @id");
            command.AddParameter("id", id);

            return connection.ExecuteNonQuery(command) == 1;
        }

        public bool Participe(int idEm, int idEv)
        {
            Connection connection = new Connection(providerName, connString);
            Command command = new Command("EXEC SP_EmployeeParticipeEvent @idEmp = @idEm,@idEve = @idEv;");
            command.AddParameter("idEm", idEm);
            command.AddParameter("idEv", idEv);

            return connection.ExecuteNonQuery(command) == 1;
        }

        public bool ParticipeN(int idEm, int idEv)
        {
            Connection connection = new Connection(providerName, connString);
            Command command = new Command("EXEC SP_EmployeeNeParticipePlusEvent @idEmp	= @idEm,@idEve = @idEv");
            command.AddParameter("idEm", idEm);
            command.AddParameter("idEv", idEv);

            return connection.ExecuteNonQuery(command) == 1;
        }
    }
}

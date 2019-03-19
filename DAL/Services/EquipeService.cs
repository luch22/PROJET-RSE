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
    public class EquipeService {

        private readonly string providerName = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/_WebApp").ConnectionStrings.ConnectionStrings["SQLConnection"].ProviderName;
        private readonly string connString = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/_WebApp").ConnectionStrings.ConnectionStrings["SQLConnection"].ConnectionString;

        public IEnumerable<Equipe> GetAll() {
            Connection connection = new Connection(providerName, connString);
            Command command = new Command("SELECT * FROM Equipe;");

            return connection.ExecuteReader(command, (dr) => dr.ToEquipe());
        }

        public IEnumerable<Equipe> GetByProjet(int id) {
            Connection connection = new Connection(providerName, connString);
            Command command = new Command("EXEC SP_EquipeByProjet @id = @i;");
            command.AddParameter("i", id);

            return connection.ExecuteReader(command, (dr) => dr.ToEquipe());
        }

        public IEnumerable<Equipe> GetOtherByProjet(int id) {
            Connection connection = new Connection(providerName, connString);
            Command command = new Command("EXEC SP_OtherEquipeByProjet @id = @i;");
            command.AddParameter("i", id);

            return connection.ExecuteReader(command, (dr) => dr.ToEquipe());
        }

        public Equipe GetById(int id) {
            Connection connection = new Connection(providerName, connString);
            Command command = new Command("SELECT * FROM Equipe WHERE Id_Equipe = @id;");
            command.AddParameter("id", id);

            return connection.ExecuteReader(command, (dr) => dr.ToEquipe()).SingleOrDefault();
        }

        public Equipe GetByEmployee(int id) {
            Connection connection = new Connection(providerName, connString);
            Command command = new Command("EXEC SP_EquipeByEmployee @id = @i;");
            command.AddParameter("i", id);

            return connection.ExecuteReader(command, (dr) => dr.ToEquipe()).FirstOrDefault();
        }

        public Equipe Insert(Equipe e) {
            Connection connection = new Connection(providerName, connString);
            Command command = new Command("EXEC SP_AddEquipe @nom = @n, @date = @d, @idpr = @i;");
            command.AddParameter("n", e.Nom);
            command.AddParameter("d", e.Creee);
            command.AddParameter("i", e.Projet);

            e.Id = (int)connection.ExecuteScalar(command);

            return e;
        }

        public bool AffecterEmployee(List<int> idsEmp, int idEq) {
            Connection connection = new Connection(providerName, connString);
            try {
                foreach (int id in idsEmp) {
                    Command command = new Command("EXEC SP_AffecteEmployeeEquipe @idemp = @ie, @ideq = @ieq;");
                    command.AddParameter("ie", id);
                    command.AddParameter("ieq", idEq);

                    connection.ExecuteNonQuery(command);
                }
                return true;
            }
            catch (Exception) {
                return false;
            }
        }

        public bool RemoveEmployee(int emp) {
            Connection connection = new Connection(providerName, connString);
            Command command = new Command("EXEC SP_RemoveEmployeeEquipe @idemp = @ie;");
            command.AddParameter("ie", emp);
            
            return connection.ExecuteNonQuery(command) == 1;
        }

        public bool Update(Equipe e) {
            Connection connection = new Connection(providerName, connString);
            Command command = new Command("UPDATE Equipe SET Nom_Equipe = @ne, Date_Creation_Equipe = @d, Id_Projet = @ip WHERE Id_Equipe = @id;");
            command.AddParameter("ne", e.Nom);
            command.AddParameter("d", e.Creee);
            command.AddParameter("ip", e.Projet);
            command.AddParameter("id", e.Id);

            return connection.ExecuteNonQuery(command) == 1;
        }

        public bool Delete(int id) {
            Connection connection = new Connection(providerName, connString);
            Command command = new Command("DELETE FROM Equipe WHERE Id_Equipe = @id;");
            command.AddParameter("id", id);

            return connection.ExecuteNonQuery(command) == 1;
        }
    }
}

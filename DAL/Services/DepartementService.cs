using DAL.Models;
using DAL.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using ToolBox;

namespace DAL.Services {
    public class DepartementService {

        private readonly string providerName = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/_WebApp").ConnectionStrings.ConnectionStrings["SQLConnection"].ProviderName;
        private readonly string connString = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/_WebApp").ConnectionStrings.ConnectionStrings["SQLConnection"].ConnectionString;

        public IEnumerable<Departement> GetAll() {
            Connection connection = new Connection(providerName, connString);
            Command command = new Command("SELECT * FROM Departement;");

            return connection.ExecuteReader(command, (dr) => dr.ToDepartement());
        }

        public Departement GetById(int id) {
            Connection connection = new Connection(providerName, connString);
            Command command = new Command("SELECT * FROM Departement WHERE Id_Departement = @Id;");
            command.AddParameter("Id", id);

            return connection.ExecuteReader(command, (dr) => dr.ToDepartement()).SingleOrDefault();
        }

        public Departement GetByEmployee(int id) {
            Connection connection = new Connection(providerName, connString);
            Command command = new Command("SELECT d.Id_Departement, Nom_Departement, Description, Id_Admin FROM Departement d, Employee_Appartient_Departement e WHERE Id_Employee = @Id AND d.Id_Departement = e.Id_Departement AND e.Date_Fin IS NULL;");
            command.AddParameter("Id", id);

            return connection.ExecuteReader(command, (dr) => dr.ToDepartement()).SingleOrDefault();
        }

        public Departement Insert(Departement d) {
            Connection connection = new Connection(providerName, connString);
            Command command = new Command("EXEC SP_AddDepartement @nom = @nd, @des = @d, @idadmin = @ia;");
            command.AddParameter("nd", d.Nom);
            command.AddParameter("d", d.Description);
            command.AddParameter("ia", d.Admin);

            d.Id = (int)connection.ExecuteScalar(command);

            return d;
        }

        public bool AffecteEmployee(List<int> idsEmp, int idDep) {
            Connection connection = new Connection(providerName, connString);
            try { 
                foreach (int id in idsEmp) {
                    Command command = new Command("EXEC SP_AffecteEmployeeDep @idemp = @ie, @iddep = @id;");
                    command.AddParameter("ie", id);
                    command.AddParameter("id", idDep);

                    connection.ExecuteNonQuery(command);
                }
                return true;
            }
            catch (Exception) {
                return false;
            }
        }

        public bool RemoveEmployee(int dep, int emp) {
            Connection connection = new Connection(providerName, connString);
            Command command = new Command("EXEC SP_RemoveEmployeeDep @iddep = @id, @idemp = @ie;");
            command.AddParameter("ie", emp);
            command.AddParameter("id", dep);

            return connection.ExecuteNonQuery(command) == 1;
        }

        public bool Update(Departement d) {
            Connection connection = new Connection(providerName, connString);
            Command command = new Command("UPDATE Departement SET Nom_Departement = @nd, Description = @d, Id_Admin = @ia WHERE Id_Departement = @id;");
            command.AddParameter("nd", d.Nom);
            command.AddParameter("d", d.Description);
            command.AddParameter("ia", d.Admin);
            command.AddParameter("id", d.Id);

            return connection.ExecuteNonQuery(command) == 1;
        }

        public bool Delete(int id) {
            Connection connection = new Connection(providerName, connString);
            Command command = new Command("DELETE FROM Departement WHERE Id_Departement = @id;");
            command.AddParameter("id", id);

            return connection.ExecuteNonQuery(command) == 1;
        }
    }
}
